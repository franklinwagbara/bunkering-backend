using Azure.Core;
using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Bunkering.Core.Utils;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Security.Claims;

namespace Bunkering.Access.Services
{
    public class PaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string User;
        private int appId;
        ApiResponse _response;
        private readonly AppLogger _logger;
        private readonly string directory = "Payment";
        private readonly IElps _elps;
        private readonly AppSetting _appSetting;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor contextAccessor,
            AppLogger logger,
            IElps elps,
            IOptions<AppSetting> appSetting)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
             User = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            _logger = logger;
            _elps = elps;
            _appSetting = appSetting.Value;
        }

        public async Task<ApiResponse> CreatePayment(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                appId = int.Parse(id.DecryptString());
                if (appId > 0)
                {
                    var baseUrl = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
                    //var IsExtraPayment = _appSettings.IsExtraPayment;
                    var RemitaPayment_URL = $"{baseUrl}/api/bunkering/payment/Remita";
                    var ResponsePayment_URL = $"{baseUrl}/api/bunkering/payment/RemitaResponse";
                    //var FailedPayment_URL = baseUrl + _appSettings.FailedExtraPayment;

                    try
                    {
                        var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id == appId, "User.Company,Facility.LGA.State,Facility.FacilityType");
                        if (app == null)
                            _response = new ApiResponse { Message = "This application record does not exist or has been removed from the system, kindly contact support.", StatusCode = HttpStatusCode.NotFound };
                        else
                        {
                            var payment = await _unitOfWork.Payment.FirstOrDefaultAsync(x => x.ApplicationId == appId);

                            if (payment != null && !string.IsNullOrEmpty(payment.RRR))
                                _response = new ApiResponse { Message = "Payments already exists", StatusCode = HttpStatusCode.BadRequest };
                            else
                            {
                                if (payment == null)
                                {
                                    var fee = await _unitOfWork.AppFee.FirstOrDefaultAsync(x => x.ApplicationTypeId.Equals(app.ApplicationTypeId) && x.FacilityTypeId.Equals(app.Facility.FacilityTypeId));
                                    if(fee != null)
                                    {
                                        var total = fee.AdministrativeFee + fee.VesselLicenseFee + fee.ApplicationFee + fee.InspectionFee + fee.AccreditationFee;

                                        payment = new Payment
                                        {
                                            ApplicationId = appId,
                                            Status = Enum.GetName(typeof(AppStatus), 1),
                                            ServiceCharge = fee.SerciveCharge,
                                            Amount = total,
                                            Description = $"Administrative Fee: USD {fee.AdministrativeFee};Vessel License Fee: USD {fee.VesselLicenseFee};Application Fee: USD {fee.ApplicationFee};Inspection Fee: USD {fee.InspectionFee};Accreditation Fee: USD {fee.AccreditationFee}; ServiceCharge: NGN {fee.SerciveCharge}",
                                        };

                                        await _unitOfWork.Payment.Add(payment);
                                        await _unitOfWork.SaveChangesAsync(app.UserId);
                                        _logger.LogRequest($"{"Payment with ref: "}{" "}{" for "}{app.Reference}{" was created successfully by"}{User} {" - "}{DateTime.Now}", false, directory);

                                        var request = await _elps.GeneratePaymentReference($"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}", app, total, 0);
                                        _logger.LogRequest("Creation of payment split for application with reference:" + app.Reference + "(" + app.User.Company.Name + ") by " + User, false, directory);

                                        if (request == null)
                                        {
                                            await _unitOfWork.Payment.Remove(payment);
                                            await _unitOfWork.SaveChangesAsync("system");
                                            _logger.LogRequest($"Payment output from Remita:: {request.Stringify()} by {User}", true, directory);
                                            _response = new ApiResponse { Message = "An error occured while generating this payment RRR. Please try again or contact support.", StatusCode = HttpStatusCode.InternalServerError };

                                        }
                                        else
                                        {
                                            //var resp = request.Stringify().Parse<RemitaResponse>();

                                            if (!string.IsNullOrEmpty(request.RRR))
                                            {
                                                payment.RRR = request.RRR;
                                                await _unitOfWork.Payment.Update(payment);
                                                await _unitOfWork.SaveChangesAsync(app.UserId);

                                                _logger.LogRequest($"Payment table updated with RRR: {payment.RRR} by {User}", false, directory);

                                                #region Send Payment E-Mail To Company
                                                string subject = $"Generation Of Payment For Application With Ref:{app.Reference}";

                                                var emailBody = string.Format($"A Payment RRR: {payment.RRR} has been generated for your application with reference number: {app.Reference}" +
                                                    "<br /><ul>" +
                                                    "<li>Amount Generated: {0}</li>" +
                                                    "<li>Remita RRR: {2}</li>" +
                                                    "<li>Payment Status: {3}</li>" +
                                                    "<li>Payment Description: {3}</li>" +
                                                    "<li>Facility Name: {4}</li>" +
                                                    "<li>Facility Address: {5}</li> <br/>" +
                                                    "<p>Kindly note that your application will be pending until this payment is completed. </p>",
                                                    payment.Amount.ToString(), payment.RRR, payment.Status, payment.Description, $"{app.Facility.Name}-{app.Facility.ElpsId}", app.Facility.Address);

                                                #endregion

                                                string successMsg = "RRR (" + payment.RRR + ") generated successfully for company: " + app.User.Company.Name + "; Facility: " + $"{app.Facility.Name}-{app.Facility.ElpsId}";
                                                _response = new ApiResponse 
                                                { 
                                                    Message = successMsg, 
                                                    Data = payment.RRR, 
                                                    StatusCode = HttpStatusCode.OK, 
                                                    Success = true
                                                };
                                            }
                                            else
                                                _response = new ApiResponse
                                                {
                                                    Message = "Unable to generate RRR, pls try again",
                                                    StatusCode = HttpStatusCode.InternalServerError
                                                };
                                        }
                                    }                                    
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogRequest($"An error {ex.Message} occured while trying to generate extra payment RRR for this application by {User}", true, directory);
                        _response = new ApiResponse { Message = "An error occured while generating this extra payment RRR. Please try again or contact support.", StatusCode = HttpStatusCode.InternalServerError };
                    }
                }
            }
            return _response;
        }

        public async Task<ApiResponse> ConfirmPayment(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                appId = int.Parse(id.DecryptString());
                try
                {
                    var payment = await _unitOfWork.Payment.FirstOrDefaultAsync(x => x.ApplicationId == appId, "Application.User.Company,Application.Facility.LGA.State,Application.Facility.FacilityType");
                    if (payment != null)
                    {
                        if (!payment.Status.Equals(Enum.GetName(typeof(AppStatus), 2)) && !string.IsNullOrEmpty(payment.RRR))
                        {
                            //confirm payme nt status on remita via ELPS
                            var http = await Utils.Send(_appSetting.ElpsUrl, new HttpRequestMessage(HttpMethod.Get, $"/Payment/checkifpaid?id=r{payment.RRR}"));

                            if (http.IsSuccessStatusCode)
                            {
                                var content = http.Content.ReadAsStringAsync().Result;
                                if (content != null)
                                {
                                    var dic = content.Parse<Dictionary<string, string>>();
                                    if ((!string.IsNullOrEmpty(dic.GetValue("message").ToString()) && dic.GetValue("message").ToString().Equals("successful"))
                                        || (!string.IsNullOrEmpty(dic.GetValue("status").ToString()) && dic.GetValue("status").ToString().Equals("00")))
                                    {
                                        payment.Status = Enum.GetName(typeof(AppStatus), AppStatus.PaymentCompleted);
                                        payment.TransactionDate = Convert.ToDateTime(dic.GetValue("transactiontime"));
                                        payment.AppReceiptId = dic.GetValue("appreceiptid");
                                        payment.TxnMessage = "Confirmed";
                                        //payment.tx = Convert.ToDecimal(dic.GetValue("amount"));
                                        //payment.Application.Status = Enum.GetName(typeof(AppStatus), 2);

                                        await _unitOfWork.Payment.Update(payment);
                                        await _unitOfWork.SaveChangesAsync(payment.Application.UserId);

                                        _response = new ApiResponse
                                        {
                                            Message = "Payment confirmed successfully",
                                            StatusCode = HttpStatusCode.OK,
                                            Success = true,
                                        };
                                    }
                                    else
                                        _response = new ApiResponse
                                        {
                                            Message = "Not Successful",
                                            StatusCode = HttpStatusCode.BadRequest
                                        };
                                }
                            }
                            else
                                _response = new ApiResponse
                                {
                                    Message = "Failed",
                                    StatusCode = HttpStatusCode.NotFound
                                };
                        }
                        else
                            _response = new ApiResponse
                            {
                                Message = "Payment already completed",
                                StatusCode = HttpStatusCode.OK,
                                Success = true
                            };
                    }

                    _logger.LogRequest($"\"Getting payment for company application -:{payment.Application.Reference}{" by"}{_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                }
                catch (Exception ex)
                {
                    _response = new ApiResponse { Message = "Internal error occured " + ex.ToString(), StatusCode = HttpStatusCode.InternalServerError };
                    _logger.LogRequest($"{ex.Message} \n {ex.InnerException} \n {ex.StackTrace}", true, directory);
                }
            }
            return _response;
        }
    }
}
