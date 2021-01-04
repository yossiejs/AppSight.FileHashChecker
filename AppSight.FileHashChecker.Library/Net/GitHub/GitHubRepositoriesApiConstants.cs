using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppSight.FileHashChecker.Library.Net.GitHub
{
    public static class GitHubRepositoriesApiConstants
    {
        public const string BaseUri = "https://api.github.com/";
        public const string ReleasesEndpointFormat = "repos/{0}/{1}/releases";
    }
}
