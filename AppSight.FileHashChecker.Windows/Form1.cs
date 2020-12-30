using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using AppSight.Extensions.System;
using AppSight.FileHashChecker.Library.Command;
using AppSight.Security.Cryptography;

namespace AppSight.FileHashChecker.Windows
{
    public partial class Form1 : Form
    {
        private string _resultMessageBodyTemplate { get; } = "" +
            "Hash(%HashType%):\r\n" +
            "%HashString%\r\n" +
            "\r\n" +
            "FilePath:\r\n" +
            "%FilePath%\r\n" +
            "\r\n" +
            "%Message%";
        private CommandArgumentsParser _commandArgumentsParser { get; }
        private FileHashCalculator _fileHashCalculator { get; }

        public Form1(
            CommandArgumentsParser commandArgumentsParser,
            FileHashCalculator fileHashCaluculator)
        {
            _commandArgumentsParser = commandArgumentsParser ?? throw new ArgumentNullException(nameof(commandArgumentsParser));
            _fileHashCalculator = fileHashCaluculator ?? throw new ArgumentNullException(nameof(fileHashCaluculator));
            // for debugging localization
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Minimize();
            Text = $"{Application.ProductName} {Application.ProductVersion}";
            var args = Environment.GetCommandLineArgs();
            var commandArguments = _commandArgumentsParser.Parse(args);
            var resourceManager = new ResourceManager(typeof(Form1));

            var fileHash = _fileHashCalculator.Calculate(
                commandArguments.Options.FilePath,
                commandArguments.Options.HashType);
            var fileHashString = fileHash.ComputedHash.ToHashString();
            var resultMessageBody = _resultMessageBodyTemplate
                .Replace("%HashType%", fileHash.HashType.ToString())
                .Replace("%HashString%", fileHashString)
                .Replace("%FilePath%", fileHash.Path)
                .Replace("%Message%", resourceManager.GetString("ComputedHashMessage"));
            var dialogResult = MessageBox.Show(
                resultMessageBody,
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                Clipboard.SetData(DataFormats.Text, fileHashString);
                MessageBox.Show(
                    resourceManager.GetString("CopiedMessage"),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            Application.Exit();
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
    }
}
