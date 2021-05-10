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

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.MaxEnergy, $"Begin your day with +{amount} max. Energy");        

        public OvernightStaminaBuff(string source, int amount)
        {
            this.source = source;
            this.amount = amount;
        }

        public override void Apply(Farmer farmer, EffectChangeReason reason)
        {
            if (reason == EffectChangeReason.DayStart)
            {
                Logger.Debug("Grant MaxEnergy buff");
                
                // create & give buff to player
                Buff staminaBuff = new Buff(0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 360, source, source);
                // we have to add it as driunk buff, otherwise the game will not correctly remove it when going to bed while the effect is still active
                Game1.buffsDisplay.tryToAddDrinkBuff(staminaBuff);

                farmer.stamina = Math.Min(farmer.maxStamina, farmer.stamina + amount);
                
                // Game1.addHUDMessage(new HUDMessage("You awake eager to get to work."));
            }
        }

        public override void Remove(Farmer farmer, EffectChangeReason reason)
        {
            // nothing to do
        }
    }
}
