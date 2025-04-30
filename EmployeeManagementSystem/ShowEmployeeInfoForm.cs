using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeManagementSystem
{
    public partial class ShowEmployeeInfoForm : Form
    {
        //グリッドの結果が社員番号の昇順であるか否か
        bool isAscendingOrderByEmpId = false;
        bool isSearching = false;
        private LoginForm loginForm;

        public ShowEmployeeInfoForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.Resize += (s, e) =>
            {
                dataGridViewEmployee.Size = new Size(this.Width - 15, this.Height - 150);
            };
            this.Load += ShowEmployeeInfoForm_Load;
            this.dataGridViewEmployee.CellFormatting += DataGridViewEmployee_CellFormatting;
            this.dataGridViewEmployee.CellContentClick += DataGridViewEmployee_CellContentClick;
            this.btnConfirm.Click += BtnConfirm_Click;
            this.btnSearch.Click += BtnSearch_Click;
            this.dataGridViewEmployee.ColumnHeaderMouseClick += dataGridView_EmployeeIdHeaderMouseClick;
            this.FormClosing += CloseFrom;

            // 社員情報追加ボタンクリック
            btnShowEmployeeInfoDetail.Click += (s, e) =>
            {
                AddEmployeeInfoForm addEmployeeInfoForm = new AddEmployeeInfoForm(this);
                addEmployeeInfoForm.Show();
            };

            //ログアウトボタンクリック
            this.btnLogout.Click += BtnLogout_Click;

            // クリアボタンクリック
            btnClear.Click += BtnClear_Click;

        }


        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // 確認ダイアログを表示
            DialogResult result = MessageBox.Show(
                InformationMessages.INFO011_LOGOUT_CONFIRMATION,
                InformationMessages.TITLE001_CONFIRMATION,
                MessageBoxButtons.YesNo, // YesとNoボタンを表示
                MessageBoxIcon.Warning  // 警告アイコンを表示
            );

            // ダイアログの結果に基づいて処理
            if (result == DialogResult.Yes)
            {
                SessionManager.SetLogoutTime(); //ログアウト時間をDBにセット
                SessionManager.Session_Clear(); //セッション情報をクリア
                this.Close();// 現在のフォームを閉じる
                loginForm.Show();// ログインフォームを再表示
            }
        }


        //<summary>
        //社員情報一覧表示画面を表示（ロード）するとき
        //Set_conboBoxDataメソッドで検索エリアのコンボボックスに値を挿入し、
        //LoadEmployeeDataメソッドでグリッドの結果に社員情報を表示する
        //</summary>
        private void ShowEmployeeInfoForm_Load(object sender, EventArgs e)
        {
            Set_conboBoxData(); //検索エリアのコンボボックスにデータセット
            LoadEmployeeData(); // データを読み込み表示
        }

        //<summary>
        //statusカラム表示変換のためのフォーマッター
        //</summary>
        private void DataGridViewEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewEmployee.Columns[e.ColumnIndex].Name == "status") // ステータス列を特定
            {
                if (e.Value != null)
                {
                    e.Value = (int)e.Value == 1 ? "在職中" : "退職済"; // 1→在職中、0→退職済
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// 社員番号リンククリック時、その社員番号の社員情報詳細画面を表示
        /// </summary>
        private void DataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // リンクがクリックされたかを確認
            if (dataGridViewEmployee.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex >= 0)
            {
                // クリックされた行の社員番号を取得
                string employeeId = dataGridViewEmployee.Rows[e.RowIndex].Cells["employee_id"].Value?.ToString();

                if (!string.IsNullOrEmpty(employeeId))
                {
                    // ShowEmployeeInfoDetailFormを開く
                    ShowEmployeeInfoDetailForm detailForm = new ShowEmployeeInfoDetailForm(employeeId, this);
                    detailForm.Show(); // 新しいフォームを開く
                }
            }
        }

        /// <summary>
        /// 検索エリアの拠点と役職のコンボボックスに値を挿入する
        /// </summary>
        private void Set_conboBoxData()
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
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 社員情報ビューからデータを取得しグリッドの結果に表示する
        /// ステータスが退職済のレコードは編集不可にする
        /// </summary>
        private void LoadEmployeeData()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // データベースから従業員データ、拠点、役職を結合して取得
                    var employeeData = dbContext.EmployeeView
                        .Where(e => e.status == 1)
                        .OrderBy(e => e.employee_id)
                        .ToList();

                    // データグリッドに表示
                    dataGridViewEmployee.DataSource = employeeData; // dataGridViewEmployee はグリッドコントロール

                    // 各行のセルのTagを初期化および設定
                    foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Tag = cell.Value; // 現在の値をTagプロパティに設定
                        }
                    }

                    isAscendingOrderByEmpId = true;
                    //MessageBox.Show(chkRetired.Checked.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 社員情報の項目で編集内容をDBに更新する
        /// </summary>
        private void UpdateDatabase()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                //グリッド結果に変更があり処理が成功したかどうか
                bool successChanged = false;
                // 変更された行をチェック
                bool hasModified = false;

                //該当エラーメッセージリスト
                List<string> errorMessages = new List<string>();

                // office_name列のデータ取得
                var officeNames = dbContext.Office
                        .Select(o => o.office_name)
                        .ToList();
                // position_name列のデータ取得
                var positionNames = dbContext.Position
                        .Select(o => o.position_name)
                        .ToList();

                foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                {
                    // 変更された行をチェック
                    bool isModified = false;
                    //入力値チェック
                    bool isValid = true;

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        // 空白チェックをすべてのセルに対して実施
                        if (row.Cells[i].Value == null || string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()))
                        {
                            MessageBox.Show(ErrorMessages.ERR001_EMPTY_FIELD, InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // メソッドを終了
                        }
                        if (i == 1 && row.Cells[i].Value.ToString().Length > 25)
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR007_FIRST_NAME_LIMIT))
                            {
                                errorMessages.Add(ErrorMessages.ERR007_FIRST_NAME_LIMIT);
                            }
                            isValid = false;
                        }
                        if (i == 2 && row.Cells[i].Value.ToString().Length > 25)
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR009_LAST_NAME_LIMIT))
                            {
                                errorMessages.Add(ErrorMessages.ERR009_LAST_NAME_LIMIT);
                            }
                            isValid = false;
                        }
                        if (i == 3 && row.Cells[i].Value.ToString().Length > 25)
                        {
                            row.Cells[i].Style.BackColor = Color.Red;　// 背景を赤く設定
                            if (!errorMessages.Contains(ErrorMessages.ERR002_KANA_FIRST_NAME_LIMIT)) // 重複防止
                            {
                                errorMessages.Add(ErrorMessages.ERR002_KANA_FIRST_NAME_LIMIT);
                            }
                            isValid = false;
                        }
                        if (i == 3 && !System.Text.RegularExpressions.Regex.IsMatch(row.Cells[i].Value.ToString(), @"^[\u3041-\u3096ー]+$"))
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY))
                            {
                                errorMessages.Add(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY);
                            }
                            isValid = false;
                        }
                        if (i == 4 && row.Cells[i].Value.ToString().Length > 25)
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR005_KANA_LAST_NAME_LIMIT))
                            {
                                errorMessages.Add(ErrorMessages.ERR005_KANA_LAST_NAME_LIMIT);
                            }
                            isValid = false;
                        }
                        if (i == 4 && !System.Text.RegularExpressions.Regex.IsMatch(row.Cells[i].Value.ToString(), @"^[\u3041-\u3096ー]+$"))
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY))
                            {
                                errorMessages.Add(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY);
                            }
                            isValid = false;
                        }
                        if (i == 5 && !System.Text.RegularExpressions.Regex.IsMatch(row.Cells[i].Value.ToString(), @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR012_REQUIRED_VALID_MAIL))
                            {
                                errorMessages.Add(ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
                            }
                            isValid = false;
                        }
                        if (i == 6 && !System.Text.RegularExpressions.Regex.IsMatch(row.Cells[i].Value.ToString(), @"^\d{2,4}-\d{2,4}-\d{4}$"))
                        {
                            MessageBox.Show(row.Cells[5].Value.ToString(), InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR014_REQUIRED_VALID_PHONE_NUM))
                            {
                                errorMessages.Add(ErrorMessages.ERR014_REQUIRED_VALID_PHONE_NUM);
                            }
                            isValid = false;
                        }
                        if (i == 8 && !officeNames.Contains(row.Cells[i].Value.ToString()))
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR016_LOCATION_NAME_INCORRECT))
                            {
                                errorMessages.Add(ErrorMessages.ERR016_LOCATION_NAME_INCORRECT);
                            }
                            isValid = false;
                        }
                        if (i == 9 && !positionNames.Contains(row.Cells[i].Value.ToString()))
                        {
                            row.Cells[i].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR018_POSITION_NAME_INCORRECT))
                            {
                                errorMessages.Add(ErrorMessages.ERR018_POSITION_NAME_INCORRECT);
                            }
                            isValid = false;
                        }
                        if (!row.Cells[i].Value.Equals(row.Cells[i].Tag))
                        {
                            // MessageBox.Show("row.Cells[i].Value;"+ $"{row.Cells[i].Value}" + "row.Cells[i].Tag" + $"{row.Cells[i].Tag}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //MessageBox.Show("isModified" + $"{isModified}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            isModified = true;
                            hasModified = true;
                        }
                    }

                    if (!isModified)
                    {
                        continue;
                    }

                    // 変更内容を収集
                    int employee_id = Convert.ToInt32(row.Cells["employee_id"].Value);
                    string first_name = row.Cells["first_name"].Value.ToString();
                    string last_name = row.Cells["last_name"].Value.ToString();
                    string kana_first_name = row.Cells["kana_first_name"].Value.ToString();
                    string kana_last_name = row.Cells["kana_last_name"].Value.ToString();
                    string mail = row.Cells["mail"].Value.ToString();
                    string phone_num = row.Cells["phone_num"].Value.ToString();
                    string office_name = row.Cells["office_name"].Value.ToString();
                    string position_name = row.Cells["position_name"].Value.ToString();


                    // データベースの更新
                    var employee = dbContext.Employee.SingleOrDefault(e => e.employee_id == employee_id);
                    if (employee != null && isValid)
                    {
                        employee.first_name = first_name;
                        employee.last_name = last_name;
                        employee.kana_first_name = kana_first_name;
                        employee.kana_last_name = kana_last_name;
                        employee.mail = mail;
                        employee.phone_num = phone_num;

                        // office_name をもとに office_id を取得して更新
                        var office = dbContext.Office.SingleOrDefault(o => o.office_name == office_name);
                        employee.office_id = office.office_id;


                        // position_name をもとに position_id を取得して更新
                        var position = dbContext.Position.SingleOrDefault(p => p.position_name == position_name);
                        employee.position_id = position.position_id;

                        // 更新を保存
                        dbContext.SaveChanges();
                        LoadEmployeeData();
                        successChanged = true;

                    }
                    else
                    {
                        successChanged = false;
                    }
                }
                if (!hasModified)
                {
                    errorMessages = null;
                    txtErrorMessages.Visible = false;
                    MessageBox.Show(InformationMessages.INFO008_NOTIFY_NO_CHANGE, InformationMessages.TITLE007_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (successChanged)
                {
                    MessageBox.Show(InformationMessages.INFO007_UPDATE_SUCCESS, InformationMessages.TITLE006_UPDATED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!successChanged)
                {
                    DisplayErrorMessages(errorMessages);
                    MessageBox.Show(ErrorMessages.ERR032_COULD_NOT_UPDATE, InformationMessages.TITLE008_UPDATE_FAILED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 確定ボタンクリックで、更新処理であるUpdateDatabaseを実行し、成功したかどうかメッセージで知らせる
        /// </summary>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {

            // 確認ダイアログを表示
            DialogResult result = MessageBox.Show(
                InformationMessages.INFO006_UPDATE_CONFIRMATION,
                InformationMessages.TITLE001_CONFIRMATION,
                MessageBoxButtons.YesNo, // YesとNoボタンを表示
                MessageBoxIcon.Warning  // 警告アイコンを表示
            );

            // ダイアログの結果に基づいて処理
            if (result == DialogResult.Yes)
            {
                try
                {
                    // UpdateDatabaseメソッドを呼び出してデータを保存
                    UpdateDatabase();

                }
                catch (Exception ex)
                {
                    // エラーメッセージを表示
                    MessageBox.Show($"{ErrorMessages.ERR031_DATA_UPDATE_ERROR} { ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }  
        }

        /// <summary>
        /// 社員情報詳細画面や社員情報追加画面を閉じたとき、社員情報一覧表示画面を更新して表示する（このメソッドは他のFormで使用される）
        /// </summary>
        internal void CheckIfFormClosed()
        {
            this.LoadEmployeeData();

        }

        /// <summary>
        /// 検索エリアの入力値を収集してSearchEmployeeDataメソッドを実行
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            txtErrorMessages.Visible = false; //更新時のエラーがある状態で検索ボタン押下したときエラーメッセージを非表示化
            dataGridViewEmployee.Location = new Point(0, 220);

            char[] delimiters = new char[] { ' ', ',' };
            List<String> strEmployeeId = txtEmployeeId.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList(); // 社員番号
            bool isCheckedRetired = chkRetired.Checked;
            string name = txtName.Text; // 名前
            string kanaName = txtKanaName.Text; // 名前（かな）
            string officeName = selectOffice.SelectedItem?.ToString(); // 拠点選択
            string positionName = selectPosition.SelectedItem?.ToString(); // 役職選択

            List<int> employeeId = new List<int>();
            try
            {
                foreach (var item in strEmployeeId)
                {
                    employeeId.Add(int.Parse(item));
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show($"{ErrorMessages.ERR024_REQUIRED_VALID_EMPLOYEE_IDS}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OverflowException oe)
            {
                MessageBox.Show($"{ErrorMessages.ERR025_OVERFLOW_NUMBER}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                //他エラーはBtnSearch_Clickメソッド処理を終了させる
                return;
            }

            if(CheckIsSearching(isCheckedRetired, employeeId, name, kanaName, officeName, positionName))
            {
                // 検索処理を実行
                SearchEmployeeData(isCheckedRetired, employeeId, name, kanaName, officeName, positionName);
            }
            else
            {
                LoadEmployeeData();
            }

            
        }

        /// <summary>
        /// 検索条件入力エリアのパラメーターを検索条件として、社員情報ビューから検索し結果を表示する
        /// </summary>
        private void SearchEmployeeData(bool isCheckedRetired, List<int> employeeId, string name, string kanaName, string officeName, string positionName)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // ベースクエリ
                    var query = dbContext.EmployeeView.AsQueryable();
                    if (isCheckedRetired)
                    {
                        query = query.Where(e => e.status == 0);
                    }

                    // 社員番号
                    if (employeeId.Any())
                    {
                        query = query.Where(e => employeeId.Contains(e.employee_id));
                    }

                    // 名前（かな）
                    if (!string.IsNullOrEmpty(kanaName))
                    {
                        query = query.Where(e => e.kana_first_name.Contains(kanaName) || e.kana_last_name.Contains(kanaName));
                    }

                    // 名前
                    if (!string.IsNullOrEmpty(name))
                    {
                        query = query.Where(e => e.first_name.Contains(name) || e.last_name.Contains(name));
                    }

                    // 拠点
                    if (!string.IsNullOrEmpty(officeName))
                    {
                        query = query.Where(e => e.office_name == officeName);
                    }

                    // 役職
                    if (!string.IsNullOrEmpty(positionName))
                    {
                        query = query.Where(e => e.position_name == positionName);
                    }

                    // クエリを実行して結果を取得
                    var results = query.OrderBy(e => e.employee_id).ToList();

                    // グリッドに結果を表示
                    dataGridViewEmployee.DataSource = results;

                    // 各行のセルのTagを初期化および設定
                    foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Tag = cell.Value; // 現在の値をTagプロパティに設定
                        }

                        // "status"列の確認
                        var statusCell = row.Cells["status"];
                        if (statusCell.Value != null && int.TryParse(statusCell.Value.ToString(), out int status) && status == 0)
                        {
                            row.ReadOnly = true; // 退職済の場合は行を読み取り専用に設定
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorMessages.ERR020_ERROR_DURING_SEARCH} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// クリアボタンクリックで検索条件すべてをリセットするメソッド
        /// </summary>
        /// <param name="sender">クリアボタンがクリックされたコントロール</param>
        /// <param name="e">クリックイベントのデータ</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            SearchAreaClear();
        }

        internal void SearchAreaClear()
        {
            // テキストボックスをクリア
            txtEmployeeId.Text = string.Empty; // 社員番号
            txtName.Text = string.Empty; // 名前
            txtKanaName.Text = string.Empty; // 名前（かな）

            // コンボボックスの選択をクリア
            selectOffice.SelectedIndex = -1; // 拠点
            selectPosition.SelectedIndex = -1; // 役職

            // チェックボックスのチェックをクリア
            chkRetired.Checked = false;
        }

        /// <summary>
        /// エラーメッセージ表示メソッド
        /// </summary>
        private void DisplayErrorMessages(List<string> errorMessages)
        {
            if (errorMessages.Any())
            {
                // エラーメッセージを改行で区切って結合
                txtErrorMessages.Text = string.Join(Environment.NewLine, errorMessages);
                txtErrorMessages.Height = TextRenderer.MeasureText(txtErrorMessages.Text, txtErrorMessages.Font, txtErrorMessages.ClientSize, TextFormatFlags.WordBreak).Height;

                // グリッドの位置をテキストボックスの位置とサイズに応じて調整
                dataGridViewEmployee.Top = txtErrorMessages.Bottom + 10;

                // テキストボックスを可視化
                txtErrorMessages.Visible = true;
            }
            else
            {
                // エラーメッセージがない場合はテキストボックスをクリア
                txtErrorMessages.Text = string.Empty;

                // 非表示にする（必要に応じて設定）
                txtErrorMessages.Visible = false;
            }
        }

        /// <summary>
        /// グリッドの社員番号のヘッダークリックでその項目の昇順、または降順のソートを行うメソッド
        /// </summary>
        private void dataGridView_EmployeeIdHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                if (!isSearching)
                {
                    if (isAscendingOrderByEmpId)
                    {
                        try
                        {
                            // データベースから従業員データ、拠点、役職を結合して取得 (社員番号　降順）
                            var employeeData = dbContext.EmployeeView
                                .Where(e => e.status != 0)
                                .OrderByDescending(e => e.employee_id)
                                .ToList();

                            // データグリッドに表示
                            dataGridViewEmployee.DataSource = employeeData; // dataGridViewEmployee はグリッドコントロール

                            // 各行のセルのTagを初期化および設定
                            foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    cell.Tag = cell.Value; // 現在の値をTagプロパティに設定
                                }
                            }

                            isAscendingOrderByEmpId = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (!isAscendingOrderByEmpId)
                    {
                        LoadEmployeeData();
                    }
                }
                return;
            }
        }

        /// <summary>
        /// ログアウトボタン以外でフォームを閉じたときのセッションの削除とログインフォームの表示メソッド
        /// </summary>
        private void CloseFrom(object sender, EventArgs e)
        {
            SessionManager.SetLogoutTime(); //ログアウト時間をDBにセット
            SessionManager.Session_Clear(); //セッション情報をクリア
            loginForm.Show();// ログインフォームを再表示
        }

        private bool CheckIsSearching(bool isCheckedRetired, List<int> employeeId, string name, string kanaName, string officeName, string positionName)
        {
            if (isCheckedRetired)
            {
                isSearching = true;
            }
            else
            {
                isSearching = false;
            }

            // 社員番号
            if (employeeId.Any())
            {
                isSearching = true;
            }

            // 名前（かな）
            if (!string.IsNullOrEmpty(kanaName))
            {
                isSearching = true;
            }

            // 名前
            if (!string.IsNullOrEmpty(name))
            {
                isSearching = true;
            }

            // 拠点
            if (!string.IsNullOrEmpty(officeName))
            {
                isSearching = true;
            }

            // 役職
            if (!string.IsNullOrEmpty(positionName))
            {
                isSearching = true;
            }

            return isSearching;
        }
    }
}
