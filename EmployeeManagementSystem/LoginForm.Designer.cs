namespace EmployeeManagementSystem
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            lblMail = new Label();
            lblPassword = new Label();
            txtMail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            panelLogin = new Panel();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // lblMail
            // 
            resources.ApplyResources(lblMail, "lblMail");
            lblMail.Name = "lblMail";
            // 
            // lblPassword
            // 
            resources.ApplyResources(lblPassword, "lblPassword");
            lblPassword.Name = "lblPassword";
            // 
            // txtMail
            // 
            resources.ApplyResources(txtMail, "txtMail");
            txtMail.BorderStyle = BorderStyle.FixedSingle;
            txtMail.Name = "txtMail";
            // 
            // txtPassword
            // 
            resources.ApplyResources(txtPassword, "txtPassword");
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Name = "txtPassword";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            resources.ApplyResources(btnLogin, "btnLogin");
            btnLogin.ForeColor = Color.Black;
            btnLogin.Name = "btnLogin";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // panelLogin
            // 
            resources.ApplyResources(panelLogin, "panelLogin");
            panelLogin.BackColor = Color.LightCyan;
            panelLogin.BorderStyle = BorderStyle.FixedSingle;
            panelLogin.Controls.Add(lblMail);
            panelLogin.Controls.Add(lblPassword);
            panelLogin.Controls.Add(txtMail);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.ForeColor = SystemColors.ActiveCaptionText;
            panelLogin.Name = "panelLogin";
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoginForm";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblMail;
        private Label lblPassword;
        private TextBox txtMail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Panel panelLogin;
    }        
}
