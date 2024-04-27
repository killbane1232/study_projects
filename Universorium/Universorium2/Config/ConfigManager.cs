using Universorium.Config.DataTypes;

namespace Universorium.Config
{
    public static class ConfigManager
    {
        private static Dictionary<string, Configuration> configs;
        
        public static void Init()
        {
            configs = [];
            if (!Directory.Exists("./config"))
                return;
            var files = Directory.EnumerateFiles("./config");
            List<string> debugFiles = [];
            List<string> releaseFiles = [];
            foreach (var file in files)
            {
                if (file.EndsWith(".local"))
                    debugFiles.Add(file);
                else
                    releaseFiles.Add(file);
            }
            List<string> allFiles = [];
#if DEBUG
            
#else

#endif
        }

        public static Configuration getConfig(string name)
        {
            if (configs == null || !configs.TryGetValue(name, out var res))
            {
                return new Configuration();
            }
            return res;
        }
    }
}
