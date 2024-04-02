using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    public interface IEffect
    {
        string EffectId { get; }

        List<EffectDescriptionLine> EffectDescription { get; }
        void Apply(Item sourceItem, EffectChangeReason reason);
        void Remove(Item sourceItem, EffectChangeReason reason);        
    }
}
