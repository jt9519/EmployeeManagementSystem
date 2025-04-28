using EmployeeManagementSystem.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class ShowEmployeeInfoDetailForm : Form
    {
        private string EmployeeId;
        private ShowEmployeeInfoForm parentForm;
        public ShowEmployeeInfoDetailForm(string employeeId, ShowEmployeeInfoForm parentForm)
        {
            InitializeComponent();
            this.EmployeeId = employeeId;
            this.ClientSize = new Size(1200, 800); // フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // サイズ変更を無効化
            this.MaximizeBox = false; // 最大化ボタンを無効化
            this.StartPosition = FormStartPosition.CenterScreen; // 画面中央に表示

            //フォームロード時にデータを表示
            this.Load += ShowEmployeeInfoDetailForm_Load;

            //更新ボタン押下後アクション
            this.btnUpdate.Click += btnUpdate_Click;

            // クリアボタンクリック
            this.btnClear.Click += btnClear_Click;

            //削除ボタンクリック
            this.btnDelete.Click += btnDelete_Click;

            // FormClosing イベントで社員情報一覧画面のメソッドを呼び出し
            this.parentForm = parentForm;  // 親フォームを保存
            this.FormClosing += ShowEmployeeInfoDetailForm_FormClosing;


            // キャンセルリンククリックイベント
            linkCancel.Click += (s, e) =>
            {
                // 確認ダイアログを表示
                DialogResult result = MessageBox.Show(
                    "本当にキャンセルしてもよろしいですか？",
                    "確認",
                    MessageBoxButtons.YesNo, // YesとNoボタンを表示
                    MessageBoxIcon.Warning  // 警告アイコンを表示
                );

                // ダイアログの結果に基づいて処理
                if (result == DialogResult.Yes)
                {
                    // キャンセル処理を実行
                    this.Close(); // フォームを閉じる例
                }
            };
        }

        //フォームを閉じた後、社員情報一覧画面のグリッドの結果を更新する
        private void ShowEmployeeInfoDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (parentForm != null)
            {
                // 社員情報一覧画面のメソッドを呼び出す
                parentForm.CheckIfFormClosed();
            }
        }



        // 初期値を保存するディクショナリ
        private Dictionary<string, string> initialValues = new Dictionary<string, string>();

        private void ShowEmployeeInfoDetailForm_Load(object sender, EventArgs e)
        {
            setConboBoxData();
            LoadEmployeeDetails(EmployeeId);


        　　// 各項目の初期値を保存
        　　initialValues["kana_first_name"] = txtKanaFirstName.Text;
            initialValues["kana_last_name"] = txtKanaLastName.Text;
            initialValues["first_name"] = txtFirstName.Text;
            initialValues["last_name"] = txtLastName.Text;
            initialValues["mail"] = txtMail.Text;
            initialValues["phone_num"] = txtPhoneNum.Text;
            initialValues["office_name"] = selectOffice.Text;
            initialValues["position_name"] = selectPosition.Text;
        }
        private void LoadEmployeeDetails(string employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                // 必要なデータを取得するクエリ
                var employeeData = (from e in dbContext.Employee
                                    join o in dbContext.Office on e.office_id equals o.office_id
                                    join p in dbContext.Position on e.position_id equals p.position_id
                                    where e.employee_id.ToString() == employeeId
                                    select new
                                    {
                                        e.employee_id,
                                        e.first_name,
                                        e.last_name,
                                        e.kana_first_name,
                                        e.kana_last_name,
                                        e.mail,
                                        e.phone_num,
                                        e.hire_date,
                                        o.office_name,
                                        p.position_name
                                    }).FirstOrDefault();
                if (employeeData != null)
                {
                    // 各コントロールにデータを設定 
                    lblAppearEmployeeId.Text = employeeData.employee_id.ToString();
                    txtKanaFirstName.Text = employeeData.kana_first_name;
                    txtKanaLastName.Text = employeeData.kana_last_name;
                    txtFirstName.Text = employeeData.first_name;
                    txtLastName.Text = employeeData.last_name;
                    txtMail.Text = employeeData.mail;
                    txtPhoneNum.Text = employeeData.phone_num;
                    lblAppearHireDate.Text = employeeData.hire_date.ToString("yyyy/MM/dd");
                    selectOffice.SelectedItem = employeeData.office_name;
                    selectPosition.SelectedItem = employeeData.position_name;
                }
                else
                {
                    MessageBox.Show("該当する社員のデータが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        //拠点と役職の項目をコンボボックスにセット
        private void setConboBoxData()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    //検索エリア　拠点コンボボックス内の項目設定
                    var officeNames = dbContext.Office // OfficeテーブルへのDbSet
                        .Select(o => o.office_name) // office_name列を選択
                        .ToList();

                    // コンボボックスに選択肢を設定
                    selectOffice.Items.Clear();
                    foreach (var name in officeNames)
                    {
                        selectOffice.Items.Add(name);
                    }

                    //検索エリア　役職コンボボックス内の項目設定
                    var positionNames = dbContext.Position // OfficeテーブルへのDbSet
                        .Select(o => o.position_name) // office_name列を選択
                        .ToList();

                    // コンボボックスに選択肢を設定
                    selectPosition.Items.Clear();
                    foreach (var name in positionNames)
                    {
                        selectPosition.Items.Add(name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"データの読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 現在の値を収集
            var currentValues = new Dictionary<string, string>
            {
                ["kana_first_name"] = txtKanaFirstName.Text,
                ["kana_last_name"] = txtKanaLastName.Text,
                ["first_name"] = txtFirstName.Text,
                ["last_name"] = txtLastName.Text,
                ["mail"] = txtMail.Text,
                ["phone_num"] = txtPhoneNum.Text,
                ["office_name"] = selectOffice.Text,
                ["position_name"] = selectPosition.Text
            };

            // 変更があるかチェック
            bool hasChanges = initialValues.Any(iv => iv.Value != currentValues[iv.Key]);

            if (hasChanges)
            {
                // 入力内容を検証
                if (!ValidateInput())
                {
                    MessageBox.Show("入力エラーがあります。修正してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新処理を実行
                UpdateDatabase(currentValues);

                // 初期値を更新
                initialValues = new Dictionary<string, string>(currentValues);

                MessageBox.Show("データが更新されました。", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                // 変更がない場合
                MessageBox.Show("変更がありません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateDatabase(Dictionary<string, string> currentValues)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                // EmployeeId を使って対象のデータを取得
                int employeeId = int.Parse(EmployeeId);
                var employee = dbContext.Employee.SingleOrDefault(e => e.employee_id == employeeId);

                if (employee != null)
                {
                    // フィールドを更新
                    employee.kana_first_name = currentValues["kana_first_name"];
                    employee.kana_last_name = currentValues["kana_last_name"];
                    employee.first_name = currentValues["first_name"];
                    employee.last_name = currentValues["last_name"];
                    employee.mail = currentValues["mail"];
                    employee.phone_num = currentValues["phone_num"];

                    // office_name から office_id を取得
                    var office = dbContext.Office.SingleOrDefault(o => o.office_name == currentValues["office_name"]);
                    if (office != null)
                    {
                        employee.office_id = office.office_id;
                    }

                    // position_name から position_id を取得
                    var position = dbContext.Position.SingleOrDefault(p => p.position_name == currentValues["position_name"]);
                    if (position != null)
                    {
                        employee.position_id = position.position_id;
                    }

                    // データベースに変更を保存
                    dbContext.SaveChanges();
                }
            }
        }

        //クリアボタンクリック時のメソッド
        private void btnClear_Click(object sender, EventArgs e)
        {
            //元のデータをセット
            LoadEmployeeDetails(EmployeeId);

            //エラーをクリア
            errorProvider.SetError(txtKanaFirstName, string.Empty);
            errorProvider.SetError(txtKanaLastName, string.Empty);
            errorProvider.SetError(txtFirstName, string.Empty);
            errorProvider.SetError(txtLastName, string.Empty);
            errorProvider.SetError(txtMail, string.Empty);
            errorProvider.SetError(txtPhoneNum, string.Empty);
            errorProvider.SetError(selectOffice, string.Empty);
            errorProvider.SetError(selectPosition, string.Empty);
        }

        //削除ボタンクリック時のメソッド
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 確認ダイアログの表示
            DialogResult result = MessageBox.Show(
                "本当に削除しますか？（ステータスが変更されます）",
                "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // ユーザーが「Yes」を選択した場合のみ処理を実行
            if (result == DialogResult.Yes)
            {
                UpdateStatusToDeleted();
            }
        }

        //削除ロジック
        private void UpdateStatusToDeleted()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // 対象の社員情報を取得
                    var employeeId = int.Parse(EmployeeId); // ラベルから社員番号を取得

                    var employee = dbContext.Employee.SingleOrDefault(e => e.employee_id == employeeId);

                    if (employee != null)
                    {
                        // ステータスを 0 に更新
                        employee.status = 0;

                        // 変更を保存
                        dbContext.SaveChanges();

                        MessageBox.Show("削除されました。", "削除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // フォームを閉じる
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("削除が失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ステータス更新中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private ErrorProvider errorProvider = new ErrorProvider();

        private bool ValidateInput()
        {
            bool isValid = true;

            //
            // 姓（かな）のバリデーション
            //
            if (string.IsNullOrEmpty(txtKanaFirstName.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, "姓（かな）は入力必須です。");
                isValid = false;
            }
            else if (txtKanaFirstName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, "姓（かな）は25文字以内で入力してください。");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtKanaFirstName.Text, @"^[\u3041-\u3096ー]+$"))
            {
                // 平仮名以外が含まれている場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, "平仮名のみ入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtKanaFirstName, string.Empty);
            }
            //
            // 名（かな）のバリデーション
            //
            if (string.IsNullOrEmpty(txtKanaLastName.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtKanaLastName, "名（かな）は入力必須です。");
                isValid = false;
            }
            else if (txtKanaFirstName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, "名（かな）は25文字以内で入力してください。");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtKanaLastName.Text, @"^[\u3041-\u3096ー]+$"))
            {
                // 平仮名以外が含まれている場合のエラー設定
                errorProvider.SetError(txtKanaLastName, "平仮名のみ入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtKanaLastName, string.Empty);
            }
            //
            // 姓のバリデーション
            //
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtFirstName, "姓 は入力必須です。");
                isValid = false;
            }
            else if (txtFirstName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtFirstName, "姓 は25文字以内で入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtFirstName, string.Empty);
            }
            //
            // 名のバリデーション
            //
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtLastName, "名 は入力必須です。");
                isValid = false;
            }
            else if (txtLastName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtLastName, "名 は25文字以内で入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtLastName, string.Empty);
            }
            //
            // メールアドレスのバリデーション
            //
            if (string.IsNullOrEmpty(txtMail.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtMail, "メールアドレス は入力必須です。");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtMail, "有効なメールアドレスを入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtMail, string.Empty);
            }
            //
            // 電話番号のバリデーション
            //
            if (string.IsNullOrEmpty(txtPhoneNum.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(txtPhoneNum, "電話番号 は入力必須です。");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNum.Text, @"^\d{2,4}-\d{2,4}-\d{4}$"))
            {
                errorProvider.SetError(txtPhoneNum, "電話番号は「080-1234-5678」の形式で入力してください。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtPhoneNum, string.Empty);
            }
            //
            // 拠点のバリデーション
            //
            if (string.IsNullOrEmpty(selectOffice.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(selectOffice, "拠点 は入力必須です。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(selectOffice, string.Empty);
            }
            //
            // 役職のバリデーション
            //
            if (string.IsNullOrEmpty(selectPosition.Text))
            {
                // 空欄の場合のエラー設定
                errorProvider.SetError(selectPosition, "役職 は入力必須です。");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(selectPosition, string.Empty);
            }
            
            return isValid;
        }
    }
}
