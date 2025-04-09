using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class OperatorModel
    {
        

        public int operator_id { get; set; }    // operator_id（主キー、整数型、NOT NULL）
        public int employee_id { get; set; }   // employee_id（整数型、NOT NULL）
        public int position_id { get; set; }   // position_id（整数型、NOT NULL）
        public required string password { get; set; }  // password（文字列型、NOT NULL）

        // Employee とのナビゲーションプロパティ
        public required EmployeeModel EmployeeIdNavigation { get; set; }



    }
}
