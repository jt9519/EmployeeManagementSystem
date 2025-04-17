using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class EmployeeView
    {
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string kana_first_name { get; set; }
        public string kana_last_name { get; set; }
        public string mail { get; set; }
        public string phone_num { get; set; }
        public DateTime hire_date { get; set; }
        public string office_name { get; set; }
        public string position_name { get; set; }
        public int status { get; set; }
    }
}
