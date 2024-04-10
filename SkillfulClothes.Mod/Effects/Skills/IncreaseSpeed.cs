using SkillfulClothes.Effects.SharedParameters;
using StardewValley.Buffs;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    internal class IncreaseSpeed : ChangeSkillEffect<AmountEffectParameters>
    {
        protected override EffectIcon Icon => EffectIcon.Speed;

        public override string SkillName => "Speed";

        protected override void UpdateEffects(Farmer farmer, BuffEffects targetEffects)
        {
            targetEffects.Speed.Value = Parameters.Amount;
        }

        public IncreaseSpeed(AmountEffectParameters parameters)
            : base(parameters)
        {
            // --
        }


        public IncreaseSpeed(int amount)
            : base(AmountEffectParameters.With(amount))
        {
            // --
        }
    }
}
