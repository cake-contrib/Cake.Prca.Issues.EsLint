namespace Cake.Prca.Issues.EsLint.Tests
{
    using System.Linq;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public class EsLintProviderTests
    {
        public sealed class TheMsBuildCodeAnalysisProviderCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new EsLintProvider(
                        null,
                        EsLintSettings.FromContent(
                            "Foo",
                            new JsonFormat(new FakeLog()))));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new EsLintProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
