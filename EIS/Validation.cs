using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EIS
{
    public static class Validation
    {
        //private static DateTime _dt;
        //private static CultureInfo _ci = new CultureInfo("en-US");

        public static string FioStandart = "A B C";
        public static string NameStandart = "Abc";
        public static string PhoneStandart = "89998887766";
        public static string EmailStandart = "gaben@newell.com";
        public static string NumberStandart = "000";
        public static string PriceStandart = "00.00";
        public static string DateStandart = "16/05/2001";

        public static bool isFio(string Fio)
        {
            return Regex.IsMatch(Fio, @"^[а-яА-Яa-zA-Z]{1,15}[ ]{0,1}[а-яА-Яa-zA-Z]{0,15}[ ]{0,1}[а-яА-Яa-zA-Z]{0,15}$");
        }

        public static bool isName(string Name)
        {
            return Regex.IsMatch(Name, @"^[а-яА-Яa-zA-Z ]{1,50}$");
        }

        public static bool isPhone(string Phone)
        {
            return Regex.IsMatch(Phone, @"^[0-9]{1,12}$");
        }

        public static bool isEmail(string Email)
        {
            return Regex.IsMatch(Email, @"^[a-zA-Z]+[@]{1}[a-zA-Z]+[.]{1}[a-zA-Z]+$");
        }

        public static bool isNumber(string Number)
        {
            return Regex.IsMatch(Number, @"^[0-9]{1,20}$");
        }

        public static bool isPrice(string Price)
        {
            return Regex.IsMatch(Price, @"^[0-9]{1,13}[.]{1}[0-9]{1,2}$");
        }

        public static bool isDate(string Date)
        {
            return Regex.IsMatch(Date, @"^[0-9]{1,2}[/]{1}[0-9]{1,2}[/]{1}[0-9]{2,4}$");
            //return DateTime.TryParseExact(Date, "dd/M/yyyy", _ci, DateTimeStyles.None, out _dt);
        }
    }
}
