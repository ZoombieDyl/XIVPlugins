﻿using Dalamud.Game.Command;
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
        [PluginService] public static IScreenshotProvider ScreenshotProvider { get; private set; } = null!;

        public AutoSelfie()
        {
            CommandManager.AddHandler("/autoselfie", new CommandInfo(OnCommand)
            {
                HelpMessage = "Run AutoSelfie: set camera, emote, and take screenshots."
            });
        }

        public void Dispose()
        {
            CommandManager.RemoveHandler("/autoselfie");
        }

        private void OnCommand(string command, string args)
        {
            ChatGui.Print("Running AutoSelfie!");

            CameraManager.SetCamera(
                yawDegrees: 180.0f,   // facing backward
                pitchDegrees: -10.0f, // slightly up
                zoomDistance: 3.0f    // selfie distance
            );

            SendEmote("/visage"); // or any emote you want

            Task.Run(async () =>
            {
                await Task.Delay(2000); // wait for emote animation

                for (int i = 0; i < 5; i++)
                {
                    SimulatePrintScreen();
                    await Task.Delay(1000);
                }
            });
        }

        private void SendEmote(string emoteCommand)
        {
            ChatGui.Print($"> {emoteCommand}");
            ChatGui.SendMessage(emoteCommand);
        }

        private void SimulatePrintScreen()
        {
            ScreenshotProvider.TakeScreenshot();
        }
    }
}
