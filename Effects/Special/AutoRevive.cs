using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillfulClothes.Types;
using StardewValley;

namespace SkillfulClothes.Effects.Special
{
    class AutoRevive : SingleEffect
    {
        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.SaveFromDeath, "Restore health to 50% once");

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicking += GameLoop_UpdateTicking;
        }

        private void GameLoop_UpdateTicking(object sender, StardewModdingAPI.Events.UpdateTickingEventArgs e)
        {
            Farmer farmer = Game1.player;
            if (farmer.health <= 0)
            {             
                farmer.health = farmer.maxHealth / 2;     
                // remove the shirt (it is consumed)
                farmer.shirtItem.Value = null;
            }
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicking -= GameLoop_UpdateTicking;
        }        
    }
}
