using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillfulClothes.Effects.SharedParameters;
using SkillfulClothes.Types;
using StardewValley;

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

        protected override void ChangeCurrentLevel(Farmer farmer, int amount) => farmer.immunity = Math.Max(0, farmer.immunity + amount);
    }
}
