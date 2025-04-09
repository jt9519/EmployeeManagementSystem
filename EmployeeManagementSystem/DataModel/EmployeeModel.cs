using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class EmployeeModel
    {
        [System.ComponentModel.DataAnnotations.Key] // 主キーを指定
        public int employee_id { get; set; } // employee_id (integer, Primary Key)
        public required string first_name { get; set; } // first_name (text, Not NULL)
        public required string last_name { get; set; } // last_name (text, Not NULL)
        public required string kana_first_name { get; set; } // kana_first_name (text, Not NULL)
        public required string kana_last_name { get; set; } // kana_last_name (text, Not NULL)
        public required string mail { get; set; } // mail (text, Not NULL)
        public required string phone_num { get; set; } // phone_num (text, Not NULL)
        public DateTime hire_date { get; set; } // hire_date (date, Not NULL)
        public int office_id { get; set; } // office_id (integer, Not NULL)
        public int position_id { get; set; } // position_id (integer, Not NULL)
        public int status { get; set; } // status (integer, Not NULL)


        public ICollection<OperatorModel> Operators { get; set; } = new List<OperatorModel>(); //OperatorModel とのリレーション (1対多)

    }
}
