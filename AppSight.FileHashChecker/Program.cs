using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppSight.FileHashChecker.Command;
using AppSight.FileHashChecker.Security;

namespace AppSight.FileHashChecker
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var commandArgumentsParser = new CommandArgumentsParser();
            var fileHashCalculator = new FileHashCalculator();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(commandArgumentsParser, fileHashCalculator));
        }
    }
}
