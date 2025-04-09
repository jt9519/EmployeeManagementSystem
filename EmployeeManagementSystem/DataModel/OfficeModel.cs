using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class OfficeModel
    {
        public int office_id { get; set; }      // office_id（主キー、整数型、NOT NULL）
        public required string office_name { get; set; } // office_name（文字列型、NOT NULL）
        public required string location { get; set; }   // locaiton（文字列型、NOT NULL）
    }
}
