using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.DataModel;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem
{
    public partial class AddEmployeeInfoForm : Form
    {
        private ShowEmployeeInfoForm parentForm;
        public AddEmployeeInfoForm(ShowEmployeeInfoForm parentForm)
        {
            InitializeComponent();
            this.ClientSize = new Size(1200, 800); // フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // サイズ変更を無効化
            this.MaximizeBox = false; // 最大化ボタンを無効化
            this.StartPosition = FormStartPosition.CenterScreen; // 画面中央に表示
            
            //拠点と役職の項目をコンボボックスにセット
            this.Load += setConboBoxData;


            //追加ボタンクリック
            this.btnAdd.Click += btnAdd_Click;

            // クリアボタンクリック
            btnClear.Click += btnClear_Click;



            // FormClosing イベントで社員情報一覧画面のメソッドを呼び出し
            this.parentForm = parentForm;  // 親フォームを保存
            this.FormClosing += ShowEmployeeInfoDetailForm_FormClosing;

            // キャンセルリンククリックイベント
            linkCancel.Click += (s, e) =>
            {
                // 確認ダイアログを表示
                DialogResult result = MessageBox.Show(
                    InformationMessages.INFO003_CANCEL_CONFIRMATION,
                    InformationMessages.TITLE001_CONFIRMATION,
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
                errorProvider.SetError(txtKanaFirstName, ErrorMessages.ERR004_MISSING_KANA_FIRST_NAME);
                isValid = false;
            }
            else if (txtKanaFirstName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, ErrorMessages.ERR002_KANA_FIRST_NAME_LIMIT);
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtKanaFirstName.Text, @"^[\u3041-\u3096ー]+$"))
            {
                // 平仮名以外が含まれている場合のエラー設定
                errorProvider.SetError(txtKanaFirstName, ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY);
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
                errorProvider.SetError(txtKanaLastName, ErrorMessages.ERR006_MISSING_KANA_LAST_NAME);
                isValid = false;
            }
            else if (txtKanaLastName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtKanaLastName, ErrorMessages.ERR005_KANA_LAST_NAME_LIMIT);
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtKanaLastName.Text, @"^[\u3041-\u3096ー]+$"))
            {
                // 平仮名以外が含まれている場合のエラー設定
                errorProvider.SetError(txtKanaLastName, ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY);
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
                errorProvider.SetError(txtFirstName, ErrorMessages.ERR008_MISSING_FIRST_NAME);
                isValid = false;
            }
            else if (txtFirstName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtFirstName, ErrorMessages.ERR007_FIRST_NAME_LIMIT);
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
                errorProvider.SetError(txtLastName, ErrorMessages.ERR010_MISSING_LAST_NAME);
                isValid = false;
            }
            else if (txtLastName.Text.Length > 25)
            {
                // 文字数超過の場合のエラー設定
                errorProvider.SetError(txtLastName, ErrorMessages.ERR009_LAST_NAME_LIMIT);
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
                errorProvider.SetError(txtMail, ErrorMessages.ERR011_MISSING_MAIL);
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtMail.Text, @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errorProvider.SetError(txtMail, ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
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
                errorProvider.SetError(txtPhoneNum, ErrorMessages.ERR013_MISSING_PHONE_NUM);
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNum.Text, @"^\d{2,4}-\d{2,4}-\d{4}$"))
            {
                errorProvider.SetError(txtPhoneNum, ErrorMessages.ERR014_REQUIRED_VALID_PHONE_NUM);
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
                errorProvider.SetError(selectOffice, ErrorMessages.ERR015_MISSING_OFFICE);
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
                errorProvider.SetError(selectPosition, ErrorMessages.ERR017_MISSING_POSITION);
                isValid = false;
            }
            else
            {
                errorProvider.SetError(selectPosition, string.Empty);
            }
            //
            // 雇用日のバリデーション
            //
            // 
            DateTime selectedDate = selectHireDate.Value;
            // 日付が未来日かどうかチェック
            if (selectedDate >= DateTime.Now)
            {
                // エラーメッセージとエラーアイコンを設定
                errorProvider.SetError(selectHireDate, ErrorMessages.ERR021_DATE_MUST_BE_PAST_OR_PRESENT);
                isValid = false;
            }
            else
            {
                // エラーをクリア
                errorProvider.SetError(selectHireDate, string.Empty);
            }

            return isValid;
        }



        //フォームを閉じた後、社員情報一覧画面のグリッドの結果を更新する
        private void ShowEmployeeInfoDetailForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (parentForm != null)
            {
                // 社員情報一覧画面のメソッドを呼び出す
                parentForm.CheckIfFormClosed();
            }
        }

        //拠点と役職の項目をコンボボックスにセット
        private void setConboBoxData(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dbContext = new EmployeeManagementSystemContext())
            {
                // 入力内容を検証
                if (!ValidateInput())
                {
                    MessageBox.Show(ErrorMessages.ERR026_HAVING_INPUT_ERROR, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 確認ダイアログを表示
                DialogResult result = MessageBox.Show(
                    InformationMessages.INFO009_ADD_CONFIRMATION,
                    InformationMessages.TITLE001_CONFIRMATION,
                    MessageBoxButtons.YesNo, // YesとNoボタンを表示
                    MessageBoxIcon.Warning  // 警告アイコンを表示
                );

                // ダイアログの結果に基づいて処理
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Office ID を取得
                        var selectedOfficeName = selectOffice.SelectedItem?.ToString();
                        var officeId = dbContext.Office
                            .Where(o => o.office_name == selectedOfficeName)
                            .Select(o => o.office_id)
                            .FirstOrDefault();

                        // Position ID を取得
                        var selectedPositionName = selectPosition.SelectedItem?.ToString();
                        var positionId = dbContext.Position
                            .Where(p => p.position_name == selectedPositionName)
                            .Select(p => p.position_id)
                            .FirstOrDefault();

                        // 新しい社員情報を作成
                        var newEmployee = new EmployeeModel
                        {
                            kana_first_name = txtKanaFirstName.Text,
                            kana_last_name = txtKanaLastName.Text,
                            first_name = txtFirstName.Text,
                            last_name = txtLastName.Text,
                            mail = txtMail.Text,
                            phone_num = txtPhoneNum.Text,
                            hire_date = selectHireDate.Value, // DateTimePicker から日付を取得
                            office_id = officeId,
                            position_id = positionId,
                            status = 1 //在職中で登録する
                        };

                        // データベースに追加
                        dbContext.Employee.Add(newEmployee);
                        dbContext.SaveChanges();

                        MessageBox.Show(InformationMessages.INFO010_SAVE_SUCCESS, InformationMessages.TITLE009_SAVED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ErrorMessages.ERR030_DATA_SAVE_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }                    
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // テキストボックスをクリア
            txtKanaFirstName.Text = string.Empty; // 姓（かな）
            txtKanaLastName.Text = string.Empty; // 名（かな）
            txtFirstName.Text = string.Empty; // 姓
            txtLastName.Text = string.Empty; // 名
            txtMail.Text = string.Empty; // メールアドレス
            txtPhoneNum.Text = string.Empty; // 電話番号
            selectHireDate.Value = DateTime.Now; //雇用日 現在の日付を設定

            // コンボボックスの選択をクリア
            selectOffice.SelectedIndex = -1; // 拠点
            selectPosition.SelectedIndex = -1; // 役職

            // エラーをクリア
            errorProvider.SetError(txtKanaFirstName, string.Empty);
            errorProvider.SetError(txtKanaLastName, string.Empty);
            errorProvider.SetError(txtFirstName, string.Empty);
            errorProvider.SetError(txtLastName, string.Empty);
            errorProvider.SetError(txtMail, string.Empty);
            errorProvider.SetError(txtPhoneNum, string.Empty);
            errorProvider.SetError(selectHireDate, string.Empty);
            errorProvider.SetError(selectOffice, string.Empty);
            errorProvider.SetError(selectPosition, string.Empty);
        }
    }
}