using MEC;
using Synapse;
using UnityEngine;

namespace Vent173_Synapse_
{
    class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.DoorInteractEvent += Map_DoorInteractEvent;
            Server.Get.Events.Round.RoundStartEvent += Round_RoundStartEvent;
            Server.Get.Events.Round.RoundEndEvent += Round_RoundEndEvent;
            Server.Get.Events.Player.PlayerDamageEvent += Player_PlayerDamageEvent;
        }

        private void Player_PlayerDamageEvent(Synapse.Api.Events.SynapseEventArguments.PlayerDamageEventArgs ev)
        {
            if(ev.Killer.RoleType == RoleType.Scp173 && ev.Killer.Invisible)
            {
                ev.DamageAmount = 0f;
            }
        }

        private void Round_RoundEndEvent()
        {
            foreach (CoroutineHandle coroutine in Plugin.Coroutine)
                Timing.KillCoroutines(coroutine);
        }

        private void Round_RoundStartEvent()
        {
            foreach (var spp in RoleType.Scp173.GetPlayers())
                spp.SendBroadcast(3, "You can vent by typing `.vent` in the `~` console!");
        }

        private void Map_DoorInteractEvent(Synapse.Api.Events.SynapseEventArguments.DoorInteractEventArgs ev)
        {
            if (ev.Player.RoleType != RoleType.Scp173 || !ev.Player.Invisible)
                return;

            if (Vector3.Distance(ev.Player.Position, ev.Door.Position) >= 1.5f)
            {
                ev.Allow = false;
                ev.Player.Position += ev.Player.gameObject.transform.forward * 3.5f;
            }
        }
    }
}
