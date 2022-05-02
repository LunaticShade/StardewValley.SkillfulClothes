using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    /*
     * A set of effects
     */
    public class EffectSet : IEffect
    {

        public IEffect[] Effects { get; }        

        List<EffectDescriptionLine> IEffect.EffectDescription => Effects.SelectMany(x => x.EffectDescription).ToList();        

        private EffectSet(params IEffect[] effects)
        {
            Effects = effects;
        }

        public static EffectSet Of(params IEffect[] effects)
        {
            return new EffectSet(effects);
        }

        public void Apply(Item sourceItem, EffectChangeReason reason)
        {
            foreach(var effect in Effects)
            {
                effect.Apply(sourceItem, reason);
            }
        }

        public void Remove(Item sourceItem, EffectChangeReason reason)
        {
            foreach (var effect in Effects)
            {
                effect.Remove(sourceItem, reason);
            }
        }
    }
}
