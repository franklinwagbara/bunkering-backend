using Bunkering.Access.DAL;
using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Bunkering.Access.Services
{
    public static class Dependencies
    {
        public static void Services(this IServiceCollection services)
        {
            services.AddScoped<AppConfiguration>();
            services.AddScoped<ApplicationContext>();
            services.AddScoped<AppProvessesService>();
            services.AddTransient<Seeder>();
            services.AddScoped<IElps, ElpsRepostory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<WorkFlowService>();
            services.AddScoped<AppService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<LicenseService>();
            services.AddScoped<CompanyService>();
            services.AddScoped<AppStageDocService>();
            services.AddScoped<StaffService>();
        }
    }
}
