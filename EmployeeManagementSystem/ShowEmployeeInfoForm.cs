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

namespace EmployeeManagementSystem
{
    public partial class ShowEmployeeInfoForm : Form
    {
        public ShowEmployeeInfoForm()
        {
            InitializeComponent();

            this.Load += ShowEmployeeInfoForm_Load;
            this.dataGridViewEmployee.CellFormatting += DataGridViewEmployee_CellFormatting;
            this.dataGridViewEmployee.CellContentClick += DataGridViewEmployee_CellContentClick;
            this.btnConfirm.Click += BtnConfirm_Click;
            this.btnSearch.Click += BtnSearch_Click;

            // 社員情報追加ボタンクリック
            btnShowEmployeeInfoDetail.Click += (s, e) =>
            {
                AddEmployeeInfoForm addEmployeeInfoForm = new AddEmployeeInfoForm(this);
                addEmployeeInfoForm.Show();
            };

            //ログアウトボタンクリック
            btnLogout.Click += (s, e) =>
            {
                // 現在のフォームを閉じる
                this.Hide();

                // ログインフォームを表示
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            };

            // クリアボタンクリック
            btnClear.Click += BtnClear_Click;

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
        /// 検索エリアの拠点と役職のコンボボックスに値を挿入する
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
                    MessageBox.Show($"データの読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        .OrderBy(e => e.employee_id)
                        .ToList();

                    // データグリッドに表示
                    dataGridViewEmployee.DataSource = employeeData; // dataGridViewEmployee はグリッドコントロール

                    // 各行のステータスを確認し、退職済の場合は読み取り専用に設定
                    foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                    {
                        var statusCell = row.Cells["status"]; // "status" 列を取得
                        if (statusCell.Value != null && int.TryParse(statusCell.Value.ToString(), out int status) && status == 0)
                        {
                            row.ReadOnly = true; // 退職済の場合は行を読み取り専用に設定
                        }
                    }
;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"データの読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //入力値チェック
                bool isValid = true;
                //該当エラーメッセージリスト
                List<string> errorMessages = new List<string>();

                foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                {

                    // 変更された行をチェック
                    bool isModified = false;
                    


                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        int i=1;
                        // 空白チェックをすべてのセルに対して実施
                        if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                        {
                            //MessageBox.Show($"セル値: {cell.Value} - 型: {cell.Value?.GetType()}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(ErrorMessages.ERR001_EMPTY_FIELD, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // メソッドを終了
                        }
                        else if(row.Cells["kana_first_name"].Value.ToString().Length > 25)
                        {
                            row.Cells["kana_first_name"].Style.BackColor = Color.Red;　// 背景を赤く設定
                            if (!errorMessages.Contains(ErrorMessages.ERR002_KANA_LAST_NAME_LIMIT)) // 重複防止
                            {
                                errorMessages.Add(ErrorMessages.ERR002_KANA_LAST_NAME_LIMIT);
                            }
                            isValid = false;
                            MessageBox.Show($"{i++}");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(row.Cells["kana_first_name"].Value.ToString(), @"^[\u3041-\u3096ー]+$"))
                        {
                            row.Cells["kana_first_name"].Style.BackColor = Color.Red;
                            if (!errorMessages.Contains(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY))
                            {
                                errorMessages.Add(ErrorMessages.ERR003_INPUT_HIRAGANA_ONLY);
                            }
                            isValid = false;
                        }
                        else if (cell.Value != cell.Tag)
                        {
                            isModified = true;
                        }
                    }

                    if (!isModified && !isValid)
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
                        if (office != null)
                        {
                            employee.office_id = office.office_id;
                        }
                        else
                        {
                            MessageBox.Show($"拠点名 '{office_name}' が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        // position_name をもとに position_id を取得して更新
                        var position = dbContext.Position.SingleOrDefault(p => p.position_name == position_name);
                        if (position != null)
                        {
                            employee.position_id = position.position_id;
                        }
                        else
                        {
                            MessageBox.Show($"役職名 '{position_name}' が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        // 更新を保存
                        dbContext.SaveChanges();

                    }  
                }
                if (!isValid)
                {
                    DisplayErrorMessages(errorMessages);
                    //MessageBox.Show($"{string.Join(Environment.NewLine, errorMessages)}", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorMessages = null;
                    txtErrorMessages.Visible = false;
                }
            }
        }

        /// <summary>
        /// 確定ボタンクリックで、更新処理であるUpdateDatabaseを実行し、成功したかどうかメッセージで知らせる
        /// </summary>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {


            try
            {
                // UpdateDatabaseメソッドを呼び出してデータを保存
                UpdateDatabase();

                // ユーザーへの通知
                MessageBox.Show("データが更新されました！", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // エラーメッセージを表示
                MessageBox.Show($"更新中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void CheckIfFormClosed()
        {

            // 必要な処理を実行 (例: グリッドを更新する)
            this.LoadEmployeeData();

        }

        /// <summary>
        /// 検索エリアの入力値を収集してSearchEmployeeDataメソッドを実行
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string strEmployeeId = txtEmployeeId.Text; // 社員番号
            string name = txtName.Text; // 名前
            string kanaName = txtKanaName.Text; // 名前（かな）
            string officeName = selectOffice.SelectedItem?.ToString(); // 拠点選択
            string positionName = selectPosition.SelectedItem?.ToString(); // 役職選択

            // 検索処理を実行
            SearchEmployeeData(strEmployeeId, name, kanaName, officeName, positionName);
        }

        /// <summary>
        /// 検索条件入力エリアのパラメーターを検索条件として、社員情報ビューから検索し結果を表示する
        /// </summary>
        private void SearchEmployeeData(string strEmployeeId, string name, string kanaName, string officeName, string positionName)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // ベースクエリ
                    var query = dbContext.EmployeeView.AsQueryable();

                    // 社員番号
                    if (!string.IsNullOrEmpty(strEmployeeId))
                    {
                        int employeeId = int.Parse(strEmployeeId);
                        query = query.Where(e => e.employee_id == employeeId);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // テキストボックスをクリア
            txtEmployeeId.Text = string.Empty; // 社員番号
            txtName.Text = string.Empty; // 名前
            txtKanaName.Text = string.Empty; // 名前（かな）

            // コンボボックスの選択をクリア
            selectOffice.SelectedIndex = -1; // 拠点
            selectPosition.SelectedIndex = -1; // 役職
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
    }
}
