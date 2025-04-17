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

        //拠点と役職の項目をコンボボックスにセット
        private void setConboBoxData(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
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

                    MessageBox.Show("データが正常に保存されました。", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"データの保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
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
        }
    }
}