using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Business.Interface;
using Core.CrossCutting;
using Core.Data.Model;

namespace Core.Business.Implementation
{
    public class LocalizationService:ILocalizationService
    {
        public string Localize(string key)
        {
            return Resources.ResourceManager.GetString(key, CultureInfo);
        }
        public CultureInfo CultureInfo { get; set; }
    }
}
