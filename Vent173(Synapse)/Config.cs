using Synapse.Config;
using System.ComponentModel;

namespace Vent173_Synapse_
{
    public class Config : AbstractConfigSection
    {
        [Description("How long is the cooldown?")]
        public float VentCooldown { get; set; } = 30f;
        [Description("How long is the cooldown on spawn?")]
        public float VentCooldownStart { get; set; } = 40f;
    }
}
