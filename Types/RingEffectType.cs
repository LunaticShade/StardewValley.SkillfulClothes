using SkillfulClothes.Effects;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    enum RingEffectType
    {
        SmallGlowRing = 516,
        GlowRing = 517,
        YobaRing = 524
    }

    static class RingEffectTypeExtensions
    {
        public static EffectDescriptionLine GetEffectDescription(this RingEffectType ring)
        {
            switch (ring)
            {
                case RingEffectType.SmallGlowRing:
                    return new EffectDescriptionLine(EffectIcon.None, "Emits a constant light");
                case RingEffectType.YobaRing:
                    return new EffectDescriptionLine(EffectIcon.Yoba, "Occasionally shields the wearer from damage");
                default:
                    return new EffectDescriptionLine(EffectIcon.None, "Unknown effect");
            }       
        }
    }
}
