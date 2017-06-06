using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Business.Interface
{
    public interface ILocalizationService
    {
        string Localize(string key);
        CultureInfo CultureInfo { set; get; }
    }
}
