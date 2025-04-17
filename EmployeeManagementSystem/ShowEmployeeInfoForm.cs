using EmployeeManagementSystem.Contexts;
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

            this.Load += ShowEmployeeInfoForm_Load; // フォームロード時にデータをロード
            this.dataGridViewEmployee.CellFormatting += dataGridViewEmployee_CellFormatting;  // CellFormattingイベントを登録
            this.dataGridViewEmployee.CellContentClick += dataGridViewEmployee_CellContentClick;
            this.btnConfirm.Click += btnConfirm_Click;
            this.btnSearch.Click += btnSearch_Click;

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
            btnClear.Click += btnClear_Click;

        }

        private void ShowEmployeeInfoForm_Load(object sender, EventArgs e)
        {
            set_conboBoxData(); //検索エリアのコンボボックスにデータセット
            LoadEmployeeData(); // データを読み込み表示
        }
        //
        //statusカラム表示変換のためのフォーマッター
        //
        private void dataGridViewEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        private void dataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        private void set_conboBoxData()
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

        private void UpdateDatabase()
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                {
                    // 新しい行はスキップ
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    // 変更された行をチェック（行の状態を手動で追跡する方法を追加することも可能）
                    bool isModified = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != cell.Tag) // Tag に元の値を保存して比較
                        {
                            isModified = true;
                            break;
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
                    if (employee != null)
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
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
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
           this. LoadEmployeeData();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 入力値を収集
            string strEmployeeId = txtEmployeeId.Text; // 社員番号
            string name = txtName.Text; // 名前
            string kanaName = txtKanaName.Text; // 名前（かな）
            string officeName = selectOffice.SelectedItem?.ToString(); // 拠点選択
            string positionName = selectPosition.SelectedItem?.ToString(); // 役職選択

            // 検索処理を実行
            SearchEmployeeData(strEmployeeId, name, kanaName, officeName, positionName);
        }


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

        private void btnClear_Click(object sender, EventArgs e)
        {
            // テキストボックスをクリア
            txtEmployeeId.Text = string.Empty; // 社員番号
            txtName.Text = string.Empty; // 名前
            txtKanaName.Text = string.Empty; // 名前（かな）

            // コンボボックスの選択をクリア
            selectOffice.SelectedIndex = -1; // 拠点
            selectPosition.SelectedIndex = -1; // 役職
        }

    }
}
