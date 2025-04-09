using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class PositionModel
    {
        public int position_id { get; set; }      //  position_id（主キー、整数型、NOT NULL）
        public required string position_name { get; set; } // position_name（文字列型、NOT NULL）
    }
}
