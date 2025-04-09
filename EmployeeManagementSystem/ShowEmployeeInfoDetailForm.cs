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
        public ShowEmployeeInfoDetailForm(string employeeId)
        {
            InitializeComponent();
            this.EmployeeId = employeeId;
            this.ClientSize = new Size(1200, 800); // フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // サイズ変更を無効化
            this.MaximizeBox = false; // 最大化ボタンを無効化
            this.StartPosition = FormStartPosition.CenterScreen; // 画面中央に表示

            //フォームロード時にデータを表示
            this.Load += ShowEmployeeInfoDetailForm_Load;

        }
        private void ShowEmployeeInfoDetailForm_Load(object sender, EventArgs e)
        {
            setConboBoxData();
            LoadEmployeeDetails(EmployeeId);
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
    }
}
