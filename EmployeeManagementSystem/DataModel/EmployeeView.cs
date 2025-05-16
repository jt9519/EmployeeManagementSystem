using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem.DataModel
{
    public class EmployeeView
    {

        public int employee_id { get; set; }

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        public string? first_name { get; set; }

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        public string? last_name { get; set; }

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        [HiraganaOnly]
        public string? kana_first_name { get; set; }

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        [HiraganaOnly]
        public string? kana_last_name { get; set; }

        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        public string? mail { get; set; }

        [Required(ErrorMessage = "電話番号は必須です")]
        [PhoneNumberValidation]
        public string? phone_num { get; set; }

        [NotFutureDate]
        public DateTime? hire_date { get; set; }

        public string? office_name { get; set; } 

        public string? position_name { get; set; }

        public int status { get; set; }
    }
}
