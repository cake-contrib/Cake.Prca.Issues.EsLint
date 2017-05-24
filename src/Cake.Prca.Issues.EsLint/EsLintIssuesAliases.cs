namespace Cake.Prca.Issues.EsLint
{
    using System;
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality related to importing code analysis issues from ESLint
    /// to write them to pull requests.
    /// </summary>
    [CakeAliasCategory(CakeAliasConstants.MainCakeAliasCategory)]
    public static class EsLintIssuesAliases
    {
        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules starting
        /// with the string <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules starting with the string
        /// <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }

        /// <summary>
        /// Gets an instance for the ESLint JSON log format as written by the JSON formatter.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the ESLint JSON log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ILogFileFormat EsLintJsonFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new JsonFormat(context.Log);
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported by ESLint using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of a provider for code analysis issues reported by ESLint.</returns>
        /// <example>
        /// <para>Report code analysis issues reported by ESLint to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         EsLintIssuesFromFilePath(
        ///             new FilePath("C:\build\ESLint.log")),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider EsLintIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            ILogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            return context.EsLintIssues(EsLintIssuesSettings.FromFilePath(logFilePath, format));
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported by ESLint using log file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of a provider for code analysis issues reported by ESLint.</returns>
        /// <example>
        /// <para>Report code analysis issues reported by ESLint to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         EsLintIssuesFromContent(
        ///             logFileContent),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider EsLintIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            ILogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return context.EsLintIssues(EsLintIssuesSettings.FromContent(logFileContent, format));
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported by ESLint using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the ESLint log.</param>
        /// <returns>Instance of a provider for code analysis issues reported by ESLint.</returns>
        /// <example>
        /// <para>Report code analysis issues reported by ESLint to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     var settings =
        ///         new EsLintIssuesSettings(
        ///             new FilePath("C:\build\ESLint.log"));
        ///
        ///     ReportCodeAnalysisIssuesToPullRequest(
        ///         EsLintIssues(settings),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider EsLintIssues(
            this ICakeContext context,
            EsLintIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new EsLintIssuesProvider(context.Log, settings);
        }
    }
}
