using EmployeeManagementSystem.Contexts;
using EmployeeManagementSystem.DataModel;
using EmployeeManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        //�Y���G���[���b�Z�[�W���X�g
        List<string> errorMessages = new List<string>();

        public LoginForm()
        {
            InitializeComponent();

            // �{�^���N���b�N�C�x���g
            btnLogin.Click += btnLogin_click;


        }

        private void btnLogin_click(object sender, EventArgs e)
        {
            {
                // ���͒l���擾
                string inputMail = txtMail.Text;
                string inputPassword = txtPassword.Text;

                errorMessages = IsValidInput(inputPassword, inputMail);

                if (errorMessages == null || !errorMessages.Any())
                {
                    using (var dbContext = new EmployeeManagementSystemContext())
                    {
                        try
                        {
                            // ���[���A�h���X�ƃp�X���[�h�Ō���
                            var operatorRecord = dbContext.Operator
                                .Join(dbContext.Employee,
                                      op => op.employee_id, // operator�e�[�u���̌����L�[
                                      e => e.employee_id, // employee�e�[�u���̌����L�[
                                      (op, e) => new { Operator = op, Mail = e.mail }) // �K�v�ȃf�[�^���v���W�F�N�V����
                                .Where(record => record.Mail == inputMail && record.Operator.password == inputPassword) // �����w��
                                .FirstOrDefault(); // �ŏ��̃��R�[�h���擾



                            // SQL���O���擾
                            //string logs = dbContext.GetLogs();

                            // ���b�Z�[�W�{�b�N�X�Ƀ��O��\��
                            //MessageBox.Show(!string.IsNullOrEmpty(logs) ? logs : "���O������܂���", "�������ꂽSQL���O", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            if (operatorRecord != null)
                            {
                                // �F�ؐ���

                                //�Z�b�V�������̍쐬
                                int employeeId = operatorRecord.Operator.employee_id;
                                String session_token = CreateSession(employeeId);
                                
                                if(session_token != null)
                                {
                                    SessionModel sessionData = GetSessionRecordData(session_token);
                                    SetSession(sessionData);
                                    SessionManager.StartSession(); // �Z�b�V�������J�n
                                    Clear_Inputs();
                                    ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm(this);
                                    showEmployeeInfoForm.Show();
                                    this.Hide(); // ���O�C���t�H�[�����\��

                                }                                
                            }
                            else
                            {
                                // �F�؎��s
                                MessageBox.Show(ErrorMessages.ERR022_REQUIRED_VALID_PASSWORD_AND_MAIL, InformationMessages.TITLE010_AUTHENTICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            // ��O����
                            MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    DisplayErrorMessages(errorMessages);
                    return;
                }
            }
        }

        private List<String> IsValidInput(String password, String mail)
        {
            List<string> isValidInput_ErrorMessages = new List<string>();

            // ���͌���: ��̏ꍇ�̓G���[���b�Z�[�W��\��
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR022_MISSING_PASSWORD_AND_MAIL);
            }
            //���[���A�h���X�����������͂���Ă��邩
            else if (!System.Text.RegularExpressions.Regex.IsMatch(mail, @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
            }

            return isValidInput_ErrorMessages;
        }

        private void DisplayErrorMessages(List<string> errorMessages)
        {
            // �G���[���b�Z�[�W�����s�ŋ�؂��Č���
            String messages = string.Join(Environment.NewLine, errorMessages);
            MessageBox.Show(messages, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        internal void Clear_Inputs()
        {
            txtMail.Text = null;
            txtPassword.Text = null;
        }

        private String CreateSession(int employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    String ip_address = GetIpAddress();
                    String user_agent = GetUserAgent();
                    String session_token = Guid.NewGuid().ToString();
                    DateTime login_time = DateTime.Now;

                    // �V�����Z�b�V�������쐬
                    var newSession = new SessionModel
                    {
                        employee_id = employeeId,
                        login_time = login_time,
                        session_token = session_token,
                        ip_address = ip_address, // IP�A�h���X�̎擾���\�b�h
                        user_agent = user_agent, // UserAgent�̎擾���\�b�h
                        EmployeeIdNavigation = dbContext.Employee.FirstOrDefault(e => e.employee_id == employeeId)
                    };

                    // �f�[�^�x�[�X�ɒǉ�
                    dbContext.Session.Add(newSession);
                    dbContext.SaveChanges();

                    return session_token;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); // �z�X�g�����擾
            var ipAddress = Dns.GetHostAddresses(hostName)
                               .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return ipAddress?.ToString() ?? "�s��";
        }

        public string GetUserAgent()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.Navigate("about:blank"); //WebBrowser �R���g���[����������Ԃɂ���B
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                // User-Agent���擾
                dynamic script = browser.Document.DomDocument;
                return script?.parentWindow?.navigator?.userAgent ?? "�s��";
            }
        }

        private SessionModel GetSessionRecordData(String session_token)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    // ���[���A�h���X�ƃp�X���[�h�Ō���
                    var SessionRecord = dbContext.Session
                        .Where(s => s.session_token == session_token) // �����w��
                        .FirstOrDefault(); // �ŏ��̃��R�[�h���擾
                        // �Z�b�V�������R�[�h�擾����
                        return SessionRecord;
                }
                catch (Exception ex)
                {
                    // ��O����
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR} {ex.Message}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        private void SetSession(SessionModel sessionModel)
        {
            SessionManager.session_id = sessionModel.session_id;
            SessionManager.employee_id = sessionModel.employee_id;
            SessionManager.login_time = sessionModel.login_time;
            SessionManager.session_token = sessionModel.session_token;
            SessionManager.ip_address = sessionModel.ip_address;
            SessionManager.user_agent = sessionModel.user_agent;
            SessionManager.is_active = sessionModel.is_active;
        }
    }
}