using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    partial class ShowEmployeeInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            btnConfirm = new Button();
            btnLogout = new Button();
            btnShowEmployeeInfoDetail = new Button();
            areaSearch = new Panel();
            btnClear = new Button();
            btnSearch = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            lblSearchKanaName = new Label();
            lblSearchName = new Label();
            lblSearchEmployeeId = new Label();
            lblSelectPosition = new Label();
            lblSelectOffice = new Label();
            selectOffice = new ComboBox();
            selectPosition = new ComboBox();


            dataGridViewEmployee = new DataGridView();
            employee_id = new DataGridViewLinkColumn();
            first_name = new DataGridViewTextBoxColumn();
            last_name = new DataGridViewTextBoxColumn();
            kana_first_name = new DataGridViewTextBoxColumn();
            kana_last_name = new DataGridViewTextBoxColumn();
            mail = new DataGridViewTextBoxColumn();
            phone_num = new DataGridViewTextBoxColumn();
            hire_date = new DataGridViewTextBoxColumn();
            office_name = new DataGridViewTextBoxColumn();
            position_name = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            dataGridViewEmployee.AutoGenerateColumns = false; // 自動列生成を無効化
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn(); //チェックボックスカラム
            

            // 各列の DataPropertyName を設定
            employee_id.DataPropertyName = "employee_id";
            first_name.DataPropertyName = "first_name";
            last_name.DataPropertyName = "last_name";
            kana_first_name.DataPropertyName = "kana_first_name";
            kana_last_name.DataPropertyName = "kana_last_name";
            mail.DataPropertyName = "mail";
            phone_num.DataPropertyName = "phone_num";
            hire_date.DataPropertyName = "hire_date";
            office_name.DataPropertyName = "OfficeName"; // 区別のため大文字
            position_name.DataPropertyName = "position_name";
            status.DataPropertyName = "status";



            panel1.SuspendLayout();
            areaSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployee).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.Azure;
            panel1.Controls.Add(btnConfirm);
            panel1.Controls.Add(btnLogout);
            panel1.Controls.Add(btnShowEmployeeInfoDetail);
            panel1.Controls.Add(areaSearch);
            panel1.Controls.Add(dataGridViewEmployee);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1630, 917);
            panel1.TabIndex = 1;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.ForeColor = Color.Black;
            btnConfirm.Location = new Point(228, 80);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(144, 47);
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "確定";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            btnLogout.ImeMode = ImeMode.NoControl;
            btnLogout.Location = new Point(1407, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(112, 34);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "ログアウト";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnShowEmployeeInfoDetail
            // 
            btnShowEmployeeInfoDetail.BackColor = Color.White;
            btnShowEmployeeInfoDetail.ForeColor = Color.Black;
            btnShowEmployeeInfoDetail.Location = new Point(30, 80);
            btnShowEmployeeInfoDetail.Name = "btnShowEmployeeInfoDetail";
            btnShowEmployeeInfoDetail.Size = new Size(144, 47);
            btnShowEmployeeInfoDetail.TabIndex = 0;
            btnShowEmployeeInfoDetail.Text = "社員情報追加";
            btnShowEmployeeInfoDetail.UseVisualStyleBackColor = false;
            // 
            // areaSearch
            // 
            areaSearch.AutoSize = true;
            areaSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            areaSearch.BorderStyle = BorderStyle.FixedSingle;
            areaSearch.Controls.Add(btnClear);
            areaSearch.Controls.Add(btnSearch);
            areaSearch.Controls.Add(textBox3);
            areaSearch.Controls.Add(textBox2);
            areaSearch.Controls.Add(textBox1);
            areaSearch.Controls.Add(lblSearchKanaName);
            areaSearch.Controls.Add(lblSearchName);
            areaSearch.Controls.Add(lblSearchEmployeeId);
            areaSearch.Controls.Add(lblSelectPosition);
            areaSearch.Controls.Add(lblSelectOffice);
            areaSearch.Controls.Add(selectOffice);
            areaSearch.Controls.Add(selectPosition);
            areaSearch.Location = new Point(501, 69);
            areaSearch.Name = "areaSearch";
            areaSearch.Size = new Size(998, 136);
            areaSearch.TabIndex = 2;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(881, 37);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(112, 34);
            btnClear.TabIndex = 11;
            btnClear.Text = "クリア";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(881, 97);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(112, 34);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(622, 97);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(229, 31);
            textBox3.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(622, 54);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(229, 31);
            textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(622, 10);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(229, 31);
            textBox1.TabIndex = 7;
            // 
            // lblSearchKanaName
            // 
            lblSearchKanaName.AutoSize = true;
            lblSearchKanaName.Location = new Point(520, 100);
            lblSearchKanaName.Name = "lblSearchKanaName";
            lblSearchKanaName.Size = new Size(111, 25);
            lblSearchKanaName.TabIndex = 6;
            lblSearchKanaName.Text = "名前（カナ）";
            // 
            // lblSearchName
            // 
            lblSearchName.AutoSize = true;
            lblSearchName.Location = new Point(520, 60);
            lblSearchName.Name = "lblSearchName";
            lblSearchName.Size = new Size(48, 25);
            lblSearchName.TabIndex = 5;
            lblSearchName.Text = "名前";
            // 
            // lblSearchEmployeeId
            // 
            lblSearchEmployeeId.AutoSize = true;
            lblSearchEmployeeId.Location = new Point(520, 16);
            lblSearchEmployeeId.Name = "lblSearchEmployeeId";
            lblSearchEmployeeId.Size = new Size(84, 25);
            lblSearchEmployeeId.TabIndex = 4;
            lblSearchEmployeeId.Text = "社員番号";
            // 
            // lblSelectPosition
            // 
            lblSelectPosition.AutoSize = true;
            lblSelectPosition.Location = new Point(256, 14);
            lblSelectPosition.Name = "lblSelectPosition";
            lblSelectPosition.Size = new Size(48, 25);
            lblSelectPosition.TabIndex = 3;
            lblSelectPosition.Text = "役職";
            // 
            // lblSelectOffice
            // 
            lblSelectOffice.AutoSize = true;
            lblSelectOffice.Location = new Point(18, 14);
            lblSelectOffice.Name = "lblSelectOffice";
            lblSelectOffice.Size = new Size(48, 25);
            lblSelectOffice.TabIndex = 2;
            lblSelectOffice.Text = "拠点";
            // 
            // selectOffice
            // 
            selectOffice.FormattingEnabled = true;
            selectOffice.Location = new Point(68, 11);
            selectOffice.Name = "selectOffice";
            selectOffice.Size = new Size(182, 33);
            selectOffice.TabIndex = 1;
            // 
            // selectPosition
            // 
            selectPosition.FormattingEnabled = true;
            selectPosition.Location = new Point(310, 12);
            selectPosition.Name = "selectPosition";
            selectPosition.Size = new Size(182, 33);
            selectPosition.TabIndex = 0;
            // 
            // dataGridViewEmployee
            // 
            dataGridViewEmployee.AllowUserToAddRows = false;
            dataGridViewEmployee.AllowUserToDeleteRows = false;
            dataGridViewEmployee.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployee.Columns.AddRange(new DataGridViewColumn[] { employee_id, first_name, last_name, kana_first_name, kana_last_name, mail, phone_num, hire_date, office_name, position_name, status });
            dataGridViewEmployee.Dock = DockStyle.Bottom;
            dataGridViewEmployee.Location = new Point(0, 274);
            dataGridViewEmployee.Name = "dataGridViewEmployee";
            dataGridViewEmployee.RowHeadersWidth = 62;
            dataGridViewEmployee.Size = new Size(1630, 643);
            dataGridViewEmployee.TabIndex = 1;
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle.Font = new Font("メイリオ", 10, FontStyle.Bold); // ヘッダー用フォント
            dataGridViewEmployee.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // 内容に応じて行の高さを調整
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue; // 背景色を青に設定
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // 文字色を白に設定
            dataGridViewEmployee.EnableHeadersVisualStyles = false; // 標準スタイルを無効化
            dataGridViewEmployee.RowHeadersVisible = false; // グリッドの１列目を非表示にする
            dataGridViewEmployee.Columns.Insert(0, checkBoxColumn); // 先頭にチェックボックス列を追加
            dataGridViewEmployee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // 列幅を内容に合わせて自動調整
            dataGridViewEmployee.EditMode = DataGridViewEditMode.EditOnEnter; // クリックで編集開始
            dataGridViewEmployee.ReadOnly = false; // 全体を編集可能にする




            //
            // checkBoxColumn
            //
            checkBoxColumn.Name = "選択"; // 列名
            checkBoxColumn.HeaderText = "選択"; // ヘッダーの表示名
            checkBoxColumn.Width = 50; // 列幅を設定
            checkBoxColumn.TrueValue = true; // チェック状態の値
            checkBoxColumn.FalseValue = false; // 未チェック状態の値

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(212, 45);
            label1.TabIndex = 0;
            label1.Text = "社員情報一覧";
            // 
            // employee_id
            // 
            employee_id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            employee_id.HeaderText = "社員番号";
            employee_id.MinimumWidth = 8;
            employee_id.Name = "employee_id";
            // 
            // first_name
            // 
            first_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            first_name.HeaderText = "姓";
            first_name.MinimumWidth = 8;
            first_name.Name = "first_name";
            first_name.ReadOnly = false; // 全体を編集可能にする
            dataGridViewEmployee.Columns["first_name"].ReadOnly = false; // 該当列を編集可能に設定
            // 
            // last_name
            // 
            last_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            last_name.HeaderText = "名";
            last_name.MinimumWidth = 8;
            last_name.Name = "last_name";
            // 
            // kana_first_name
            // 
            kana_first_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            kana_first_name.HeaderText = "姓（カナ）";
            kana_first_name.MinimumWidth = 8;
            kana_first_name.Name = "kana_first_name";
            // 
            // kana_last_name
            // 
            kana_last_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            kana_last_name.HeaderText = "名（カナ）";
            kana_last_name.MinimumWidth = 8;
            kana_last_name.Name = "kana_last_name";
            // 
            // mail
            // 
            mail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mail.HeaderText = "メールアドレス";
            mail.MinimumWidth = 8;
            mail.Name = "mail";
            // 
            // phone_num
            // 
            phone_num.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            phone_num.HeaderText = "電話番号";
            phone_num.MinimumWidth = 8;
            phone_num.Name = "phone_num";
            // 
            // hire_date
            // 
            hire_date.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hire_date.HeaderText = "雇用日";
            hire_date.MinimumWidth = 8;
            hire_date.Name = "hire_date";
            // 
            // office_name
            // 
            office_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            office_name.HeaderText = "拠点";
            office_name.MinimumWidth = 8;
            office_name.Name = "office_name";
            // 
            // position_name
            // 
            position_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            position_name.HeaderText = "役職";
            position_name.MinimumWidth = 8;
            position_name.Name = "position_name";
            // 
            // status
            // 
            status.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            status.HeaderText = "ステータス";
            status.MinimumWidth = 8;
            status.Name = "status";
            // 
            // ShowEmployeeInfoForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1630, 917);
            Controls.Add(panel1);
            Name = "ShowEmployeeInfoForm";
            Text = "社員情報一覧画面";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            areaSearch.ResumeLayout(false);
            areaSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridViewEmployee;
        private Label label1;
        private Button btnLogout;
        private Panel areaSearch;
        private Button btnShowEmployeeInfoDetail;
        private Label lblSelectPosition;
        private Label lblSelectOffice;
        private ComboBox selectOffice;
        private ComboBox selectPosition;
        private Label lblSearchKanaName;
        private Label lblSearchName;
        private Label lblSearchEmployeeId;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button btnClear;
        private Button btnSearch;
        private Button btnConfirm;
        private DataGridViewLinkColumn employee_id;
        private DataGridViewTextBoxColumn first_name;
        private DataGridViewTextBoxColumn last_name;
        private DataGridViewTextBoxColumn kana_first_name;
        private DataGridViewTextBoxColumn kana_last_name;
        private DataGridViewTextBoxColumn mail;
        private DataGridViewTextBoxColumn phone_num;
        private DataGridViewTextBoxColumn hire_date;
        private DataGridViewTextBoxColumn office_name;
        private DataGridViewTextBoxColumn position_name;
        private DataGridViewTextBoxColumn status;
    }
}