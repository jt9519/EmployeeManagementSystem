using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8602

namespace EmployeeManagementSystem.Utils
{
    public static class CloseFormHelper
    {

        /// <summary>
        /// formNameToExcludeと一致する名前のフォーム（ログインフォーム）以外を閉じるメソッド
        /// </summary>
        /// <param name="formNameToExclude">残しておきたいFormのファイル名（このシステムでは現状formNameToExcludeにセットされる文字列は'LoginForm'のみ）</param>
        public static void CloseSpecificForm(string formNameToExclude)
        {
            Form? loginForm = null; // LoginFormインスタンスを保持する変数

            // ログインフォームのUIスレッド外か（Application.OpenForms[0]：ログインフォーム）
            if (Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].Invoke((Action)(() =>
                {
                    // フォームの列挙と処理を開いているフォームの後ろから行う
                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        var form = Application.OpenForms[i]; // i 番目のフォームを form 変数に格納

                        //formがnullではないか
                        if (form != null)
                        {
                            //formの名前がformNameToExcludeと同じか（このシステムでは現状formNameToExcludeにセットされる文字列は'LoginForm'のみ）
                            if (form.Name == formNameToExclude)
                            {
                                //LoginFormを保持
                                loginForm = form;
                            }
                            else
                            {
                                // 他のフォームを閉じる
                                form.Close();
                            }
                        }
                    }
                    // LoginFormが見つかれば再表示
                    loginForm?.Show();
                }));
            }
            else
            {
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    var form = Application.OpenForms[i];

                    if (form != null)
                    {
                        if (form.Name == formNameToExclude)
                        {
                            loginForm = form;
                        }
                        else
                        {
                            form.Close();
                        }
                    }
                }
                loginForm?.Show();
            }
        }
    }
}
