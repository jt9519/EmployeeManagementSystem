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
                    ShowEmployeeInfoDetailForm detailForm = new ShowEmployeeInfoDetailForm(employeeId);
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
                    var employeeData = dbContext.Employee
                        .Join(dbContext.Office,
                              e => e.office_id,
                              o => o.office_id,
                              (e, o) => new { Employee = e, OfficeName = o.office_name }) // 拠点名を追加
                        .Join(dbContext.Position,
                              eo => eo.Employee.position_id,
                              p => p.position_id,
                              (eo, p) => new
                              {
                                  employee_id = eo.Employee.employee_id,
                                  first_name = eo.Employee.first_name,
                                  last_name = eo.Employee.last_name,
                                  kana_first_name = eo.Employee.kana_first_name,
                                  kana_last_name = eo.Employee.kana_last_name,
                                  mail = eo.Employee.mail,
                                  phone_num = eo.Employee.phone_num,
                                  hire_date = eo.Employee.hire_date,
                                  OfficeName = eo.OfficeName,
                                  position_name = p.position_name,
                                  status = eo.Employee.status
                              })
                        .OrderBy(e => e.employee_id) // ここでemployee_idを昇順に並び替え
                        .ToList();

                    // データグリッドに表示
                    dataGridViewEmployee.DataSource = employeeData; // dataGridViewEmployee はグリッドコントロール
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"データの読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            // ボタンクリックイベント
            btnShowEmployeeInfoDetail.Click += (s, e) =>
            {
                AddEmployeeInfoForm addEmployeeInfoForm = new AddEmployeeInfoForm();
                addEmployeeInfoForm.Show();
            };
        }
    }
}
