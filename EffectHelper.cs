using Microsoft.Xna.Framework;
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

        public static IModHelper modHelper;

        public static void Init(IModHelper _modHelper)
        {
            modHelper = _modHelper;
        }

        public static Point getExtraSpaceNeededForTooltipSpecialIcons(IEffect effect, SpriteFont font, int minWidth, int horizontalBuffer, int startingHeight, StringBuilder descriptionText, string boldTitleText, int moneyAmountToDisplayAtBottom)
        {
            Point dimensions = new Point(0, startingHeight);
            List<EffectDescriptionLine> descr = effect.EffectDescription;
            int extra_rows_needed = descr.Count;
            dimensions.X = (int)Math.Max(minWidth, descr.Max(line => font.MeasureString(line.Text).X + (float)horizontalBuffer));
            dimensions.Y += extra_rows_needed * Math.Max((int)font.MeasureString("TT").Y, 48);
            return dimensions;
        }

        public static void drawTooltip(IEffect effect, SpriteBatch spriteBatch, ref int x, ref int y, SpriteFont font, float alpha, StringBuilder overrideText)
        {            
            List<EffectDescriptionLine> descr = effect.EffectDescription;
            foreach (EffectDescriptionLine line in descr)
            {
                line.Icon.Draw(spriteBatch, new Vector2(x + 16 + 4, y + 16 + 4 + 2));
                //Utility.drawTextWithShadow(spriteBatch, line.Text, font, new Vector2(x + 16 + 52 - 10, y + 16 + 7), Game1.textColor * 0.9f * alpha);
                y += (int)Math.Max(font.MeasureString("TT").Y, 36f);
            }
        }

        public static int getDescriptionWidth(IEffect effect)
        {
            // const int iconSpace = 52;
            return /*iconSpace +*/ (int)effect.EffectDescription.Max(x => Game1.dialogueFont.MeasureString(x.Text)).X;
        }

    }
}
