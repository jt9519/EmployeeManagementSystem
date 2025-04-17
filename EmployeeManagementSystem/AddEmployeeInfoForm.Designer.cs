namespace EmployeeManagementSystem
{
    partial class AddEmployeeInfoForm
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
            selectPosition = new ComboBox();
            selectOffice = new ComboBox();
            linkCancel = new LinkLabel();
            btnAdd = new Button();
            btnClear = new Button();
            selectHireDate = new DateTimePicker();
            txtPhoneNum = new TextBox();
            txtMail = new TextBox();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            txtKanaLastName = new TextBox();
            txtKanaFirstName = new TextBox();
            lblPosition = new Label();
            lblOffice = new Label();
            lblHireDate = new Label();
            lblPhoneNum = new Label();
            lblMail = new Label();
            lblLastName = new Label();
            lblFirstName = new Label();
            lblKanaLastName = new Label();
            lblKanaFirstName = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Azure;
            panel1.Controls.Add(selectPosition);
            panel1.Controls.Add(selectOffice);
            panel1.Controls.Add(linkCancel);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(selectHireDate);
            panel1.Controls.Add(txtPhoneNum);
            panel1.Controls.Add(txtMail);
            panel1.Controls.Add(txtLastName);
            panel1.Controls.Add(txtFirstName);
            panel1.Controls.Add(txtKanaLastName);
            panel1.Controls.Add(txtKanaFirstName);
            panel1.Controls.Add(lblPosition);
            panel1.Controls.Add(lblOffice);
            panel1.Controls.Add(lblHireDate);
            panel1.Controls.Add(lblPhoneNum);
            panel1.Controls.Add(lblMail);
            panel1.Controls.Add(lblLastName);
            panel1.Controls.Add(lblFirstName);
            panel1.Controls.Add(lblKanaLastName);
            panel1.Controls.Add(lblKanaFirstName);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(2);
            panel1.Size = new Size(942, 595);
            panel1.TabIndex = 1;
            // 
            // selectPosition
            // 
            selectPosition.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            selectPosition.FormattingEnabled = true;
            selectPosition.Location = new Point(144, 501);
            selectPosition.Margin = new Padding(2);
            selectPosition.Name = "selectPosition";
            selectPosition.Size = new Size(146, 33);
            selectPosition.TabIndex = 34;
            // 
            // selectOffoce
            // 
            selectOffice.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            selectOffice.FormattingEnabled = true;
            selectOffice.Location = new Point(143, 439);
            selectOffice.Margin = new Padding(2);
            selectOffice.Name = "selectOffice";
            selectOffice.Size = new Size(146, 33);
            selectOffice.TabIndex = 33;
            // 
            // linkCancel
            // 
            linkCancel.AutoSize = true;
            linkCancel.Location = new Point(636, 511);
            linkCancel.Margin = new Padding(2, 0, 2, 0);
            linkCancel.Name = "linkCancel";
            linkCancel.Size = new Size(65, 20);
            linkCancel.TabIndex = 32;
            linkCancel.TabStop = true;
            linkCancel.Text = "キャンセル";
            // 
            // btnComfirm
            // 
            btnAdd.Location = new Point(616, 439);
            btnAdd.Margin = new Padding(2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 27);
            btnAdd.TabIndex = 31;
            btnAdd.Text = "追加";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(486, 439);
            btnClear.Margin = new Padding(2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 27);
            btnClear.TabIndex = 30;
            btnClear.Text = "クリア";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // selectHireDate
            // 
            selectHireDate.CalendarFont = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            selectHireDate.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            selectHireDate.Location = new Point(160, 378);
            selectHireDate.Margin = new Padding(2);
            selectHireDate.Name = "selectHireDate";
            selectHireDate.Size = new Size(241, 32);
            selectHireDate.TabIndex = 27;
            // 
            // txtPhoneNum
            // 
            txtPhoneNum.BorderStyle = BorderStyle.FixedSingle;
            txtPhoneNum.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtPhoneNum.Location = new Point(183, 316);
            txtPhoneNum.Margin = new Padding(2);
            txtPhoneNum.Name = "txtPhoneNum";
            txtPhoneNum.Size = new Size(265, 32);
            txtPhoneNum.TabIndex = 26;
            // 
            // txtMail
            // 
            txtMail.BorderStyle = BorderStyle.FixedSingle;
            txtMail.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtMail.Location = new Point(204, 253);
            txtMail.Margin = new Padding(2);
            txtMail.Name = "txtMail";
            txtMail.Size = new Size(469, 32);
            txtMail.TabIndex = 25;
            // 
            // txtLastName
            // 
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtLastName.Location = new Point(482, 191);
            txtLastName.Margin = new Padding(2);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(180, 32);
            txtLastName.TabIndex = 24;
            // 
            // txtFirstName
            // 
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtFirstName.Location = new Point(191, 191);
            txtFirstName.Margin = new Padding(2);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(180, 32);
            txtFirstName.TabIndex = 23;
            // 
            // txtKanaLastName
            // 
            txtKanaLastName.BorderStyle = BorderStyle.FixedSingle;
            txtKanaLastName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtKanaLastName.Location = new Point(491, 129);
            txtKanaLastName.Margin = new Padding(2);
            txtKanaLastName.Name = "txtKanaLastName";
            txtKanaLastName.Size = new Size(180, 32);
            txtKanaLastName.TabIndex = 22;
            // 
            // txtKanaFirstName
            // 
            txtKanaFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtKanaFirstName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtKanaFirstName.Location = new Point(196, 126);
            txtKanaFirstName.Margin = new Padding(2);
            txtKanaFirstName.Name = "txtKanaFirstName";
            txtKanaFirstName.Size = new Size(180, 32);
            txtKanaFirstName.TabIndex = 21;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.BackColor = Color.DodgerBlue;
            lblPosition.BorderStyle = BorderStyle.FixedSingle;
            lblPosition.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblPosition.ForeColor = Color.White;
            lblPosition.Location = new Point(90, 501);
            lblPosition.Margin = new Padding(2);
            lblPosition.Name = "lblPosition";
            lblPosition.Padding = new Padding(2);
            lblPosition.Size = new Size(56, 31);
            lblPosition.TabIndex = 20;
            lblPosition.Text = "役職";
            // 
            // lblOffice
            // 
            lblOffice.AutoSize = true;
            lblOffice.BackColor = Color.DodgerBlue;
            lblOffice.BorderStyle = BorderStyle.FixedSingle;
            lblOffice.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblOffice.ForeColor = Color.White;
            lblOffice.Location = new Point(90, 440);
            lblOffice.Margin = new Padding(2);
            lblOffice.Name = "lblOffice";
            lblOffice.Padding = new Padding(2);
            lblOffice.Size = new Size(56, 31);
            lblOffice.TabIndex = 19;
            lblOffice.Text = "拠点";
            // 
            // lblHireDate
            // 
            lblHireDate.AutoSize = true;
            lblHireDate.BackColor = Color.DodgerBlue;
            lblHireDate.BorderStyle = BorderStyle.FixedSingle;
            lblHireDate.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblHireDate.ForeColor = Color.White;
            lblHireDate.Location = new Point(90, 378);
            lblHireDate.Margin = new Padding(2);
            lblHireDate.Name = "lblHireDate";
            lblHireDate.Padding = new Padding(2);
            lblHireDate.Size = new Size(75, 31);
            lblHireDate.TabIndex = 18;
            lblHireDate.Text = "雇用日";
            // 
            // lblPhoneNum
            // 
            lblPhoneNum.AutoSize = true;
            lblPhoneNum.BackColor = Color.DodgerBlue;
            lblPhoneNum.BorderStyle = BorderStyle.FixedSingle;
            lblPhoneNum.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblPhoneNum.ForeColor = Color.White;
            lblPhoneNum.Location = new Point(90, 316);
            lblPhoneNum.Margin = new Padding(2);
            lblPhoneNum.Name = "lblPhoneNum";
            lblPhoneNum.Padding = new Padding(2);
            lblPhoneNum.Size = new Size(94, 31);
            lblPhoneNum.TabIndex = 17;
            lblPhoneNum.Text = "電話番号";
            // 
            // lblMail
            // 
            lblMail.AutoSize = true;
            lblMail.BackColor = Color.DodgerBlue;
            lblMail.BorderStyle = BorderStyle.FixedSingle;
            lblMail.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblMail.ForeColor = Color.White;
            lblMail.Location = new Point(90, 254);
            lblMail.Margin = new Padding(2);
            lblMail.Name = "lblMail";
            lblMail.Padding = new Padding(2);
            lblMail.Size = new Size(115, 31);
            lblMail.TabIndex = 16;
            lblMail.Text = "メールアドレス";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.BackColor = Color.DodgerBlue;
            lblLastName.BorderStyle = BorderStyle.FixedSingle;
            lblLastName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblLastName.ForeColor = Color.White;
            lblLastName.Location = new Point(385, 191);
            lblLastName.Margin = new Padding(2);
            lblLastName.Name = "lblLastName";
            lblLastName.Padding = new Padding(2);
            lblLastName.Size = new Size(102, 31);
            lblLastName.TabIndex = 15;
            lblLastName.Text = "名             ";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.BackColor = Color.DodgerBlue;
            lblFirstName.BorderStyle = BorderStyle.FixedSingle;
            lblFirstName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblFirstName.ForeColor = Color.White;
            lblFirstName.Location = new Point(90, 191);
            lblFirstName.Margin = new Padding(2);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Padding = new Padding(2);
            lblFirstName.Size = new Size(102, 31);
            lblFirstName.TabIndex = 14;
            lblFirstName.Text = "姓             ";
            // 
            // lblKanaLastName
            // 
            lblKanaLastName.AutoSize = true;
            lblKanaLastName.BackColor = Color.DodgerBlue;
            lblKanaLastName.BorderStyle = BorderStyle.FixedSingle;
            lblKanaLastName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblKanaLastName.ForeColor = Color.White;
            lblKanaLastName.Location = new Point(385, 129);
            lblKanaLastName.Margin = new Padding(2);
            lblKanaLastName.Name = "lblKanaLastName";
            lblKanaLastName.Padding = new Padding(2);
            lblKanaLastName.Size = new Size(107, 31);
            lblKanaLastName.TabIndex = 13;
            lblKanaLastName.Text = "名（かな）";
            // 
            // lblKanaFirstName
            // 
            lblKanaFirstName.AutoSize = true;
            lblKanaFirstName.BackColor = Color.DodgerBlue;
            lblKanaFirstName.BorderStyle = BorderStyle.FixedSingle;
            lblKanaFirstName.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblKanaFirstName.ForeColor = Color.White;
            lblKanaFirstName.Location = new Point(90, 126);
            lblKanaFirstName.Margin = new Padding(2);
            lblKanaFirstName.Name = "lblKanaFirstName";
            lblKanaFirstName.Padding = new Padding(2);
            lblKanaFirstName.Size = new Size(107, 31);
            lblKanaFirstName.TabIndex = 12;
            lblKanaFirstName.Text = "姓（かな）";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(179, 37);
            label1.TabIndex = 1;
            label1.Text = "社員情報追加";
            // 
            // AddEmployeeInfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 595);
            Controls.Add(panel1);
            Margin = new Padding(2);
            Name = "AddEmployeeInfoForm";
            Text = "社員情報追加画面";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private LinkLabel linkCancel;
        private Button btnAdd;
        private Button btnClear;
        private DateTimePicker selectHireDate;
        private TextBox txtPhoneNum;
        private TextBox txtMail;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtKanaLastName;
        private TextBox txtKanaFirstName;
        private Label lblPosition;
        private Label lblOffice;
        private Label lblHireDate;
        private Label lblPhoneNum;
        private Label lblMail;
        private Label lblLastName;
        private Label lblFirstName;
        private Label lblKanaLastName;
        private Label lblKanaFirstName;
        private Label label1;
        private ComboBox selectPosition;
        private ComboBox selectOffice;
    }
}