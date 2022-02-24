using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    public abstract class CustomizableEffect<TParameters> : IEffect
        where TParameters: IEffectParameters, new()
    {         

        public TParameters Parameters { get; private set; }
     

        public abstract List<EffectDescriptionLine> EffectDescription { get; }

        public void SetParameters(TParameters parameters)
        {
            Parameters = parameters;
            ReloadParameters();
        }

        public virtual void ReloadParameters()
        {
            // override if needed
        }

        /**
         * Create this effect with the given parameters
         */
        public CustomizableEffect(TParameters parameters)
        {
            SetParameters(parameters ?? new TParameters());
        }

        /**
         * Create this effect with default parameters
         */
        public CustomizableEffect()
        {
            SetParameters(new TParameters());
        }


        public abstract void Apply(Item sourceItem, EffectChangeReason reason);

        public abstract void Remove(Item sourceItem, EffectChangeReason reason);        
    }
}
