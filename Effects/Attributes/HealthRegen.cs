using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace SkillfulClothes.Effects.Attributes
{
    class HealthRegen : AttributeRegenEffect
    {

        public HealthRegen()
            : base(30)
        {
            // --
        }

        protected override string AttributeName => "Health";

        public override EffectIcon Icon => EffectIcon.Health;

        protected override int GetCurrentValue(Farmer farmer) => (int)farmer.health;

        protected override int GetMaxValue(Farmer farmer) => farmer.maxHealth;

        protected override void SetCurrentValue(Farmer farmer, int newValue) => farmer.health = newValue;
    }
}
