using System;
using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.GitHub.PowerShell.Config;
using Ninject;

namespace Illallangi.GitHub.PowerShell
{
    [Cmdlet(VerbsCommon.Get, GitHubCmdlet<T>.Null)]
    public abstract class GitHubCmdlet<T> : NinjectCmdlet<GitHubModule> where T : class
    {
        public GitHubCmdlet()
            : base(new NinjectSettings { AllowNullInjection = true })
        {
        }

        protected override void ProcessRecord()
        {
            try
            {
                this.WriteObject(this.Process(this.Get<T>()), true);
            }
            catch (AggregateException failures)
            {
                foreach (var failure in failures.InnerExceptions)
                {
                    this.WriteError(new ErrorRecord(
                        failure,
                        failure.Message,
                        ErrorCategory.InvalidResult,
                        GitHubConfig.Config));
                }
            }
            catch (Exception failure)
            {
                this.WriteError(new ErrorRecord(
                    failure,
                    failure.Message,
                    ErrorCategory.InvalidResult,
                    GitHubConfig.Config));
            }
        }

        protected abstract IEnumerable<Object> Process(T client);
    }
}