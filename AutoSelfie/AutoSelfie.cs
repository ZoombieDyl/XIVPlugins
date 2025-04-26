﻿// AutoSelfie.cs
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using System.Threading.Tasks;

namespace AutoSelfie
{
    public sealed class AutoSelfie : IDalamudPlugin
    {
        public string Name => "AutoSelfie";

        [PluginService] public static ICommandManager CommandManager { get; private set; } = null!;
        [PluginService] public static IChatGui ChatGui { get; private set; } = null!;
        [PluginService] public static IPluginLog Log { get; private set; } = null!;
        [PluginService] public static IFramework Framework { get; private set; } = null!;

        public AutoSelfie()
        {
            CommandManager.AddHandler("/autoselfie", new CommandInfo(OnCommand)
            {
                HelpMessage = "Set camera and take selfies automatically."
            });
        }

        public void Dispose()
        {
            CommandManager.RemoveHandler("/autoselfie");
        }

        private void OnCommand(string command, string args)
        {
            ChatGui.Print("[AutoSelfie] Setting camera and starting!");
            CameraManager.SetSelfieCamera();

            SendEmote("/visage"); // Customize emote here

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                for (int i = 0; i < 5; i++)
                {
                    PluginUtilities.TakeScreenshot();
                    await Task.Delay(1000);
                }
            });
        }

        private void SendEmote(string emoteCommand)
        {
            ChatGui.SendMessage(emoteCommand);
        }
    }
}
