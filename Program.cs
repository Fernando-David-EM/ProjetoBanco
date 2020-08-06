using Banco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var telaLogin = new TelaLogin();
            Application.Run(telaLogin);

            if (telaLogin.UsuarioAutenticado)
            {
                Application.Run(new TelaContas());
            }
        }
    }
}
