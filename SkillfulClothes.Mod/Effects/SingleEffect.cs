using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    abstract class SingleEffect<TParameters> : CustomizableEffect<TParameters>
        where TParameters: IEffectParameters, new()
    {        

        List<EffectDescriptionLine> effectDescription;
        public override List<EffectDescriptionLine> EffectDescription
        {
            get
            {
                if (effectDescription == null)
                {
                    effectDescription = new List<EffectDescriptionLine>() { GenerateEffectDescription() };
                }

                return effectDescription;
            }
        }

        public SingleEffect(TParameters parameters)
            : base(parameters)
        {
            // --
        }

        protected abstract EffectDescriptionLine GenerateEffectDescription();
    }

    abstract class ParameterlessSingleEffect : CustomizableEffect<NoEffectParameters>
    {
        public ParameterlessSingleEffect()
            : base(NoEffectParameters.Default)
        {
            // --
        }
    }
}
