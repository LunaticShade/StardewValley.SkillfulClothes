using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Buffs
{
    /// <summary>
    /// Increases the player's max health
    /// </summary>
    class MaxHealthBuff : CustomBuff
    {
        int amount;

        public MaxHealthBuff(string id, int amount, int durationMinutes, string source)
            : base(id, $"+{amount} max. Health", source, durationMinutes)
        {
            this.amount = amount;

            iconTexture = EffectHelper.Textures.LooseSprites;
            iconSheetIndex = 2; // new Rectangle(0, 10, 16, 16), 4f
        }

        public override void OnAdded()
        {
            bool wasFullHealth = Game1.player.maxHealth == Game1.player.health;
            Game1.player.maxHealth += amount;

            if (wasFullHealth)
            {
                Game1.player.health = Game1.player.maxHealth;
            }
        }

        public override void OnRemoved()
        {
            Game1.player.maxHealth = Math.Max(1, Game1.player.maxHealth - amount);
            Game1.player.health = Math.Min(Game1.player.health, Game1.player.maxHealth);
            
        }
    }
}
