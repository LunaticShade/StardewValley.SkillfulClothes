using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void ChangeCurrentLevel(Farmer farmer, int amount) => farmer.addedSpeed += amount;
    }
}
