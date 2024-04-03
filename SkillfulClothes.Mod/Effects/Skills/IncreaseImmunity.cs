using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillfulClothes.Effects.SharedParameters;
using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Buffs;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseImmunity : ChangeSkillEffect<AmountEffectParameters>
    {
        public IncreaseImmunity(AmountEffectParameters parameters) 
            : base(parameters)
        {
            // --
        }

        public IncreaseImmunity(int amount)
            : base(AmountEffectParameters.With(amount))
        {
            // --
        }

        public override string SkillName => "Immunity";

        protected override EffectIcon Icon => EffectIcon.Immunity;

        protected override void UpdateEffects(Farmer farmer, BuffEffects targetEffects)
        {            
            targetEffects.Immunity.Value = Parameters.Amount;            
        }
    }
}
