using Exiled.API.Interfaces;
using System.ComponentModel;

namespace LockLocker
{
    public class Config : IConfig
    {
        [Description("↓Indicates whether the plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;
    }
}
