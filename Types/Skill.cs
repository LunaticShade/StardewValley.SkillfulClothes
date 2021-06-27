using SkillfulClothes.Effects;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
	public enum Skill
	{
		Cultivo = 0,
		Pesca = 1,
		Coleta = 2,
        Mineração = 3,						
		Combate = 4,	
		Sorte = 5,
    }

    public static class SkilLExtensions
    {
		public static EffectIcon GetIcon(this Skill skill)
        {
            switch (skill)
            {
                case Skill.Cultivo: return EffectIcon.SkillFarming;
                case Skill.Pesca: return EffectIcon.SkillFishing;
                case Skill.Coleta: return EffectIcon.SkillForaging;
                case Skill.Mineração: return EffectIcon.SkillMining;
                case Skill.Combate: return EffectIcon.SkillCombat;
                case Skill.Sorte: return EffectIcon.SkillLuck;
                default: return EffectIcon.None;
            }
        }

        public static int GetCurrentLevel(this Skill skill)
        {
            switch (skill)
            {
                case Skill.Combate: return Game1.player.CombatLevel;
                case Skill.Pesca: return Game1.player.FishingLevel;
                case Skill.Cultivo: return Game1.player.FarmingLevel;
                case Skill.Mineração: return Game1.player.MiningLevel;
                case Skill.Coleta: return Game1.player.ForagingLevel;
                default: return 0;
            }
        }
    }
}
