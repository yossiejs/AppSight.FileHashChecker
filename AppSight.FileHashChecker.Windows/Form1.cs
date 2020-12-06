using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppSight.FileHashChecker.Library.Command;
using AppSight.FileHashChecker.Library.Cryptography;

namespace AppSight.FileHashChecker.Windows
{
    public partial class Form1 : Form
    {
        private string _resultMessageBodyTemplate { get; } = "" +
            "%HashType%: %HashString%\r\n" +
            "Path: %FilePath%\r\n" +
            "\r\n" +
            "%ConfirmationString%";
        private CommandArgumentsParser _commandArgumentsParser { get; }
        private FileHashCalculator _fileHashCalculator { get; }

        public Form1(
            CommandArgumentsParser commandArgumentsParser,
            FileHashCalculator fileHashCaluculator)
        {
            _commandArgumentsParser = commandArgumentsParser ?? throw new ArgumentNullException(nameof(commandArgumentsParser));
            _fileHashCalculator = fileHashCaluculator ?? throw new ArgumentNullException(nameof(fileHashCaluculator));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Minimize();
            Text = $"{Application.ProductName} {Application.ProductVersion}";
            string[] args = Environment.GetCommandLineArgs();
            var commandArguments = _commandArgumentsParser.Parse(args);
            var fileHash = _fileHashCalculator.Calculate(
                commandArguments.Options.FilePath,
                commandArguments.Options.HashType);

            var resultMessageBody = _resultMessageBodyTemplate
                .Replace("%HashType%", fileHash.HashType.ToString())
                .Replace("%HashString%", fileHash.ComputedHash.ToHashString())
                .Replace("%FilePath%", fileHash.Path)
                .Replace("%ConfirmationString%", "Whould you like to copy hash to clipboard?"); // TODO: support locale
            var dialogResult = MessageBox.Show(
                resultMessageBody,
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("TODO: impl copy to clipboard!");
            }
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
    }
}
