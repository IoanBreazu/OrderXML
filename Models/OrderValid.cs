using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderXML.Models
{
    public static class OrderValid
    {
        public static bool IsValid(DataRow row)
        {
            if (!IsValidName(row["CustomerName"].ToString()))
                return false;
            if (!IsValidEmail(row["CustomerEmail"].ToString()))
                return false;
            if (!IsValidQt(row["Quantity"].ToString()))
                return false;
            if (!IsValidSize(row["Size"].ToString()))
                return false;
            if (!IsValidDateForOrder(row["DateRequired"].ToString()))
                return false;
            return true;
        }

        public static bool IsValidSize(string size)
        {
            float nSize; 
            if (!float.TryParse(size, out nSize))
                return false;
            if (nSize > 15)
                return false;
            if (nSize < 11.5)
                return false;
            if (nSize * 10 % 5 != 0)
                return false;
            return true;
        }

        public static bool IsValidQt(string qt)
        {
            short nQt;
            if (!short.TryParse(qt, out nQt))
                return false;
            if (nQt % 1000 != 0) 
                return false;
            if (nQt < 1000)
                return false;
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); 
            Match match = regex.Match(email);
            return match.Success;
        }

        public static bool IsValidDateForOrder(string sDateRequired)
        {
            DateTime dateRequired;
            int workingDays = 0;
            if (!DateTime.TryParse(sDateRequired, out dateRequired))
                return false;
            DateTime tempDate = DateTime.Today;
            while (tempDate < dateRequired)
            {
                bool isWorkingDay = true; 
                tempDate = tempDate.AddDays(1);
                if (tempDate.DayOfWeek == DayOfWeek.Sunday)
                    isWorkingDay = false;
                if (tempDate.DayOfWeek == DayOfWeek.Saturday)
                    isWorkingDay = false;
                if (isWorkingDay)
                    workingDays++;

            }
            return workingDays >= 10;
        }

        public static bool IsValidName(string name)
        {
            name = name.Trim(); 
            return name.Length > 0;
        }
    }

}
