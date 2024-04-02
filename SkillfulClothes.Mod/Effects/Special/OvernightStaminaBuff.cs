using SkillfulClothes.Effects.Buffs;
using SkillfulClothes.Effects.SharedParameters;
using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Buffs;
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
    class OvernightStaminaBuff : SingleEffect<AmountEffectParameters>
    {
        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.MaxEnergy, $"Begin your day with +{Parameters.Amount} max. Energy");        

        public OvernightStaminaBuff(AmountEffectParameters parameters)
            : base(parameters)
        {
            // --
        }

        public OvernightStaminaBuff(int amount)
            : base(AmountEffectParameters.With(amount))
        {
            // --
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            if (reason == EffectChangeReason.DayStart)
            {
                Logger.Debug("Grant MaxEnergy buff");

                // create & give buff to player
                Buff staminaBuff = new Buff(EffectHelper.GetEffectId(this), null, null, 360);
                staminaBuff.visible = false;
                BuffEffects effects = new BuffEffects();
                effects.MaxStamina.Value = Parameters.Amount;
                staminaBuff.effects.Add(effects);

                Game1.player.buffs.Apply(staminaBuff);

                // Game1.addHUDMessage(new HUDMessage("You awake eager to get to work."));
            }
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            // nothing todo
        }
    }
}
