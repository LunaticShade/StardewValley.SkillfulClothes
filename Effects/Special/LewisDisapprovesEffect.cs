using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Special
{
    /// <summary>
    /// Special effect for the Trimmed Lucky Shorts
    /// </summary>
    class LewisDisapprovesEffect : SingleEffect
    {
        const int LewisNpcId = 17;        

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.Events.InteractedWithNPC -= Events_InteractedWithNPC;
            EffectHelper.Events.InteractedWithNPC += Events_InteractedWithNPC;
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.Events.InteractedWithNPC -= Events_InteractedWithNPC;
        }

        private void Events_InteractedWithNPC(object sender, InteractedWithNPCEventArgs e)
        {
            if (e.Npc.id == LewisNpcId)
            {
                // player started talking to Lewis
                Game1.addHUDMessage(new HUDMessage("Lewis fortemente desaprova o seu estilo de roupas"));
                Game1.player.changeFriendship(-10, e.Npc);
            }
        }    

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.Person_Lewis, "Talvez evite que Lewis os veja");
    }
}
