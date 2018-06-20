﻿using ShaderPlayground.Core.Util;

namespace ShaderPlayground.Core.Compilers.SpirvTools
{
    internal sealed class SpirvStatsCompiler : IShaderCompiler
    {
        public string Name { get; } = CompilerNames.SpirvStats;
        public string DisplayName { get; } = "spirv-stats";
        public string Description { get; } = "Shows statistics from SPIR-V files.";

        public string[] InputLanguages { get; } = { LanguageNames.SpirV };

        public ShaderCompilerParameter[] Parameters { get; } =
        {
            CommonParameters.CreateVersionParameter("spirv-tools")
        };

        public ShaderCompilerResult Compile(ShaderCode shaderCode, ShaderCompilerArguments arguments)
        {
            using (var tempFile = TempFile.FromShaderCode(shaderCode))
            {
                ProcessHelper.Run(
                    CommonParameters.GetBinaryPath("spirv-tools", arguments, "spirv-stats.exe"),
                    $"\"{tempFile.FilePath}\"",
                    out var stdOutput,
                    out var _);

                return new ShaderCompilerResult(
                    true,
                    null,
                    null,
                    new ShaderCompilerOutput("Output", null, stdOutput));
            }
        }
    }
}
