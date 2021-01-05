using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppSight.AutoUpdate;
using AppSight.Extensions.System;
using AppSight.FileHashChecker.Library.Command;
using AppSight.Security.Cryptography;

namespace AppSight.FileHashChecker.Windows
{
    public partial class Form1 : Form
    {
        private string _resultMessageBodyTemplate { get; } = "" +
            "%HashType%:\r\n" +
            "%HashString%\r\n" +
            "\r\n" +
            "FilePath:\r\n" +
            "%FilePath%\r\n" +
            "\r\n" +
            "%Message%";
        private CommandArgumentsParser _commandArgumentsParser { get; }
        private FileHashCalculator _fileHashCalculator { get; }
        private HttpClient _httpClient { get; }
        private IUpdateManager _updateManager { get; }
        private ResourceManager _resourceManager { get; }

        public Form1(
            CommandArgumentsParser commandArgumentsParser,
            FileHashCalculator fileHashCaluculator,
            IUpdateManager updateManager)
        {
            _commandArgumentsParser = commandArgumentsParser ?? throw new ArgumentNullException(nameof(commandArgumentsParser));
            _fileHashCalculator = fileHashCaluculator ?? throw new ArgumentNullException(nameof(fileHashCaluculator));
            _updateManager = updateManager ?? throw new ArgumentNullException(nameof(updateManager));
            // for debugging localization
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            InitializeComponent();

            _updateManager.UpdateFound += new EventHandler<UpdateFoundEventArgs>(HandleUpdateFound);
            _resourceManager = new ResourceManager(typeof(Form1));
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Minimize();
            Text = $"{Application.ProductName} {Application.ProductVersion}";
            var args = Environment.GetCommandLineArgs();
            var commandArguments = _commandArgumentsParser.Parse(args);
            

            var fileHash = _fileHashCalculator.Calculate(
                commandArguments.Options.FilePath,
                commandArguments.Options.HashType);
            var fileHashString = fileHash.ComputedHash.ToHashString();
            var resultMessageBody = _resultMessageBodyTemplate
                .Replace("%HashType%", fileHash.HashType.ToString())
                .Replace("%HashString%", fileHashString)
                .Replace("%FilePath%", fileHash.Path)
                .Replace("%Message%", _resourceManager.GetString("ComputedHashMessage"));
            var dialogResult = MessageBox.Show(
                resultMessageBody,
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                Clipboard.SetData(DataFormats.Text, fileHashString);
                MessageBox.Show(
                    _resourceManager.GetString("CopiedMessage"),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            await _updateManager.CheckForUpdatesAsync();

            Application.Exit();
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void HandleUpdateFound(object sender, UpdateFoundEventArgs args)
        {
            var dialogResult = MessageBox.Show(
                string.Format(_resourceManager.GetString("UpdateFoundMessageFormat"), args.Version),
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = args.Uri,
                    UseShellExecute = true,
                    ErrorDialog = true,
                };
                Process.Start(processStartInfo);
            }
        }
    }
}
