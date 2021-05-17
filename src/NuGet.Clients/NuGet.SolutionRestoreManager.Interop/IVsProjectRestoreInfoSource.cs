// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.SolutionRestoreManager
{
    /// <summary>
    /// 
    /// </summary>
    [ComImport]
    [Guid("35AD5FF2-6AB7-48E9-BCC0-189042410FA6")]
    public interface IVsProjectRestoreInfoSource
    {
        /// <summary>
        /// Project Unique Name.
        /// Must be equivalent to the name provided in the <see cref="IVsSolutionRestoreService3.NominateProjectAsync(string, IVsProjectRestoreInfo2, CancellationToken)"/> or equivalent.
        /// </summary>
        /// <remarks>Never <see langword="null"/>.</remarks>
        string Name { get; }

        /// <summary>
        /// Whether the source needs to do some work that could lead to a nomination.
        /// This method may be called frequently, so it should be very efficient.
        /// </summary> 
        bool HasPendingNomination();

        /// <summary>
        /// NuGet calls this method to wait on a potential nomination.
        /// If the project has no pending restore data, it will return a completed task.
        /// Otherwise, the task will be completed once the project nominates.
        /// The task will be cancelled, if the source decide it no longer needs to nominate (for example: the restore state has no change)
        /// The task will be failed, if the source runs into a problem, so it cannot get correct data to nominate (for exammple: DT build failed)
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task WhenNominated(CancellationToken cancellationToken);
    }
}
