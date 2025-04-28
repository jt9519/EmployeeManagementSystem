using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem.DataModel
{
    public class EmployeeModel
    {
        [System.ComponentModel.DataAnnotations.Key] // 主キーを指定
        public int employee_id { get; set; } // employee_id (integer, Primary Key)

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        public required string first_name { get; set; } // first_name (text, Not NULL)

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        public required string last_name { get; set; } // last_name (text, Not NULL)

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        [HiraganaOnly]
        public required string kana_first_name { get; set; } // kana_first_name (text, Not NULL)

        [Required(ErrorMessage = "名前は必須です")]
        [StringLength(25, ErrorMessage = "名前は25文字以内にしてください")]
        [HiraganaOnly]
        public required string kana_last_name { get; set; } // kana_last_name (text, Not NULL)

        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        public required string mail { get; set; } // mail (text, Not NULL)

        [Required(ErrorMessage = "電話番号は必須です")]
        [PhoneNumberValidation]
        public required string phone_num { get; set; } // phone_num (text, Not NULL)

        [NotFutureDate]
        public DateTime hire_date { get; set; } // hire_date (date, Not NULL)

        public int office_id { get; set; } // office_id (integer, Not NULL)

        public int position_id { get; set; } // position_id (integer, Not NULL)

        public int status { get; set; } // status (integer, Not NULL)



        public ICollection<OperatorModel> Operator { get; set; } = new List<OperatorModel>(); //OperatorModel とのリレーション (1対多)

        public ICollection<SessionModel> Session { get; set; } = new List<SessionModel>(); //SessionModel とのリレーション (1対多)

    }
}
