using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Validation
    {
        

        public Validation()
        {

        }
        public static bool validateName(string input)
        {
            string pattern = "[^a-zA-Z]";
            if (Regex.IsMatch(input, pattern) && input.Length > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validateSurname(string input)
        {
            string pattern = "[^a-zA-Z]";
            if (Regex.IsMatch(input, pattern) && input.Length > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validateDegree(string input)
        {
            string pattern = "[^a-zA-Z]";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validateSalary(double input)
        {
            if (input == double.NaN && input < 150 && input > 2000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool validateUsername(string input)
        {
            string pattern = "[^a-zA-Z0-9]";
            if (Regex.IsMatch(input, pattern) && input.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validatePass(string input)
        {
            if (input.Length > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validateEmail(string input)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(input, pattern,RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
 
    }
}
