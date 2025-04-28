using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Utils
{
    internal class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? inputDate = value as DateTime?;

            if (inputDate.HasValue && inputDate.Value > DateTime.Now)
            {
                return new ValidationResult("雇用日は未来日を設定できません。");
            }

            return ValidationResult.Success;
        }
    }
}
