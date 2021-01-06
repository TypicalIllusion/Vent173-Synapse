using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using MEC;
using Synapse.Api;
using Synapse.Command;

namespace Vent173_Synapse_
{
    [CommandInformation(
    Name = "vent",
    Aliases = new[] { "v" },
    Description = "vent",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".vent"
    )]
    class Command : ISynapseCommand
    {
        public static IEnumerator<float> VentCooldownStart(float duration, Player pp)
        {
            pp.BroadcastMessage("You are preparing your abilities", 2);
            yield return Timing.WaitForSeconds(Plugin.Config.VentCooldownStart);
        }
        public static List<Player> CmdCooldown = new List<Player>();
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (!CmdCooldown.Contains(context.Player))
            {
                if (context.Player.RoleType == RoleType.Scp173)
                {
                    foreach (var effect in context.Player.PlayerEffectsController.AllEffects.Values
                            .Where(x => x.GetType() == typeof(Scp268) || x.GetType() == typeof(Amnesia)))
                        if (!effect.Enabled)
                            context.Player.PlayerEffectsController.EnableEffect(effect, 15);
                        else
                            effect.ServerDisable();
                    context.Player.Invisible = !context.Player.Invisible;
                    context.Player.BroadcastMessage($"You are now {(context.Player.Invisible ? "invisible" : "visible")}!", 5);
                    if (context.Player.Invisible)
                    {
                        Timing.CallDelayed(15f, () =>
                        {
                            context.Player.Invisible = false;
                        });
                    }
                    result.Message = $"You are now {(context.Player.Invisible ? "invisible" : "visible")}!";
                    foreach (Player shitass in Synapse.Server.Get.Players)
                    {
                        if (shitass.PlayerEffectsController.GetEffect<Amnesia>().Enabled && context.Player.RoleType == RoleType.Scp173)
                        {
                            context.Player.Scp173Controller.IgnoredPlayers.Add(shitass);
                        }
                    }
                    Plugin.Coroutine.Add(Timing.RunCoroutine(VentCooldown(Plugin.Config.VentCooldown, context.Player)));
                    CmdCooldown.Add(context.Player);
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }
            else
            {
                context.Player.BroadcastMessage("You are on cooldown!", 2);
            }
            result.Message = "You are not SCP-173";
            result.State = CommandResultState.Error;
            return result;
        }
        public static IEnumerator<float> VentCooldown(float duration, Player pp)
        {
            yield return Timing.WaitUntilTrue(() => !pp.PlayerEffectsController.GetEffect<Amnesia>().Enabled);
            yield return Timing.WaitForSeconds(Plugin.Config.VentCooldown);
            CmdCooldown.Remove(pp);
        }
    }
}
