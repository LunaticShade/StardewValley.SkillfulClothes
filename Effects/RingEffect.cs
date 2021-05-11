using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    /// <summary>
    /// An effect which "simulates" the player wearing a specific ring
    /// </summary>
    class RingEffect : SingleEffect
    {
        public RingEffectType Ring { get; }

        public RingEffect(RingEffectType ring)
        {
            Ring = ring;
        }

        protected override EffectDescriptionLine GenerateEffectDescription() => Ring.GetEffectDescription();

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            // --
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            // --
        }        
    }
}
