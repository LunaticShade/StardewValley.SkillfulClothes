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
    class IncreaseAttack : ChangeSkillEffect<AmountEffectParameters>
    {
        protected override EffectIcon Icon => EffectIcon.Attack;

        public override string SkillName => "Attack";

        protected override void UpdateEffects(Farmer farmer, BuffEffects targetEffects)
        {
            targetEffects.Attack.Value = Parameters.Amount;
        }

        public IncreaseAttack(AmountEffectParameters parameters)
            : base(parameters)
        {
            // --
        }


        public IncreaseAttack(int amount)
            : base(AmountEffectParameters.With(amount))
        {
            // --
        }
    }
}
