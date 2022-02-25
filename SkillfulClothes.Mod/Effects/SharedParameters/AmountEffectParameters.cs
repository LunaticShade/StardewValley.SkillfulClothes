using SkillfulClothes.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.SharedParameters
{
    public class AmountEffectParameters : IEffectParameters
    {
        public int Amount { get; set; } = 1;

        public static AmountEffectParameters With(int amount)
        {
            return new AmountEffectParameters() { Amount = amount };
        }
    }
}
