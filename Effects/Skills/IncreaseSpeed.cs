using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillfulClothes.Types;
using StardewValley;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseSpeed : ChangeSkillEffect
    {
        public IncreaseSpeed(int amount) 
            : base(amount)
        {
            // --
        }

        protected override EffectIcon Icon => EffectIcon.Speed;

        public override string SkillName => "Speed";        

        protected override void ChangeCurrentLevel(Farmer farmer, int amount) => farmer.addedSpeed = Math.Max(0, farmer.addedSpeed + amount);

        public override void Apply(Farmer farmer, EffectChangeReason reason)
        {
            base.Apply(farmer, reason);

            // if the game resets the speed value, reapply the effect
            EffectHelper.Events.PlayerSpeedWasReset -= Events_PlayerSpeedWasReset;
            EffectHelper.Events.PlayerSpeedWasReset += Events_PlayerSpeedWasReset; 
        }

        private void Events_PlayerSpeedWasReset(object sender, EventArgs e)
        {
            base.Apply(Game1.player, EffectChangeReason.Reset);
        }

        public override void Remove(Farmer farmer, EffectChangeReason reason)
        {
            base.Remove(farmer, reason);

            EffectHelper.Events.PlayerSpeedWasReset -= Events_PlayerSpeedWasReset;
        }
    }
}
