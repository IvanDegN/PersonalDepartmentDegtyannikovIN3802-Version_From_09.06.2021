using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonalDepartmentDegtyannikovIN3802.ValidationRules
{
    public class PhoneRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phone = string.Empty;
            if(value != null)
            {
                phone = value.ToString();
            }
            else
            {
                return new ValidationResult(false, "Номер телефона не задан");
            }
            if(phone.Length == 11 && phone.Contains("+") || phone.Contains("8"))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Номер телефона должен быть длиной 11 символов и содержать символ + или цифру 8 \n" +
                    "Шаблон номера телефона: +79312999764 или 8931299764");
            }    
        }
    }
}
