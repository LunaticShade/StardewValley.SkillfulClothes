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
        List<EffectDescriptionLine> EffectDescription { get; }
        void Apply(Farmer farmer, EffectChangeReason reason);
        void Remove(Farmer farmer, EffectChangeReason reason);
    }
}
