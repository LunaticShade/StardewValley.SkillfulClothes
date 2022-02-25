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

        public MaxHealthBuff(int amount, int durationMinutes, string source)
            : base($"+{amount} max. Health", source, durationMinutes)
        {
            this.amount = amount;
        }

        public override void ApplyCustomEffect()
        {
            bool wasFullHealth = Game1.player.maxHealth == Game1.player.health;
            Game1.player.maxHealth += amount;

            if (wasFullHealth)
            {
                Game1.player.health = Game1.player.maxHealth;
            }
        }

        public override List<ClickableTextureComponent> GetCustomBuffIcons()
        {
            ClickableTextureComponent ctc = new ClickableTextureComponent("", Rectangle.Empty, null, description, EffectHelper.Textures.LooseSprites, new Rectangle(0, 10, 16, 16), 4f);
            return new List<ClickableTextureComponent>() { ctc };
        }

        public override void RemoveCustomEffect(bool clearingAllBuffs)
        {
            Game1.player.maxHealth = Math.Max(1, Game1.player.maxHealth - amount);
            Game1.player.health = Math.Min(Game1.player.health, Game1.player.maxHealth);
            
        }
    }
}
