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

        /// <summary>
        /// ログイン画面のイベント
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // ログインボタンクリックイベント
            btnLogin.Click += btnLogin_click;

        }

        /// <summary>
        /// ログインボタンクリックイベント。<br/>
        /// 入力されたメールアドレスとパスワードの組み合わせがDBにある場合社員情報一覧画面へ遷移するメソッド
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        private void btnLogin_click(object? sender, EventArgs e)
        {
            {
                if(sender == null)
                {
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 入力値を取得
                string inputMail = txtMail.Text; //メールアドレス
                string inputPassword = txtPassword.Text; //パスワード

                
                errorMessages = IsValidInput(inputPassword, inputMail); //入力チェックをしエラーメッセージのリストを受け取る

                //errorMessages が null または空のコレクションであるか
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



                            //検索結果がnullではないか
                            if (operatorRecord != null)
                            {
                                // 認証成功

                                //セッション情報の作成
                                int employeeId = operatorRecord.Operator.employee_id; //ログインしている社員番号
                                string? session_token = CreateSession(employeeId); //
                                
                                if(session_token != null)
                                {
                                    SessionModel? sessionData = GetSessionRecordData(session_token);
                                    SetSession(sessionData);
                                    SessionManager.StartSession(); // セッションを開始
                                    Clear_Inputs();

                                    // 現在のログイン画面 (`this`) のデータを渡して
                                    // 社員情報一覧画面 (`ShowEmployeeInfoForm`) をインスタンス化する
                                    ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm(this); 

                                    showEmployeeInfoForm.Show(); //社員情報一覧画面を表示
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

        /// <summary>
        /// パスワードとメールアドレスの入力チェックを行い、バリデーションが失敗した場合エラーメッセージのリストを返すメソッド（バリデーション成功の場合戻り値のリストはnull）
        /// </summary>
        private List<string> IsValidInput(string password, string mail)
        {

            List<string> isValidInput_ErrorMessages = new List<string>(); //エラーメッセージを格納するためのリスト


            // 入力検証: 空の場合はエラーメッセージを表示
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                //エラーメッセージをリストに追加
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR022_MISSING_PASSWORD_AND_MAIL);
            }
            //メールアドレスが正しく入力されているか
            else if (!System.Text.RegularExpressions.Regex.IsMatch(mail, @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
            }

            return isValidInput_ErrorMessages;
        }

        /// <summary>
        /// 引数で受け取ったエラーメッセージのリストをメッセージボックスで表示するメソッド
        /// </summary>
        /// <param name="errorMessages">エラーメッセージが格納されたリスト</param>
        private void DisplayErrorMessages(List<string> errorMessages)
        {
            // エラーメッセージを改行で区切って結合
            string messages = string.Join(Environment.NewLine, errorMessages);
            MessageBox.Show(messages, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>
        /// メールアドレスとパスワードのテキストボックスの入力値をクリアするメソッド
        /// </summary>
        internal void Clear_Inputs()
        {
            txtMail.Text = null;
            txtPassword.Text = null;
        }

        /// <summary>
        /// 引数でログインしようとしている社員番号を受け取り、受け取った社員番号、IPアドレス、<br/>
        /// クライアントブラウザ情報、作成したセッショントークン、ログイン時間をセッションテーブルに登録しセッショントークンを返すメソッド
        /// </summary>
        /// <param name="employeeId">ログインしようとしている社員番号</param>
        /// <returns>session_token</returns>
        private string? CreateSession(int employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    string ip_address = GetIpAddress(); //IPアドレス
                    string user_agent = GetUserAgent(); //クライアントブラウザ情報
                    string session_token = Guid.NewGuid().ToString(); //セッショントークン
                    DateTime login_time = DateTime.Now; //ログイン時間

                    // 新しいセッションを作成（セッションテーブルにセッション情報レコードの新規登録）
                    var newSession = new SessionModel
                    {
                        employee_id = employeeId,
                        login_time = login_time,
                        session_token = session_token,
                        ip_address = ip_address,
                        user_agent = user_agent,
                        is_active = true,
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

        /// <summary>
        /// IPアドレスを取得し返すメソッド
        /// </summary>
        /// <returns>ipAddress（IPアドレス）</returns>
        public string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); // ホスト名を取得

            //ホスト名のIPアドレスを取得（IPAddress型の配列）
            var ipAddress = Dns.GetHostAddresses(hostName)
                               .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return ipAddress?.ToString() ?? "不明";
        }

        /// <summary>
        /// ブラウザの種類・バージョン・OS などの情報を含む文字列を取得し返すメソッド
        /// </summary>
        /// <returns>ブラウザの種類・バージョン・OS などの情報を含む文字列（取得できなかった場合は‘不明’）</returns>
        public string GetUserAgent()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.Navigate("about:blank"); //WebBrowser コントロールを準備状態にする。

                //WebBrowser コントロールがページの読み込みを完了するまで
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents(); // UIの応答を維持

                }

                // WebBrowser コントロールの Document プロパティを使って、現在の HTML ドキュメントを取得
                dynamic? script = browser.Document?.DomDocument;

                return script?.parentWindow?.navigator?.userAgent ?? "不明";
            }
        }

        /// <summary>
        /// ログインしようとしている社員番号とセッショントークンの組み合わせを使ってセッションテーブルに問い合わせを行い、<br/>
        /// 取得したレコードの情報をセッションテーブルのデータモデル（SessionModel）にセットして返すメソッド
        /// </summary>
        /// <param name="session_token">セッショントークン</param>
        /// <returns>ログインしようとしている社員番号とセッショントークンの組み合わせでDBに問い合わせた結果を格納したSessionModelクラス型オブジェクト</returns>
        private SessionModel? GetSessionRecordData(string? session_token)
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

        /// <summary>
        /// SessionManagerクラスのフィールドにデータをセットするメソッド
        /// </summary>
        /// <param name="sessionModel">SessionModelクラス型オブジェクト</param>
        private void SetSession(SessionModel? sessionModel)
        {
            if (sessionModel != null)
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
}