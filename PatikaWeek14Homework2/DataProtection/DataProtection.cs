using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.DataProtection
{
    public class DataProtection : IDataProtection
    {
        public readonly IDataProtector _dataProtector;

        public DataProtection(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security-v1");
        }

        public string Protect(string text)
        {
          return  _dataProtector.Protect(text);
        }

        public string UnProtect(string protectText)
        {
            return _dataProtector.Unprotect(protectText);
        }
    }
}
