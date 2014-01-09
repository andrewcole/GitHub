using System;
using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.GitHub.Config;
using Ninject;

namespace Illallangi.GitHub.GitHub
{
    [Cmdlet(VerbsCommon.Get, Nouns.Abstract)]
    public abstract class GitHubCmdlet<T> : PSCmdlet, IGitHubClientConfig where T : class
    {
        private StandardKernel currentKernel;
        private GitHubModule currentModule;

        private GitHubModule Module
        {
            get
            {
                return this.currentModule ?? (this.currentModule = this.GetModule());
            }
        }

        private StandardKernel Kernel
        {
            get
            {
                return this.currentKernel ?? (this.currentKernel = this.GetKernel());
            }
        }

        private GitHubModule GetModule()
        {
            return new GitHubModule(this);
        }

        private StandardKernel GetKernel()
        {
            return new StandardKernel(this.Module);
        }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
        public virtual string UserName { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
        public virtual string Token { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                this.WriteObject(this.Process(this.Kernel.Get<T>()), true);
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