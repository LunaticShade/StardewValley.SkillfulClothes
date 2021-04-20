using Harmony;
using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Effects;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
    class HarmonyPatches
    {
        public static void Apply(String modId)
        {
            HarmonyInstance harmony = HarmonyInstance.Create(modId);

            harmony.Patch(
                   original: AccessTools.Method(typeof(Item), nameof(Item.drawTooltip)),
                   postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.drawTooltip_Postfix))
                );

            harmony.Patch(
                  original: AccessTools.Method(typeof(Item), "getDescriptionWidth"),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.getDescriptionWidth))
               );
        }

        static void drawTooltip_Postfix(Item __instance, SpriteBatch spriteBatch, ref int x, ref int y, SpriteFont font, float alpha, StringBuilder overrideText)
        {
            if (PredefinedEffects.GetEffect(__instance, out IEffect effect))
            {                
                // calculate the top point of the textbox
                int textHeight = (int)font.MeasureString(overrideText).Y + 4;
                int topY = y - textHeight;            
                EffectHelper.drawEffectIcons(effect, spriteBatch, x, topY, font, alpha, overrideText);
            }
        }

        static int getDescriptionWidth(int __result, Item __instance)
        {
             // increase the width so that effect descriptions stay on one line and do not break
             if (PredefinedEffects.GetEffect(__instance, out IEffect effect))
             {
                 return Math.Max(__result, EffectHelper.getDescriptionWidth(effect));
             }

             return __result;
        }
    }
}
