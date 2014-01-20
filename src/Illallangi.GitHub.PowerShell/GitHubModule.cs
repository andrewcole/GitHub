using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

using Common.Logging;
using Common.Logging.Log4Net;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.PowerShell.Config;

using log4net.Config;

using Ninject.Modules;

namespace Illallangi.GitHub.PowerShell
{
    public sealed class GitHubModule : NinjectModule
    {
        public override void Load()
        {
            LogManager.Adapter = new Log4NetLoggerFactoryAdapter(new NameValueCollection { { "configType", "EXTERNAL" } });

            XmlConfigurator.Configure(
                Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream(
                        string.Format(
                            "{0}.Log4Net.config", 
                            Assembly.GetExecutingAssembly().GetName().Name)));

            this.Bind<IGitHubConfig>()
                .ToMethod(
                    cx =>
                    (GitHubConfig)
                    ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)
                        .GetSection("GitHubConfig"));

            this.Bind<ILog>().ToMethod(cx => LogManager.GetLogger(cx.Request.Target.Type));
        }
    }
}