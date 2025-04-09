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
    public partial class AddEmployeeInfoForm : Form
    {
        public AddEmployeeInfoForm()
        {
            InitializeComponent();
            this.ClientSize = new Size(1200, 800); // フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // サイズ変更を無効化
            this.MaximizeBox = false; // 最大化ボタンを無効化
            this.StartPosition = FormStartPosition.CenterScreen; // 画面中央に表示

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
    }
}
