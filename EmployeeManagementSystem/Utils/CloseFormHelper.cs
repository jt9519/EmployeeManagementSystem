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
        public static void CloseSpecificForm(string formNameToExclude)
        {
            Form? loginForm = null; // LoginFormインスタンスを保持する変数

            // Invokeを使用してUIスレッド上で操作を実行
            if (Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].Invoke((Action)(() =>
                {
                    // フォームの列挙と処理
                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        var form = Application.OpenForms[i];
                        if (form != null)
                        {
                            if (form.Name == formNameToExclude) // LoginFormを保持
                            {
                                loginForm = form;
                            }
                            else
                            {
                                form.Close(); // 他のフォームを閉じる
                            }
                        }
                    }

                    // LoginFormが見つかれば再表示
                    loginForm?.Show();
                }));
            }
            else
            {
                // UIスレッドで処理を実行
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    var form = Application.OpenForms[i];
                    if (form != null)
                    {
                        if (form.Name == formNameToExclude) // LoginFormを保持
                        {
                            loginForm = form;
                        }
                        else
                        {
                            form.Close(); // 他のフォームを閉じる
                        }
                    }
                }

                // LoginFormが見つかれば再表示
                loginForm?.Show();
            }
        }
    }
}
