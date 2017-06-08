namespace Cake.Prca.Issues.EsLint
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition of a ESLint log file format.
    /// </summary>
    public interface ILogFileFormat
    {
        /// <summary>
        /// Gets all code analysis issues.
        /// </summary>
        /// <param name="prcaSettings">General settings to use.</param>
        /// <param name="settings">Settings for code analysis provider to use.</param>
        /// <returns>List of code analysis issues</returns>
        IEnumerable<ICodeAnalysisIssue> ReadIssues(
            ReportIssuesToPullRequestSettings prcaSettings,
            EsLintIssuesSettings settings);
    }
}
