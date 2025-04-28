using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Utils
{
    internal class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;

            // 電話番号の正規表現（ハイフン付きを許可）
            if (!string.IsNullOrEmpty(phoneNumber) &&
                !Regex.IsMatch(phoneNumber, @"^\d{2,4}-\d{2,4}-\d{4}$"))
            {
                return new ValidationResult("電話番号は「080-1234-5678」の形式で入力してください。");
            }

            return ValidationResult.Success;
        }
    }
}
