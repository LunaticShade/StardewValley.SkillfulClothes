using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseSkillLevel : ChangeSkillEffect
    {
        Skill skill;

        public override string SkillName => skill.ToString();

        EffectIcon icon;
        protected override EffectIcon Icon => icon;        

        protected override void ChangeCurrentLevel(Farmer farmer, int amount)
        {
            switch (skill)
            {
                case Skill.Cultivo: farmer.addedFarmingLevel.Value = Math.Max(0, farmer.addedFarmingLevel + amount); break;
                case Skill.Pesca: farmer.addedFishingLevel.Value = Math.Max(0, farmer.addedFishingLevel + amount); break;
                case Skill.Coleta: farmer.addedForagingLevel.Value = Math.Max(0, farmer.addedForagingLevel + amount); break;
                case Skill.Mineração: farmer.addedMiningLevel.Value = Math.Max(0, farmer.addedMiningLevel + amount); break;
                case Skill.Combate: farmer.addedCombatLevel.Value = Math.Max(0, farmer.addedCombatLevel + amount); break;
                case Skill.Sorte: farmer.addedLuckLevel.Value = Math.Max(0, farmer.addedLuckLevel + amount); break;
            }
        }

        public IncreaseSkillLevel(Skill skill, int amount)
            : base(amount)
        {
            this.skill = skill;
            icon = skill.GetIcon();
        }
    }
}
