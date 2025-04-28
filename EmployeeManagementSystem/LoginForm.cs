using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.DataModel;
using EmployeeManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        //該当エラーメッセージリスト
        List<string> errorMessages = new List<string>();

        public LoginForm()
        {
            InitializeComponent();

            // ボタンクリックイベント
            btnLogin.Click += btnLogin_click;


        }

        private void btnLogin_click(object sender, EventArgs e)
        {
            {
                // 入力値を取得
                string inputMail = txtMail.Text;
                string inputPassword = txtPassword.Text;

                errorMessages = IsValidInput(inputPassword, inputMail);

                if (errorMessages == null || !errorMessages.Any())
                {
                    using (var dbContext = new EmployeeManagementSystemContext())
                    {
                        try
                        {
                            // メールアドレスとパスワードで検索
                            var operatorRecord = dbContext.Operator
                                .Join(dbContext.Employee,
                                      op => op.employee_id, // operatorテーブルの結合キー
                                      e => e.employee_id, // employeeテーブルの結合キー
                                      (op, e) => new { Operator = op, Mail = e.mail }) // 必要なデータをプロジェクション
                                .Where(record => record.Mail == inputMail && record.Operator.password == inputPassword) // 条件指定
                                .FirstOrDefault(); // 最初のレコードを取得



                            // SQLログを取得
                            //string logs = dbContext.GetLogs();

                            // メッセージボックスにログを表示
                            //MessageBox.Show(!string.IsNullOrEmpty(logs) ? logs : "ログがありません", "生成されたSQLログ", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            if (operatorRecord != null)
                            {
                                // 認証成功

                                //セッション情報の作成
                                int employeeId = operatorRecord.Operator.employee_id;
                                String session_token = CreateSession(employeeId);
                                
                                if(session_token != null)
                                {
                                    SessionModel sessionData = GetSessionRecordData(session_token);
                                    SetSession(sessionData);
                                    SessionManager.StartSession(); // セッションを開始
                                    Clear_Inputs();
                                    ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm(this);
                                    showEmployeeInfoForm.Show();
                                    this.Hide(); // ログインフォームを非表示

                                }                                
                            }
                            else
                            {
                                // 認証失敗
                                MessageBox.Show(ErrorMessages.ERR022_REQUIRED_VALID_PASSWORD_AND_MAIL, InformationMessages.TITLE010_AUTHENTICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 例外処理
                            MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    DisplayErrorMessages(errorMessages);
                    return;
                }
            }
        }

        private List<String> IsValidInput(String password, String mail)
        {
            List<string> isValidInput_ErrorMessages = new List<string>();

            // 入力検証: 空の場合はエラーメッセージを表示
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR022_MISSING_PASSWORD_AND_MAIL);
            }
            //メールアドレスが正しく入力されているか
            else if (!System.Text.RegularExpressions.Regex.IsMatch(mail, @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
            }

            return isValidInput_ErrorMessages;
        }

        private void DisplayErrorMessages(List<string> errorMessages)
        {
            // エラーメッセージを改行で区切って結合
            String messages = string.Join(Environment.NewLine, errorMessages);
            MessageBox.Show(messages, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        internal void Clear_Inputs()
        {
            txtMail.Text = null;
            txtPassword.Text = null;
        }

        private String CreateSession(int employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    String ip_address = GetIpAddress();
                    String user_agent = GetUserAgent();
                    String session_token = Guid.NewGuid().ToString();
                    DateTime login_time = DateTime.Now;

                    // 新しいセッションを作成
                    var newSession = new SessionModel
                    {
                        employee_id = employeeId,
                        login_time = login_time,
                        session_token = session_token,
                        ip_address = ip_address, // IPアドレスの取得メソッド
                        user_agent = user_agent, // UserAgentの取得メソッド
                        EmployeeIdNavigation = dbContext.Employee.FirstOrDefault(e => e.employee_id == employeeId)
                    };

                    // データベースに追加
                    dbContext.Session.Add(newSession);
                    dbContext.SaveChanges();

                    return session_token;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); // ホスト名を取得
            var ipAddress = Dns.GetHostAddresses(hostName)
                               .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return ipAddress?.ToString() ?? "不明";
        }

        public string GetUserAgent()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.Navigate("about:blank"); //WebBrowser コントロールを準備状態にする。
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                // User-Agentを取得
                dynamic script = browser.Document.DomDocument;
                return script?.parentWindow?.navigator?.userAgent ?? "不明";
            }
        }

        private SessionModel GetSessionRecordData(String session_token)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // メールアドレスとパスワードで検索
                    var SessionRecord = dbContext.Session
                        .Where(s => s.session_token == session_token) // 条件指定
                        .FirstOrDefault(); // 最初のレコードを取得
                        // セッションレコード取得成功
                        return SessionRecord;
                }
                catch (Exception ex)
                {
                    // 例外処理
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        private void SetSession(SessionModel sessionModel)
        {
            SessionManager.session_id = sessionModel.session_id;
            SessionManager.employee_id = sessionModel.employee_id;
            SessionManager.login_time = sessionModel.login_time;
            SessionManager.session_token = sessionModel.session_token;
            SessionManager.ip_address = sessionModel.ip_address;
            SessionManager.user_agent = sessionModel.user_agent;
            SessionManager.is_active = sessionModel.is_active;
        }
    }
}