using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.DataModel;
using System.Timers;
using Timer = System.Timers.Timer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManagementSystem.Utils
{
    public static class SessionManager
    {
        public static int session_id { get; set; } // セッションID（主キー）

        public static int employee_id { get; set; } // 社員ID（外部キー）

        public static DateTime? login_time { get; set; } // ログイン時間

        public static DateTime? logout_time { get; set; } // ログアウト時間（null可能）

        public static string? session_token { get; set; } // セッショントークン

        public static string? ip_address { get; set; } // IPアドレス

        public static string? user_agent { get; set; } // ブラウザ・デバイス情報

        public static bool is_active { get; set; } // アクティブ状態（1: アクティブ, 0: 非アクティブ）


        // タイムアウト時間（30分）
        private static Timer? sessionTimer;
        private static readonly int TimeoutDurationInMinutes = 30;


        /// <summary>
        /// セッションが作成されたとき、タイマーを30分にセット
        /// </summary>
        public static void StartSession()
        {
            // タイマーを初期化
            sessionTimer = new Timer(TimeoutDurationInMinutes * 60 * 1000); // ミリ秒で指定
            //sessionTimer = new Timer(10 * 1000); // 30秒で指定(テスト用）
            sessionTimer.Elapsed += OnSessionTimeout;
            sessionTimer.AutoReset = false;
            sessionTimer.Start();
        }


        /// <summary>
        /// セッションタイムアウト時のイベント。<br/>
        /// セッションテーブルにログアウト時間を記録して、セッションをクリアし、タイムアウトしたことをメッセージ表示
        /// </summary>
        private static void OnSessionTimeout(object? sender, ElapsedEventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ログアウト時間の記録
            SetLogoutTime();
            //セッションクリア
            Session_Clear();

            MessageBox.Show(InformationMessages.INFO002_NOTIFY_SESSION_TIMEOUT, InformationMessages.TITLE003_SESSION_TIMEOUT, MessageBoxButtons.OK, MessageBoxIcon.Information);    

            // ログインフォーム以外をすべて閉じる
            CloseFormHelper.CloseSpecificForm("LoginForm");

        }


        /// <summary>
        /// Sessionテーブルにログアウト時間を記録する
        /// </summary>
        public static void SetLogoutTime()
        {
            logout_time = DateTime.Now; //ログアウト時間（メソッド呼び出し時の時間）

            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {

                    var SessionRecord = dbContext.Session
                        .Where(s => s.session_id == session_id) // フィールドのsession_idの値で検索
                        .FirstOrDefault(); // 最初のレコードを取得

                    //フィールドのsession_idの値で検索した結果がnullではないか
                    if (SessionRecord != null)
                    {
                        // logout_time、is_activeを更新
                        SessionRecord.logout_time = logout_time;
                        SessionRecord.is_active = false;

                        // データベースに保存
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // 例外処理
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// フィールドに格納されたデータをクリアする
        /// </summary>
        public static void Session_Clear()
        {
            session_id = 0;
            employee_id = 0;
            login_time = null;
            logout_time = null;
            session_token = null;
            ip_address = null;
            user_agent = null;
            is_active = false;
        }


    }
}
