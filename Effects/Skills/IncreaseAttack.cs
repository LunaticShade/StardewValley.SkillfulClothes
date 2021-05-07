using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseAttack : ChangeSkillEffect
    {
        protected override EffectIcon Icon => EffectIcon.Attack;

        public override string SkillName => "Attack";        

        protected override void ChangeCurrentLevel(Farmer farmer, int amount) => farmer.attack = Math.Max(0, farmer.attack + amount);

        public IncreaseAttack(int amount)
            : base(amount)
        {
            // --
        }
    }
}
