using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Game.ClientState.Party;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Hooking;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Group;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Lua;
using Lumina.Excel.Sheets;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Veda;


namespace AutoSelfie
{
    public sealed class AutoSelfie : IDalamudPlugin
    {
        public string Name => "AutoSeasdasdlfie";

        [PluginService] public static ICommandManager CommandManager { get; private set; } = null!;
        [PluginService] public static IChatGui ChatGui { get; private set; } = null!;
        [PluginService] public static ITargetManager TargetManager { get; private set; } = null!;

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
                yawDegrees: 180.0f,
                pitchDegrees: -10.0f,
                zoomDistance: 3.0f
            );

            SendEmote("/visage");

            Task.Run(async () =>
            {
                await Task.Delay(2000);

                for (int i = 0; i < 5; i++)
                {
                    SimulatePrintScreen();
                    await Task.Delay(1000);
                }
            });
        }

        private void SendEmote(string emoteCommand)
        {
            // Print to your chat (optional for debugging)
            this.chatGui.Print($"> {emoteCommand}");
            // Actually send the emote command
            this.chatGui.SendMessage(emoteCommand);
        }


        private void SimulatePrintScreen()
        {
            SendKeys.SendWait("{PRTSC}");
        }
    }
}
