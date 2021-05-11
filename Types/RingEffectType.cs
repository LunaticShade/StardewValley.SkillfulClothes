using SkillfulClothes.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    enum RingEffectType
    {
        YobaRing = 524
    }

    static class RingEffectTypeExtensions
    {
        public static EffectDescriptionLine GetEffectDescription(this RingEffectType ring)
        {
            switch (ring)
            {
                case RingEffectType.YobaRing:
                    return new EffectDescriptionLine(EffectIcon.Yoba, "Occasionally shields the wearer from damage");
                default:
                    return new EffectDescriptionLine(EffectIcon.None, "Unknown effect");
            }
        }
    }
}
