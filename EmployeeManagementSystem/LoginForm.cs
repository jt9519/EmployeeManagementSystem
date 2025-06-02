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

        /// <summary>
        /// ���O�C����ʂ̃C�x���g
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // ���O�C���{�^���N���b�N�C�x���g
            btnLogin.Click += btnLogin_click;

        }

        /// <summary>
        /// ���O�C���{�^���N���b�N�C�x���g�B<br/>
        /// ���͂��ꂽ���[���A�h���X�ƃp�X���[�h�̑g�ݍ��킹��DB�ɂ���ꍇ�Ј����ꗗ��ʂ֑J�ڂ��郁�\�b�h
        /// </summary>
        /// <param name="sender">�C�x���g�𔭐��������I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g�̃f�[�^</param>
        private void btnLogin_click(object? sender, EventArgs e)
        {
            {
                if(sender == null)
                {
                    MessageBox.Show($"{ErrorMessages.ERR019_DATABASE_READ_ERROR}", InformationMessages.TITLE002_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ���͒l���擾
                string inputMail = txtMail.Text; //���[���A�h���X
                string inputPassword = txtPassword.Text; //�p�X���[�h

                
                errorMessages = IsValidInput(inputPassword, inputMail); //���̓`�F�b�N�����G���[���b�Z�[�W�̃��X�g���󂯎��

                //errorMessages �� null �܂��͋�̃R���N�V�����ł��邩
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



                            //�������ʂ�null�ł͂Ȃ���
                            if (operatorRecord != null)
                            {
                                // �F�ؐ���

                                //�Z�b�V�������̍쐬
                                int employeeId = operatorRecord.Operator.employee_id; //���O�C�����Ă���Ј��ԍ�
                                string? session_token = CreateSession(employeeId); //
                                
                                if(session_token != null)
                                {
                                    SessionModel? sessionData = GetSessionRecordData(session_token);
                                    SetSession(sessionData);
                                    SessionManager.StartSession(); // �Z�b�V�������J�n
                                    Clear_Inputs();

                                    // ���݂̃��O�C����� (`this`) �̃f�[�^��n����
                                    // �Ј����ꗗ��� (`ShowEmployeeInfoForm`) ���C���X�^���X������
                                    ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm(this); 

                                    showEmployeeInfoForm.Show(); //�Ј����ꗗ��ʂ�\��
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

        /// <summary>
        /// �p�X���[�h�ƃ��[���A�h���X�̓��̓`�F�b�N���s���A�o���f�[�V���������s�����ꍇ�G���[���b�Z�[�W�̃��X�g��Ԃ����\�b�h�i�o���f�[�V���������̏ꍇ�߂�l�̃��X�g��null�j
        /// </summary>
        private List<string> IsValidInput(string password, string mail)
        {

            List<string> isValidInput_ErrorMessages = new List<string>(); //�G���[���b�Z�[�W���i�[���邽�߂̃��X�g


            // ���͌���: ��̏ꍇ�̓G���[���b�Z�[�W��\��
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
            {
                //�G���[���b�Z�[�W�����X�g�ɒǉ�
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR022_MISSING_PASSWORD_AND_MAIL);
            }
            //���[���A�h���X�����������͂���Ă��邩
            else if (!System.Text.RegularExpressions.Regex.IsMatch(mail, @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                isValidInput_ErrorMessages.Add(ErrorMessages.ERR012_REQUIRED_VALID_MAIL);
            }

            return isValidInput_ErrorMessages;
        }

        /// <summary>
        /// �����Ŏ󂯎�����G���[���b�Z�[�W�̃��X�g�����b�Z�[�W�{�b�N�X�ŕ\�����郁�\�b�h
        /// </summary>
        /// <param name="errorMessages">�G���[���b�Z�[�W���i�[���ꂽ���X�g</param>
        private void DisplayErrorMessages(List<string> errorMessages)
        {
            // �G���[���b�Z�[�W�����s�ŋ�؂��Č���
            string messages = string.Join(Environment.NewLine, errorMessages);
            MessageBox.Show(messages, InformationMessages.TITLE004_INPUT_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>
        /// ���[���A�h���X�ƃp�X���[�h�̃e�L�X�g�{�b�N�X�̓��͒l���N���A���郁�\�b�h
        /// </summary>
        internal void Clear_Inputs()
        {
            txtMail.Text = null;
            txtPassword.Text = null;
        }

        /// <summary>
        /// �����Ń��O�C�����悤�Ƃ��Ă���Ј��ԍ����󂯎��A�󂯎�����Ј��ԍ��AIP�A�h���X�A<br/>
        /// �N���C�A���g�u���E�U���A�쐬�����Z�b�V�����g�[�N���A���O�C�����Ԃ��Z�b�V�����e�[�u���ɓo�^���Z�b�V�����g�[�N����Ԃ����\�b�h
        /// </summary>
        /// <param name="employeeId">���O�C�����悤�Ƃ��Ă���Ј��ԍ�</param>
        /// <returns>session_token</returns>
        private string? CreateSession(int employeeId)
        {
            using (var dbContext = new EmployeeManagementSystemContext())
            {
                try
                {
                    string ip_address = GetIpAddress(); //IP�A�h���X
                    string user_agent = GetUserAgent(); //�N���C�A���g�u���E�U���
                    string session_token = Guid.NewGuid().ToString(); //�Z�b�V�����g�[�N��
                    DateTime login_time = DateTime.Now; //���O�C������

                    // �V�����Z�b�V�������쐬�i�Z�b�V�����e�[�u���ɃZ�b�V������񃌃R�[�h�̐V�K�o�^�j
                    var newSession = new SessionModel
                    {
                        employee_id = employeeId,
                        login_time = login_time,
                        session_token = session_token,
                        ip_address = ip_address,
                        user_agent = user_agent,
                        is_active = true,
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

        /// <summary>
        /// IP�A�h���X���擾���Ԃ����\�b�h
        /// </summary>
        /// <returns>ipAddress�iIP�A�h���X�j</returns>
        public string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); // �z�X�g�����擾

            //�z�X�g����IP�A�h���X���擾�iIPAddress�^�̔z��j
            var ipAddress = Dns.GetHostAddresses(hostName)
                               .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return ipAddress?.ToString() ?? "�s��";
        }

        /// <summary>
        /// �u���E�U�̎�ށE�o�[�W�����EOS �Ȃǂ̏����܂ޕ�������擾���Ԃ����\�b�h
        /// </summary>
        /// <returns>�u���E�U�̎�ށE�o�[�W�����EOS �Ȃǂ̏����܂ޕ�����i�擾�ł��Ȃ������ꍇ�́e�s���f�j</returns>
        public string GetUserAgent()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.Navigate("about:blank"); //WebBrowser �R���g���[����������Ԃɂ���B

                //WebBrowser �R���g���[�����y�[�W�̓ǂݍ��݂���������܂�
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents(); // UI�̉������ێ�

                }

                // WebBrowser �R���g���[���� Document �v���p�e�B���g���āA���݂� HTML �h�L�������g���擾
                dynamic? script = browser.Document?.DomDocument;

                return script?.parentWindow?.navigator?.userAgent ?? "�s��";
            }
        }

        /// <summary>
        /// ���O�C�����悤�Ƃ��Ă���Ј��ԍ��ƃZ�b�V�����g�[�N���̑g�ݍ��킹���g���ăZ�b�V�����e�[�u���ɖ₢���킹���s���A<br/>
        /// �擾�������R�[�h�̏����Z�b�V�����e�[�u���̃f�[�^���f���iSessionModel�j�ɃZ�b�g���ĕԂ����\�b�h
        /// </summary>
        /// <param name="session_token">�Z�b�V�����g�[�N��</param>
        /// <returns>���O�C�����悤�Ƃ��Ă���Ј��ԍ��ƃZ�b�V�����g�[�N���̑g�ݍ��킹��DB�ɖ₢���킹�����ʂ��i�[����SessionModel�N���X�^�I�u�W�F�N�g</returns>
        private SessionModel? GetSessionRecordData(string? session_token)
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

        /// <summary>
        /// SessionManager�N���X�̃t�B�[���h�Ƀf�[�^���Z�b�g���郁�\�b�h
        /// </summary>
        /// <param name="sessionModel">SessionModel�N���X�^�I�u�W�F�N�g</param>
        private void SetSession(SessionModel? sessionModel)
        {
            if (sessionModel != null)
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
}