using System.Collections.Generic;
using MEC;
using Synapse.Api.Plugin;

namespace Vent173_Synapse_
{
    [PluginInformation(
       Author = "TypicalIllusion",
       Description = "Vent173",
       LoadPriority = 0,
       Name = "Vent173",
       SynapseMajor = 2,
       SynapseMinor = 4,
       SynapsePatch = 0,
       Version = "1.5.0"
   )]
    class Plugin : AbstractPlugin
    {
        public static List<CoroutineHandle> Coroutine = new List<CoroutineHandle>();
        [Synapse.Api.Plugin.Config(section = "Vent173")]
        public static Config Config;
        public override void Load()
        {
            SynapseController.Server.Logger.Info("Vent173 Loaded");
            new EventHandlers();
        }
    }
}
