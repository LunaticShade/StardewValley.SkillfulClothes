using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    abstract class ChangeSkillEffect : SingleEffect
    {
        int amount;        

        public abstract string SkillName { get; }

        protected abstract EffectIcon Icon { get; }

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(Icon, $"+{amount} {SkillName}");

        protected abstract void ChangeCurrentLevel(Farmer farmer, int amount);

        public ChangeSkillEffect(int amount)
        {
            this.amount = amount;
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            ChangeCurrentLevel(Game1.player, amount);            

            Logger.Debug($"{SkillName} + {amount}");
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            ChangeCurrentLevel(Game1.player, - amount);

            Logger.Debug($"{SkillName} - {amount}");
        }
    }
}
