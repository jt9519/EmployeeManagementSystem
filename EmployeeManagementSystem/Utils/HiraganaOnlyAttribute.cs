using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Utils
{
    internal class HiraganaOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string input = value as string;

            // 平仮名の正規表現チェック
            if (!string.IsNullOrEmpty(input) && !Regex.IsMatch(input, @"^[\u3041-\u3096ー]+$"))
            {
                return new ValidationResult("このフィールドはひらがなのみ入力できます");
            }

            return ValidationResult.Success;
        }
    }
}
