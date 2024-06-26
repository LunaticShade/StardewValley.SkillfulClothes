﻿using SkillfulClothes.Effects;
using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
#if DEBUG
    /// <summary>
    /// A Helper effect with arbitrary effects used for debugging
    /// </summary>
    class DebugEffect : ParameterlessSingleEffect
    {
        List<EffectDescriptionLine> effectDescription = new List<EffectDescriptionLine>() { new EffectDescriptionLine(EffectIcon.Money, "Arbitrary effects used for debugging") };

        public override List<EffectDescriptionLine> EffectDescription => effectDescription;

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            
        }
    }
#endif
}
