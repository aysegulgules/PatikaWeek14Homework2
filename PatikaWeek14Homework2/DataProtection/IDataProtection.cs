using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.DataProtection
{
    public interface IDataProtection
    {
        string Protect(string text);
        string UnProtect(string protectText);
    }
}
