using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    class NullEffect : IEffect
    {            
        public List<EffectDescriptionLine> EffectDescription => new List<EffectDescriptionLine>() { new EffectDescriptionLine(EffectIcon.None, "Does nothing") };

        public void Apply(Item sourceItem, EffectChangeReason reason)
        {
            // --
        }        

        public void Remove(Item sourceItem, EffectChangeReason reason)
        {
            // --
        }
    }
}
