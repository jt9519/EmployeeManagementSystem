using EmployeeManagementSystem.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // �t�H�[���T�C�Y���Œ�
            this.ClientSize = new Size(1000, 800); // ��1000, �c800
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // �T�C�Y�ύX�𖳌���
            this.MaximizeBox = false; // �ő剻�{�^���𖳌���
            this.StartPosition = FormStartPosition.CenterScreen; // ��ʒ����ɕ\��

            // �p�X���[�h���̓{�b�N�X�̐ݒ�i�}�X�L���O�j
            txtPassword.UseSystemPasswordChar = true;

            // �{�^���N���b�N�C�x���g
            btnLogin.Click += (s, e) =>
            {
                // ���͒l���擾
                string inputMail = txtMail.Text;
                string inputPassword = txtPassword.Text;

                // ���͌���: ��̏ꍇ�̓G���[���b�Z�[�W��\��
                if (string.IsNullOrEmpty(inputMail) || string.IsNullOrEmpty(inputPassword))
                {
                    MessageBox.Show("���[���A�h���X�ƃp�X���[�h����͂��Ă��������B", "���̓G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
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

                        //var joinedData = dbContext.Operator
                        //    .Join(dbContext.Employee,
                        //    op => op.employee_id,
                        //    e => e.employee_id,
                        //    (op, e) => new { Operator = op, Mail = e.mail })
                        //    .ToList();
                        //MessageBox.Show($"�������ꂽ�f�[�^��: {joinedData.Count}",
                        //    "�f�o�b�O����", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        //MessageBox.Show($"���[��: {inputMail}, �p�X���[�h: {inputPassword}", "���͒l�m�F", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (operatorRecord != null)
                        //{
                        //    MessageBox.Show($"���[��: {operatorRecord.Mail}, �p�X���[�h: {operatorRecord.Operator.Password}",
                        //        "�N�G�����ʊm�F", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("�N�G�����ʂ���ł��B�f�[�^������܂���B",
                        //        "�m�F����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}


                        // SQL���O���擾
                        //string logs = dbContext.GetLogs();

                        // ���b�Z�[�W�{�b�N�X�Ƀ��O��\��
                        //MessageBox.Show(!string.IsNullOrEmpty(logs) ? logs : "���O������܂���", "�������ꂽSQL���O", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (operatorRecord != null)
                        {
                            // �F�ؐ���
                            //MessageBox.Show("���O�C���������܂����B", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowEmployeeInfoForm showEmployeeInfoForm = new ShowEmployeeInfoForm();
                            showEmployeeInfoForm.Show();
                            this.Hide(); // ���O�C���t�H�[�����\��
                        }
                        else
                        {
                            // �F�؎��s
                            MessageBox.Show("���[���A�h���X�܂��̓p�X���[�h������������܂���B", "�F�؃G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // ��O����
                        MessageBox.Show($"�G���[���������܂���: {ex.Message}", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
        }
    }
}