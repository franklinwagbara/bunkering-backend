using Bunkering.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Core.Data
{
    public class AppConfiguration
    {
        private readonly ApplicationContext _context;
        public AppConfiguration(ApplicationContext context) { _context = context; }
        public Dictionary<string, string> Config()
        {
            var dic = new Dictionary<string, string>();
            _context.AppSettings.ToList().ForEach(x =>
            {
                dic.Add(x.Name, x.Value);
            });
            return dic;
        }

    }
}
