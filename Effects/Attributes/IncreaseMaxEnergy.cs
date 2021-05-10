using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace SkillfulClothes.Effects.Attributes
{
    class IncreaseMaxEnergy : ChangeAttributeMaxEffect
    {
        public override string AttributeName => "Energy";

        public override EffectIcon Icon => EffectIcon.MaxEnergy;

        public IncreaseMaxEnergy(int amount)
            : base(amount)
        {
            // --
        }

        protected override int GetCurrentValue(Farmer farmer) => (int)farmer.stamina;

        protected override void SetCurrentValue(Farmer farmer, int newValue) => farmer.stamina = newValue;

        protected override int GetMaxValue(Farmer farmer) => farmer.MaxStamina;

        protected override void SetMaxValue(Farmer farmer, int newValue) => farmer.MaxStamina = newValue;
    }
}
