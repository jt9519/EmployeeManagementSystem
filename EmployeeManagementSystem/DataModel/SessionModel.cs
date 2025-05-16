using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataModel
{
    public class SessionModel
    {
        
        public int session_id { get; set; } // セッションID（主キー）
        
        public int employee_id { get; set; } // 社員ID（外部キー）
        
        public DateTime login_time { get; set; } // ログイン時間
        
        public DateTime? logout_time { get; set; } // ログアウト時間（null可能）
        
        public string? session_token { get; set; } // セッショントークン
        
        public string? ip_address { get; set; } // IPアドレス
      
        public string? user_agent { get; set; } // ブラウザ・デバイス情報

        public bool is_active { get; set; } // アクティブ状態（1: アクティブ, 0: 非アクティブ）



        public required EmployeeModel? EmployeeIdNavigation { get; set; }
    }
}
