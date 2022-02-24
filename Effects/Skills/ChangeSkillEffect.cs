using SkillfulClothes.Effects.SharedParameters;
using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    abstract class ChangeSkillEffect<TParameters> : SingleEffect<TParameters>
        where TParameters : AmountEffectParameters, new()
    {
        public abstract string SkillName { get; }

        protected abstract EffectIcon Icon { get; }

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(Icon, $"+{Parameters.Amount} {SkillName}");

        protected abstract void ChangeCurrentLevel(Farmer farmer, int amount);        

        public ChangeSkillEffect(TParameters parameters)            
            : base(parameters)
        {
            // --
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            ChangeCurrentLevel(Game1.player, Parameters.Amount);            

            Logger.Debug($"{SkillName} + {Parameters.Amount}");
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            ChangeCurrentLevel(Game1.player, -Parameters.Amount);

            Logger.Debug($"{SkillName} - {Parameters.Amount}");
        }
    }
}
