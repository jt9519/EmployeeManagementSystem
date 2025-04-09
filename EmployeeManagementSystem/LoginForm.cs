using EmployeeManagementSystem.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // フォームサイズを固定
            this.ClientSize = new Size(1000, 800); // 横1000, 縦800
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // サイズ変更を無効化
            this.MaximizeBox = false; // 最大化ボタンを無効化
            this.StartPosition = FormStartPosition.CenterScreen; // 画面中央に表示

            // パスワード入力ボックスの設定（マスキング）
            txtPassword.UseSystemPasswordChar = true;

            // ボタンクリックイベント
            btnLogin.Click += (s, e) =>
            {
                // 入力値を取得
                string inputMail = txtMail.Text;
                string inputPassword = txtPassword.Text;

                // 入力検証: 空の場合はエラーメッセージを表示
                if (string.IsNullOrEmpty(inputMail) || string.IsNullOrEmpty(inputPassword))
                {
                    MessageBox.Show("メールアドレスとパスワードを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
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

                        //var joinedData = dbContext.Operator
                        //    .Join(dbContext.Employee,
                        //    op => op.employee_id,
                        //    e => e.employee_id,
                        //    (op, e) => new { Operator = op, Mail = e.mail })
                        //    .ToList();
                        //MessageBox.Show($"結合されたデータ数: {joinedData.Count}",
                        //    "デバッグ結果", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        //MessageBox.Show($"メール: {inputMail}, パスワード: {inputPassword}", "入力値確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (operatorRecord != null)
                        //{
                        //    MessageBox.Show($"メール: {operatorRecord.Mail}, パスワード: {operatorRecord.Operator.Password}",
                        //        "クエリ結果確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("クエリ結果が空です。データがありません。",
                        //        "確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}


                        // SQLログを取得
                        //string logs = dbContext.GetLogs();

                        // メッセージボックスにログを表示
                        //MessageBox.Show(!string.IsNullOrEmpty(logs) ? logs : "ログがありません", "生成されたSQLログ", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (operatorRecord != null)
                        {
                            // 認証成功
                            //MessageBox.Show("ログイン成功しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm();
                            showEmployeeInfoForm.Show();
                            this.Hide(); // ログインフォームを非表示
                        }
                        else
                        {
                            // 認証失敗
                            MessageBox.Show("メールアドレスまたはパスワードが正しくありません。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 例外処理
                        MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
        }
    }
}