using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.Utils;
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
        private string EmployeeId;　//表示したい社員の社員番号
        private ShowEmployeeInfoForm parentForm; //親フォーム
        private bool isRetired; //退職済みかどうか


        /// <summary>
        /// 社員情報詳細画面のイベント
        /// </summary>
        /// <param name="employeeId">詳細を表示したい社員番号</param>
        /// <param name="parentForm">ShowEmployeeInfoFormクラス型オブジェクト（親フォームデータ）</param>
        public ShowEmployeeInfoDetailForm(string employeeId, ShowEmployeeInfoForm parentForm)
        {
            InitializeComponent();
            this.EmployeeId = employeeId;

            //フォームロード時にデータを表示
            this.Load += ShowEmployeeInfoDetailForm_Load;

            //更新ボタンクリック
            this.btnUpdate.Click += BtnUpdate_Click;

            //クリアボタンクリック
            this.btnClear.Click += BtnClear_Click;

            //削除ボタンクリック
            this.btnDelete.Click += BtnDelete_Click;

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

                // ダイアログのYesを選択したか
                if (result == DialogResult.Yes)
                {
                    // キャンセル処理を実行
                    this.Close(); // フォームを閉じる
                }
            };
        }


        /// <summary>
        /// フォームを閉じた後、社員情報一覧画面のグリッドの結果を更新する
        /// </summary>
        /// /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        private void ShowEmployeeInfoDetailForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //親フォームがnullではないか
            if (parentForm != null)
            {
                //親フォームの検索エリアの入力や選択をクリア
                parentForm.SearchAreaClear();
                //社員情報一覧画面をロード
                parentForm.CheckIfFormClosed();
            }
        }

  
        // 初期値を保存するディクショナリ
        private Dictionary<string, string> initialValues = new Dictionary<string, string>();

        /// <summary>
        /// もし詳細表示したい社員が在職中の場合、詳細表示したい社員の情報を画面にセット、社員情報詳細画面のコンボボックスにデータを挿入、画面にセットされた情報の初期値をinitialValuesにセット（編集されたかどうか判定のため）<br/>
        /// 退職済みの場合、画面に情報をセットして内容を編集できないようにする
        /// </summary>
        private void ShowEmployeeInfoDetailForm_Load(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //拠点と役職のコンボボックスにデータ挿入
            SetConboBoxData();
            //コンボボックスデータ以外のデータを挿入
            LoadEmployeeDetails(EmployeeId);


            //退職済の社員の情報はテキストボックスを読み取り専用に、拠点と役職のコンボボックス、クリア、削除、更新ボタンは非活性にする
            if (isRetired) 
            {
                txtKanaFirstName.ReadOnly = true;
                txtKanaLastName.ReadOnly = true;
                txtFirstName.ReadOnly = true;
                txtLastName.ReadOnly = true;
                txtMail.ReadOnly = true;
                txtPhoneNum.ReadOnly = true;
                selectOffice.Enabled = false;
                selectPosition.Enabled = false;
                btnClear.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                notifyEmployeeRetired.Text = InformationMessages.INFO001_NOTIFY_EMPLOYEE_RETIRED;
                notifyEmployeeRetired.Visible = true;
            }
            else
            {
                

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
        }

        /// <summary>
        /// 詳細表示したい社員の社員番号を引数で受け取り、表示に必要なデータをDBから取得し、画面にセット、
        /// クラス内のisRetiredの真偽のセットを行う
        /// </summary>
        /// <param name="employeeId">詳細表示したい社員番号</param>
        private void LoadEmployeeDetails(string employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                // Employee、Office、Positionテーブルを結合し必要なデータを取得するクエリ
                // (検索条件はEmployeeテーブルの社員番号と詳細表示したい社員番号が等しいもの）
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
                                        p.position_name,
                                        e.status
                                    }).FirstOrDefault();
                //検索結果がnullではないか
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

                    //詳細表示したい社員のstatusが0（退職済)であるか
                    if(employeeData.status == 0)
                    {
                        isRetired = true;
                    }
                    else
                    {
                        isRetired = false;
                    }
                }
                else
                {
                    MessageBox.Show(ErrorMessages.ERR029_NOT_FIND_EMPLOYEE, InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }


        /// <summary>
        /// 拠点と役職の項目をコンボボックスにセット
        /// </summary>
        private void SetConboBoxData()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    //拠点コンボボックス内の項目設定
                    var officeNames = dbContext.Office // OfficeテーブルへのDbSet
                        .Select(o => o.office_name) // office_name列を選択
                        .ToList();

                    //コンボボックスをクリア
                    selectOffice.Items.Clear();

                    // コンボボックスに選択肢を設定
                    foreach (var name in officeNames)
                    {
                        selectOffice.Items.Add(name);
                    }


                    //役職コンボボックス内の項目設定
                    var positionNames = dbContext.Position // PositionテーブルへのDbSet
                        .Select(o => o.position_name) // position_name列を選択
                        .ToList();

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


        /// <summary>
        /// 更新ボタンクリックイベント。<br/>
        /// 変更前のデータと、更新ボタンクリック時のデータを比較し、変更があった場合入力チェックを行う。<br/>
        /// 入力チェックが成功したら、確認ダイアログを表示し、ユーザーがYesを選択すると変更内容がDBに登録される
        /// </summary>
        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 更新ボタンクリック時の値を収集
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

            // 変更があるかチェック (initialValues:変更前のデータ　currentValues:更新ボタン押下時のデータ）
            bool hasChanges = initialValues.Any(iv => iv.Value != currentValues[iv.Key]);

            //変更があるか
            if (hasChanges)
            {
                // 入力内容を検証 (ValidateInput が false の場合メッセージを表示し処理を終了させる)
                if (!ValidateInput())
                {
                    MessageBox.Show(ErrorMessages.ERR026_HAVING_INPUT_ERROR, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // 確認ダイアログを表示
                DialogResult result = MessageBox.Show(
                    InformationMessages.INFO006_UPDATE_CONFIRMATION,
                    InformationMessages.TITLE001_CONFIRMATION,
                    MessageBoxButtons.YesNo, // YesとNoボタンを表示
                    MessageBoxIcon.Warning  // 警告アイコンを表示
                );

                // Yesを選択したか
                if (result == DialogResult.Yes)
                {
                    // 更新処理を実行
                    UpdateDatabase(currentValues);

                    // 初期値を更新
                    initialValues = new Dictionary<string, string>(currentValues);

                    MessageBox.Show(InformationMessages.INFO007_UPDATE_SUCCESS, InformationMessages.TITLE006_UPDATED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // 更新ボタンを押したが変更がない場合メッセージ表示
                MessageBox.Show(InformationMessages.INFO008_NOTIFY_NO_CHANGE, InformationMessages.TITLE007_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 確定ボタン押下時の社員情報詳細画面のデータを対象社員のレコードに反映させてDBを更新する
        /// </summary>
        /// <param name="currentValues">確定ボタン押下時の社員情報詳細画面のデータ</param>
        private void UpdateDatabase(Dictionary<string, string> currentValues)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                // EmployeeId を使って対象社員のデータを取得
                int employeeId = int.Parse(EmployeeId);
                var employee = dbContext.Employee.SingleOrDefault(e => e.employee_id == employeeId);

                //対象社員のデータがnullではないか
                if (employee != null)
                {
                    // DBの社員情報を更新
                    employee.kana_first_name = currentValues["kana_first_name"];
                    employee.kana_last_name = currentValues["kana_last_name"];
                    employee.first_name = currentValues["first_name"];
                    employee.last_name = currentValues["last_name"];
                    employee.mail = currentValues["mail"];
                    employee.phone_num = currentValues["phone_num"];

                    // office_name から office_id を取得しEmployeeテーブルのoffice_idを更新
                    var office = dbContext.Office.SingleOrDefault(o => o.office_name == currentValues["office_name"]);
                    if (office != null)
                    {
                        employee.office_id = office.office_id;
                    }

                    // position_name から position_id を取得しEmployeeテーブルのposition_idを更新
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


        /// <summary>
        /// クリアボタンクリック時のメソッド
        /// </summary>
        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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


        /// <summary>
        /// 削除ボタンクリック時のメソッド。<br/>
        /// 対象社員のEmployeeテーブルのstatusを1(在職中)から0(退職済)に変更する（対象社員レコードの論理削除）
        /// </summary>
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                MessageBox.Show($"{ErrorMessages.ERR033_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 確認ダイアログの表示
            DialogResult result = MessageBox.Show(
                InformationMessages.INFO004_DELETE_CONFIRMATION,
                InformationMessages.TITLE001_CONFIRMATION,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // ユーザーが「Yes」を選択したか
            if (result == DialogResult.Yes)
            {
                //対象社員のstatusを0（退職済）に変更
                UpdateStatusToDeleted();
            }
        }

        /// <summary>
        /// 対象社員のEmployeeテーブルのstatusを0に変更するメソッド（社員情報の論理削除）
        /// </summary>
        private void UpdateStatusToDeleted()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // 対象の社員の社員番号を数字に変換し変数employeeIdに代入
                    var employeeId = int.Parse(EmployeeId);

                    //DBから取得した対象社員の社員のデータ
                    var employee = dbContext.Employee.SingleOrDefault(e => e.employee_id == employeeId);

                    //対象社員のデータがnullではないか
                    if (employee != null)
                    {
                        // ステータスを 0 に更新
                        employee.status = 0;

                        // 変更を保存
                        dbContext.SaveChanges();

                        MessageBox.Show(InformationMessages.INFO005_DELETE_SUCCESS, InformationMessages.TITLE005_DELETED, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // フォームを閉じる
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.ERR028_DELETE_FAILED, InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorMessages.ERR027_STATUS_UPDATE_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //入力エラーを通知するためのコンポーネント
        private ErrorProvider errorProvider = new ErrorProvider();

        /// <summary>
        /// 画面のプロパティの値を入力チェックするメソッド
        /// </summary>
        /// <returns>バリデーション成功(true)か失敗か(false)</returns>
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
                //対象プロパティのバリデーション成功している場合、エラー表示解除（エラーメッセージを空にする）
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
            
            return isValid;
        }
    }
}
