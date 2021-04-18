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
                if (__instance is Clothing clothing && clothing.dyeable)
                {
                    // the game adds "Dyeable" at the bottom -> put effects above
                    y -= (int)Math.Max(font.MeasureString("Dyeable").Y, 42f) + 10;
                }

                // we are already at the bottom of the tooltip, so go back to the start of the effect slines
                y -= (int)(Math.Max(font.MeasureString("TT").Y, 32f) + 5) * effect.EffectDescription.Count;

                EffectHelper.drawTooltip(effect, spriteBatch, ref x, ref y, font, alpha, overrideText);                
            }
        }

        static int getDescriptionWidth(int __result, Item __instance)
        {
             if (PredefinedEffects.GetEffect(__instance, out IEffect effect))
             {
                 return Math.Max(__result, EffectHelper.getDescriptionWidth(effect));
             }

             return __result;
        }
    }
}
