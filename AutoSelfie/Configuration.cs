﻿using Dalamud.Configuration;
using Dalamud.Plugin;

namespace AutoSelfie
{
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; }
        public bool AutoUpdatePortraitFromGearsetUpdate = true;
        public bool ShowMessageInChatWhenAutoUpdatingPortraitFromGearsetUpdate = true;

        private IDalamudPluginInterface pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.pluginInterface.SavePluginConfig(this);
        }
    }
}