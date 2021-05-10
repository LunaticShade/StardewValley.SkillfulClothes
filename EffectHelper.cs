﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Effects;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
    static class EffectHelper
    {
        public const int toolTipBottomPadding = 6;
        public const int iconSpace = 52;

        public static IModHelper ModHelper { get; private set; }

        public static SkillfulClothesConfig Config { get; private set; }

        public static Random Random { get; } = new Random();

        public static EffectHelperEvents Events { get; } = new EffectHelperEvents();

        public static ModTextures Textures { get; } = new ModTextures();

        public static void Init(IModHelper modHelper, SkillfulClothesConfig config)
        {
            ModHelper = modHelper;
            Config = config;

            Textures.Init();

            Events.Watch(modHelper);            
        }

        private static void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady) return;
        }

        /// <summary>
        /// Draws icons and textual description of the given effect
        /// </summary>
        public static void drawTooltip(IEffect effect, SpriteBatch spriteBatch, ref int x, ref int y, SpriteFont font, float alpha, StringBuilder overrideText)
        {
            y -= EffectHelper.toolTipBottomPadding;

            List<EffectDescriptionLine> descr = effect.EffectDescription;
            foreach (EffectDescriptionLine line in descr)
            {
                line.Icon.Draw(spriteBatch, new Vector2(x + 16 + 4, y + 16 + 4 + 2));
                Utility.drawTextWithShadow(spriteBatch, line.Text, font, new Vector2(x + 16 + 52 - 10, y + 16 + 7), Game1.textColor * 0.9f * alpha);
                y += (int)Math.Max(font.MeasureString("TT").Y, 36f);
            }
        }

        /// <summary>
        /// Calculate the additional size we need to display the given effect's icon and description
        /// </summary>
        public static Point getExtraSpaceNeededForTooltipSpecialIcons(IEffect effect, SpriteFont font, int minWidth, int horizontalBuffer, int startingHeight, StringBuilder descriptionText, string boldTitleText, int moneyAmountToDisplayAtBottom)
        {
            Point dimensions = new Point(0, startingHeight);
            List<EffectDescriptionLine> descr = effect.EffectDescription;
            int extra_rows_needed = descr.Count;
            dimensions.X = (int)Math.Max(minWidth, descr.Max(line => font.MeasureString(line.Text).X + (float)horizontalBuffer));
            dimensions.Y += extra_rows_needed * Math.Max((int)font.MeasureString("TT").Y, 36);

            return dimensions;
        }

        public static int getDescriptionWidth(IEffect effect)
        {
            return iconSpace + (int)effect.EffectDescription.Max(x => Game1.dialogueFont.MeasureString(x.Text).X);
        }
    }

    /// <summary>
    /// Additional events to which effects can register
    /// </summary>
    class EffectHelperEvents
    {
        int? lastPlayerAddedSpeed = 0;

        /// <summary>
        /// Raised when the game resetted the player's addedSpeed value to 0
        /// </summary>
        public event EventHandler PlayerSpeedWasReset;

        protected void RaisePlayerSpeedWasReset()
        {
            Logger.Debug("RaisePlayerSpeedWasReset");
            PlayerSpeedWasReset?.Invoke(this, EventArgs.Empty);
        }

        public void Watch(IModHelper modHelper)
        {
            modHelper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        }

        private void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            if (lastPlayerAddedSpeed.HasValue)
            {
                if (lastPlayerAddedSpeed > 0 && Game1.player.addedSpeed == 0)
                {
                    RaisePlayerSpeedWasReset();
                }
            }

            lastPlayerAddedSpeed = Game1.player.addedSpeed;
        }
    }
}
