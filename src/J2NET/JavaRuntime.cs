﻿using System.Diagnostics;
using System.IO;
using J2NET.Exceptions;
using J2NET.Utilities;

namespace J2NET
{
    public static class JavaRuntime
    {
        public static Process ExecuteJar(string value, string arguments = null)
        {
            return Execute($"-jar {value}", arguments);
        }

        public static Process ExecuteClass(string value, string arguments = null)
        {
            return Execute($"-cp {value}", arguments);
        }

        public static Process Execute(string value, string arguments = null)
        {
            var runtimePath = PathUtility.GetInstalledJavaPath();
            if (string.IsNullOrEmpty(runtimePath))
                runtimePath = PathUtility.GetRuntimePath();

            if (!Directory.Exists(Path.GetDirectoryName(runtimePath)))
                throw new RuntimeNotFoundException();

            return !string.IsNullOrEmpty(arguments)
                ? Process.Start(runtimePath, $"{value} {arguments}")
                : Process.Start(runtimePath, $"{value}");
        }
    }
}
