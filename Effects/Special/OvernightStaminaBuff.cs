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
    /// Grants a time-limited buff to max stamina
    /// if the player slept with the clothing item on
    /// </summary>
    class OvernightStaminaBuff : SingleEffect
    {
        string source;
        int amount;

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.Energy, $"Begin your day with + {amount} max. Energy");        

        public OvernightStaminaBuff(string source, int amount)
        {
            this.source = source;
            this.amount = amount;
        }

        public override void Apply(Farmer farmer, EffectChangeReason reason)
        {
            if (reason == EffectChangeReason.DayStart)
            {
                // create & give buff to player
                Buff staminaBuff = new Buff(0, 0, 0, 0, 0, 0, 0, amount, 0, 0, 0, 0, 5, source, "");
                staminaBuff.addBuff();
            }
        }

        public override void Remove(Farmer farmer, EffectChangeReason reason)
        {
            // nothing to do
        }
    }
}
