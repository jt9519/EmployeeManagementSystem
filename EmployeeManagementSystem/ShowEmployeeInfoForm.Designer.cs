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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            txtErrorMessages = new TextBox();
            btnConfirm = new Button();
            btnLogout = new Button();
            btnShowEmployeeInfoDetail = new Button();
            areaSearch = new Panel();
            chkRetired = new CheckBox();
            btnClear = new Button();
            btnSearch = new Button();
            txtKanaName = new TextBox();
            txtName = new TextBox();
            txtEmployeeId = new TextBox();
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
            label1 = new Label();
            panel1.SuspendLayout();
            areaSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployee).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.Azure;
            panel1.Controls.Add(txtErrorMessages);
            panel1.Controls.Add(btnConfirm);
            panel1.Controls.Add(btnLogout);
            panel1.Controls.Add(btnShowEmployeeInfoDetail);
            panel1.Controls.Add(areaSearch);
            panel1.Controls.Add(dataGridViewEmployee);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1304, 734);
            panel1.TabIndex = 1;
            // 
            // txtErrorMessages
            // 
            txtErrorMessages.BackColor = Color.Azure;
            txtErrorMessages.BorderStyle = BorderStyle.None;
            txtErrorMessages.Enabled = false;
            txtErrorMessages.Location = new Point(24, 178);
            txtErrorMessages.Multiline = true;
            txtErrorMessages.Name = "txtErrorMessages";
            txtErrorMessages.ReadOnly = true;
            txtErrorMessages.Size = new Size(516, 37);
            txtErrorMessages.TabIndex = 6;
            txtErrorMessages.Visible = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.ForeColor = Color.Black;
            btnConfirm.Location = new Point(182, 64);
            btnConfirm.Margin = new Padding(2);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(115, 38);
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "確定";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            btnLogout.ImeMode = ImeMode.NoControl;
            btnLogout.Location = new Point(1126, 10);
            btnLogout.Margin = new Padding(2);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(90, 27);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "ログアウト";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnShowEmployeeInfoDetail
            // 
            btnShowEmployeeInfoDetail.BackColor = Color.White;
            btnShowEmployeeInfoDetail.ForeColor = Color.Black;
            btnShowEmployeeInfoDetail.Location = new Point(24, 64);
            btnShowEmployeeInfoDetail.Margin = new Padding(2);
            btnShowEmployeeInfoDetail.Name = "btnShowEmployeeInfoDetail";
            btnShowEmployeeInfoDetail.Size = new Size(115, 38);
            btnShowEmployeeInfoDetail.TabIndex = 0;
            btnShowEmployeeInfoDetail.Text = "社員情報追加";
            btnShowEmployeeInfoDetail.UseVisualStyleBackColor = false;
            // 
            // areaSearch
            // 
            areaSearch.AutoSize = true;
            areaSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            areaSearch.BorderStyle = BorderStyle.FixedSingle;
            areaSearch.Controls.Add(chkRetired);
            areaSearch.Controls.Add(btnClear);
            areaSearch.Controls.Add(btnSearch);
            areaSearch.Controls.Add(txtKanaName);
            areaSearch.Controls.Add(txtName);
            areaSearch.Controls.Add(txtEmployeeId);
            areaSearch.Controls.Add(lblSearchKanaName);
            areaSearch.Controls.Add(lblSearchName);
            areaSearch.Controls.Add(lblSearchEmployeeId);
            areaSearch.Controls.Add(lblSelectPosition);
            areaSearch.Controls.Add(lblSelectOffice);
            areaSearch.Controls.Add(selectOffice);
            areaSearch.Controls.Add(selectPosition);
            areaSearch.Location = new Point(344, 55);
            areaSearch.Margin = new Padding(2);
            areaSearch.Name = "areaSearch";
            areaSearch.Size = new Size(928, 111);
            areaSearch.TabIndex = 2;
            // 
            // chkRetired
            // 
            chkRetired.AutoSize = true;
            chkRetired.Location = new Point(14, 10);
            chkRetired.Name = "chkRetired";
            chkRetired.Size = new Size(76, 24);
            chkRetired.TabIndex = 12;
            chkRetired.Text = "退職済";
            chkRetired.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(834, 32);
            btnClear.Margin = new Padding(2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 27);
            btnClear.TabIndex = 11;
            btnClear.Text = "クリア";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(834, 75);
            btnSearch.Margin = new Padding(2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(90, 27);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtKanaName
            // 
            txtKanaName.BorderStyle = BorderStyle.FixedSingle;
            txtKanaName.Location = new Point(615, 80);
            txtKanaName.Margin = new Padding(2);
            txtKanaName.Name = "txtKanaName";
            txtKanaName.Size = new Size(184, 27);
            txtKanaName.TabIndex = 9;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(615, 44);
            txtName.Margin = new Padding(2);
            txtName.Name = "txtName";
            txtName.Size = new Size(184, 27);
            txtName.TabIndex = 8;
            // 
            // txtEmployeeId
            // 
            txtEmployeeId.BorderStyle = BorderStyle.FixedSingle;
            txtEmployeeId.Location = new Point(615, 10);
            txtEmployeeId.Margin = new Padding(2);
            txtEmployeeId.Name = "txtEmployeeId";
            txtEmployeeId.Size = new Size(184, 27);
            txtEmployeeId.TabIndex = 7;
            // 
            // lblSearchKanaName
            // 
            lblSearchKanaName.AutoSize = true;
            lblSearchKanaName.Location = new Point(518, 82);
            lblSearchKanaName.Margin = new Padding(2, 0, 2, 0);
            lblSearchKanaName.Name = "lblSearchKanaName";
            lblSearchKanaName.Size = new Size(94, 20);
            lblSearchKanaName.TabIndex = 6;
            lblSearchKanaName.Text = "名前（かな）";
            // 
            // lblSearchName
            // 
            lblSearchName.AutoSize = true;
            lblSearchName.Location = new Point(565, 48);
            lblSearchName.Margin = new Padding(2, 0, 2, 0);
            lblSearchName.Name = "lblSearchName";
            lblSearchName.Size = new Size(39, 20);
            lblSearchName.TabIndex = 5;
            lblSearchName.Text = "名前";
            // 
            // lblSearchEmployeeId
            // 
            lblSearchEmployeeId.AutoSize = true;
            lblSearchEmployeeId.Location = new Point(538, 16);
            lblSearchEmployeeId.Margin = new Padding(2, 0, 2, 0);
            lblSearchEmployeeId.Name = "lblSearchEmployeeId";
            lblSearchEmployeeId.Size = new Size(69, 20);
            lblSearchEmployeeId.TabIndex = 4;
            lblSearchEmployeeId.Text = "社員番号";
            // 
            // lblSelectPosition
            // 
            lblSelectPosition.AutoSize = true;
            lblSelectPosition.Location = new Point(335, 14);
            lblSelectPosition.Margin = new Padding(2, 0, 2, 0);
            lblSelectPosition.Name = "lblSelectPosition";
            lblSelectPosition.Size = new Size(39, 20);
            lblSelectPosition.TabIndex = 3;
            lblSelectPosition.Text = "役職";
            // 
            // lblSelectOffice
            // 
            lblSelectOffice.AutoSize = true;
            lblSelectOffice.Location = new Point(128, 11);
            lblSelectOffice.Margin = new Padding(2, 0, 2, 0);
            lblSelectOffice.Name = "lblSelectOffice";
            lblSelectOffice.Size = new Size(39, 20);
            lblSelectOffice.TabIndex = 2;
            lblSelectOffice.Text = "拠点";
            // 
            // selectOffice
            // 
            selectOffice.FormattingEnabled = true;
            selectOffice.Location = new Point(169, 9);
            selectOffice.Margin = new Padding(2);
            selectOffice.Name = "selectOffice";
            selectOffice.Size = new Size(146, 28);
            selectOffice.TabIndex = 1;
            // 
            // selectPosition
            // 
            selectPosition.FormattingEnabled = true;
            selectPosition.Location = new Point(377, 11);
            selectPosition.Margin = new Padding(2);
            selectPosition.Name = "selectPosition";
            selectPosition.Size = new Size(146, 28);
            selectPosition.TabIndex = 0;
            // 
            // dataGridViewEmployee
            // 
            dataGridViewEmployee.AllowUserToAddRows = false;
            dataGridViewEmployee.AllowUserToDeleteRows = false;
            dataGridViewEmployee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewEmployee.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Blue;
            dataGridViewCellStyle2.Font = new Font("メイリオ", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployee.Columns.AddRange(new DataGridViewColumn[] { employee_id, first_name, last_name, kana_first_name, kana_last_name, mail, phone_num, hire_date, office_name, position_name, status });
            dataGridViewEmployee.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridViewEmployee.EnableHeadersVisualStyles = false;
            dataGridViewEmployee.Location = new Point(0, 220);
            dataGridViewEmployee.Margin = new Padding(2);
            dataGridViewEmployee.Name = "dataGridViewEmployee";
            dataGridViewEmployee.RowHeadersWidth = 62;
            dataGridViewEmployee.Size = new Size(panel1.Width - 10, panel1.Height - 100);
            dataGridViewEmployee.TabIndex = 1;
            // 
            // employee_id
            // 
            employee_id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            employee_id.DataPropertyName = "employee_id";
            employee_id.HeaderText = "社員番号";
            employee_id.MinimumWidth = 8;
            employee_id.Name = "employee_id";
            employee_id.ReadOnly = true;
            // 
            // first_name
            // 
            first_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            first_name.DataPropertyName = "first_name";
            first_name.HeaderText = "姓";
            first_name.MinimumWidth = 8;
            first_name.Name = "first_name";
            // 
            // last_name
            // 
            last_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            last_name.DataPropertyName = "last_name";
            last_name.HeaderText = "名";
            last_name.MinimumWidth = 8;
            last_name.Name = "last_name";
            // 
            // kana_first_name
            // 
            kana_first_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            kana_first_name.DataPropertyName = "kana_first_name";
            kana_first_name.HeaderText = "姓（かな）";
            kana_first_name.MinimumWidth = 8;
            kana_first_name.Name = "kana_first_name";
            // 
            // kana_last_name
            // 
            kana_last_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            kana_last_name.DataPropertyName = "kana_last_name";
            kana_last_name.HeaderText = "名（かな）";
            kana_last_name.MinimumWidth = 8;
            kana_last_name.Name = "kana_last_name";
            // 
            // mail
            // 
            mail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mail.DataPropertyName = "mail";
            mail.HeaderText = "メールアドレス";
            mail.MinimumWidth = 8;
            mail.Name = "mail";
            // 
            // phone_num
            // 
            phone_num.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            phone_num.DataPropertyName = "phone_num";
            phone_num.HeaderText = "電話番号";
            phone_num.MinimumWidth = 8;
            phone_num.Name = "phone_num";
            // 
            // hire_date
            // 
            hire_date.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hire_date.DataPropertyName = "hire_date";
            hire_date.HeaderText = "雇用日";
            hire_date.MinimumWidth = 8;
            hire_date.Name = "hire_date";
            hire_date.ReadOnly = true;
            // 
            // office_name
            // 
            office_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            office_name.DataPropertyName = "office_name";
            office_name.HeaderText = "拠点";
            office_name.MinimumWidth = 8;
            office_name.Name = "office_name";
            // 
            // position_name
            // 
            position_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            position_name.DataPropertyName = "position_name";
            position_name.HeaderText = "役職";
            position_name.MinimumWidth = 8;
            position_name.Name = "position_name";
            // 
            // status
            // 
            status.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            status.DataPropertyName = "status";
            status.HeaderText = "ステータス";
            status.MinimumWidth = 8;
            status.Name = "status";
            status.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(10, 12);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(179, 37);
            label1.TabIndex = 0;
            label1.Text = "社員情報一覧";
            // 
            // ShowEmployeeInfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1304, 734);
            Controls.Add(panel1);
            Margin = new Padding(2);
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
        private TextBox txtKanaName;
        private TextBox txtName;
        private TextBox txtEmployeeId;
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
        private TextBox txtErrorMessages;
        private CheckBox chkRetired;
    }
}