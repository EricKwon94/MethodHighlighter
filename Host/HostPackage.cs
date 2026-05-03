using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Host
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(HostPackage.PackageGuidString)]
    public sealed class HostPackage : AsyncPackage
    {
        public const string PackageGuidString = "f4168b27-46ce-4da6-9a0a-9eb0ae508beb";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        }
    }
}
