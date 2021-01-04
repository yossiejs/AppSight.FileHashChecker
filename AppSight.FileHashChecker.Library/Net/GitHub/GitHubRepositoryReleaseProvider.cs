using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppSight.FileHashChecker.Library.Net.GitHub
{
    public class GitHubRepositoryReleaseProvider : IGitHubRepositoryReleaseProvider
    {
        private HttpClient _httpClient { get; }

        public GitHubRepositoryReleaseProvider(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(GitHubRepositoriesApiConstants.BaseUri);
        }

        public async Task<IEnumerable<GitHubRepositoryRelease>> GetReleasesAsync(
            string uri,
            CancellationToken cancellationToken = default)
        {
            using (var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            })
            {
                using (var response = await _httpClient.SendAsync(
                    request,
                    cancellationToken).ConfigureAwait(false))
                {
                    var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<IEnumerable<GitHubRepositoryRelease>>(responseText);
                }
            }
        }
    }
}
