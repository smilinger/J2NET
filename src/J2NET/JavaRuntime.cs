using System.Diagnostics;
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

            var args = $"{value} {arguments}".Trim();
            var startInfo = new ProcessStartInfo(runtimePath, args)
            {
#if RELEASE
                UseShellExecute = false,
                CreateNoWindow = true,
#endif
            };

            return Process.Start(startInfo);
        }
    }
}
