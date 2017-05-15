namespace Cake.Prca.Issues.EsLint
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Provider for code analysis issues reported by ESLint.
    /// </summary>
    internal class EsLintProvider : CodeAnalysisProvider
    {
        private readonly EsLintSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public EsLintProvider(ICakeLog log, EsLintSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<ICodeAnalysisIssue> InternalReadIssues(PrcaCommentFormat format)
        {
            return this.settings.Format.ReadIssues(this.PrcaSettings, this.settings);
        }
    }
}
