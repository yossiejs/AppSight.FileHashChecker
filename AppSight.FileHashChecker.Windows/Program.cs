using System;
using System.Net.Http;
using System.Windows.Forms;
using AppSight.FileHashChecker.Library.Command;
using AppSight.FileHashChecker.Library.Net.GitHub;
using AppSight.FileHashChecker.Library.Net.GitHub.Repositories;
using AppSight.FileHashChecker.Library.Net.Releases;
using AppSight.Security.Cryptography;

namespace AppSight.FileHashChecker.Windows
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
            var gitHubHttpClient = new HttpClient();
            gitHubHttpClient.BaseAddress = new Uri(GitHubRepositoriesApiConstants.BaseUri);
            gitHubHttpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"{Application.ProductName} {Application.ProductVersion}");
            var releaseProvider = new GitHubRepositoryReleaseProvider(
                gitHubHttpClient,
                new GitHubRepositoryReleaseProviderSetting
                {
                    OwnerName = "yossiejs",
                    RepositoryName = "AppSight.FileHashChecker",
                });
            var updateManager = new UpdateManager(
                releaseProvider,
                Application.ProductVersion);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(
                commandArgumentsParser,
                fileHashCalculator,
                updateManager));
        }
    }
}
