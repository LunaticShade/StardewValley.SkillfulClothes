using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewValley;

namespace SkillfulClothes.Effects.Attributes
{
    class StaminaRegen : AttributeRegenEffect
    {

        public StaminaRegen()
            : base(Color.Green, 5, 1, 1)
        {
            // --
        }

        protected override string AttributeName => "Energy";

        public override EffectIcon Icon => EffectIcon.Energy;

        protected override int GetCurrentValue(Farmer farmer) => (int)farmer.stamina;

        protected override int GetMaxValue(Farmer farmer) => farmer.MaxStamina;

        protected override void SetCurrentValue(Farmer farmer, int newValue) => farmer.Stamina = newValue;
    }
}
