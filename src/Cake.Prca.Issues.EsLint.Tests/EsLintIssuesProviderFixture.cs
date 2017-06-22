namespace Cake.Prca.Issues.EsLint.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Core.Diagnostics;
    using Testing;

    public class EsLintIssuesProviderFixture
    {
        public EsLintIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Prca.Issues.EsLint.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Settings =
                        EsLintIssuesSettings.FromContent(
                            sr.ReadToEnd(),
                            new JsonFormat(this.Log));
                }
            }

            this.PrcaSettings =
                new ReportIssuesToPullRequestSettings(@"c:\Source\Cake.Prca");
        }

        public FakeLog Log { get; set; }

        public EsLintIssuesSettings Settings { get; set; }

        public ReportIssuesToPullRequestSettings PrcaSettings { get; set; }

        internal EsLintIssuesProvider Create()
        {
            var provider = new EsLintIssuesProvider(this.Log, this.Settings);
            provider.Initialize(this.PrcaSettings);
            return provider;
        }

        internal IEnumerable<ICodeAnalysisIssue> ReadIssues()
        {
            var codeAnalysisProvider = this.Create();
            return codeAnalysisProvider.ReadIssues(PrcaCommentFormat.PlainText);
        }
    }
}
