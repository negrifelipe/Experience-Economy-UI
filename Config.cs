using Rocket.API;

namespace F.ExperienceUI
{
    public class Config : IRocketPluginConfiguration
    {
        public ushort ExperienceUI;
        public string EconomySymbol;
        public ushort ServerNameUI;
        public string ServerName;
        public bool UconomyMode;

        public void LoadDefaults()
        {
            this.ExperienceUI = (ushort)32400;
            this.EconomySymbol = "$";
            this.ServerNameUI = (ushort)32402;
            this.ServerName = "Server Name Here";
            UconomyMode = false;
        }
    }
}
