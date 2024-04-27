using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universorium.Config.DataTypes
{
    public class LogConfigItem
    {
        public enum LogType
        {
            Directory,
            File,
            Address
        }
        public LogType Type { get; set; }
        public string Path { get; set; } = "";
    }
}
