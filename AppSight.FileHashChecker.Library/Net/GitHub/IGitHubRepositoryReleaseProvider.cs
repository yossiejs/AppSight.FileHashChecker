using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppSight.FileHashChecker.Library.Net.GitHub
{
    public interface IGitHubRepositoryReleaseProvider
    {
        Task<IEnumerable<GitHubRepositoryRelease>> GetReleasesAsync(string uri, CancellationToken cancellationToken = default);
    }
}