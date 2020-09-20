using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using fr34kyn01535.Uconomy;

namespace F.ExperienceUI
{
    public class Main : RocketPlugin<Config>
    {
        public static Main Instance;

        protected override void Load()
        {
            Main.Instance = this;


            Logger.Log("####################################################", ConsoleColor.Red);
            Logger.Log("#           F.Economy/ExperienceUI Loaded          #", ConsoleColor.Red);
            Logger.Log("#                 Plugin By: Feli                  #", ConsoleColor.Red);
            Logger.Log("#    Discord Support:https://discord.gg/6zQVJ9p    #", ConsoleColor.Red);
            Logger.Log("#                 Plugin Ver: 3.0.0                #", ConsoleColor.Red);
            Logger.Log("####################################################", ConsoleColor.Red);


            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            if (this.Configuration.Instance.UconomyMode == true)
            {
                Uconomy.Instance.OnBalanceUpdate += UconomyOnBalanceUpdate;
            }
            else { UnturnedPlayerEvents.OnPlayerUpdateExperience += Events_OnPlayerUpdateExperience; }
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            if (this.Configuration.Instance.UconomyMode == false)
            {
                EffectManager.sendUIEffect(this.Configuration.Instance.ExperienceUI, (short)32401, player.CSteamID, true, this.Configuration.Instance.EconomySymbol + player.Experience.ToString());
            }
            else { EffectManager.sendUIEffect(this.Configuration.Instance.ExperienceUI, (short)32401, player.CSteamID, true, this.Configuration.Instance.EconomySymbol + Uconomy.Instance.Database.GetBalance(player.Id).ToString()); }
            EffectManager.sendUIEffect(this.Configuration.Instance.ServerNameUI, (short)32403, player.CSteamID, true, this.Configuration.Instance.ServerName.ToString());
        }

        private void Events_OnPlayerUpdateExperience(UnturnedPlayer player, uint experience)
        {
            EffectManager.sendUIEffect(this.Configuration.Instance.ExperienceUI, (short)32401, player.CSteamID, true, this.Configuration.Instance.EconomySymbol + player.Experience.ToString());
        }

        private void UconomyOnBalanceUpdate(UnturnedPlayer player, decimal v)
        {
            EffectManager.sendUIEffect(this.Configuration.Instance.ExperienceUI, (short)32401, player.CSteamID, true, this.Configuration.Instance.EconomySymbol + Uconomy.Instance.Database.GetBalance(player.Id).ToString());
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            EffectManager.askEffectClearByID(this.Configuration.Instance.ExperienceUI, player.CSteamID);
            EffectManager.askEffectClearByID(this.Configuration.Instance.ServerNameUI, player.CSteamID);
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateExperience -= Events_OnPlayerUpdateExperience;
            Uconomy.Instance.OnBalanceUpdate -= UconomyOnBalanceUpdate;
        }
    }
}
