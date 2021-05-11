using Harmony;
using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Effects;
using SkillfulClothes.Effects.Buffs;
using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Patches
{
    class HarmonyPatches
    {
        static HatDrawWrapper hatDrawWrapper = new HatDrawWrapper();
        static ClothingDrawWrapper clothingDrawWrapper = new ClothingDrawWrapper();

        public static void Apply(String modId)
        {
            HarmonyInstance harmony = HarmonyInstance.Create(modId);

            harmony.Patch(
                  original: AccessTools.Method(typeof(Item), "getDescriptionWidth"),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.getDescriptionWidth))
               );

            // patch IClickableMenu.drawToolTip, since Harmony (1.2.0.1) is unable to patch Item.getExtraSpaceNeededForTooltipSpecialIcons() correctly (as it returns a struct)
            // see https://github.com/pardeike/Harmony/issues/159 and https://github.com/pardeike/Harmony/issues/77
            // seems to be fixed in Harmony 2.0.4.0
            harmony.Patch(
                  original: typeof(IClickableMenu).GetMethod(nameof(IClickableMenu.drawToolTip), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public),
                  prefix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.drawToolTip))
                );

            // Patches for RingEffect
            harmony.Patch(
                  original: AccessTools.Method(typeof(Farmer), nameof(Farmer.isWearingRing)),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.isWearingRing))
               );

            // Patches for ICustomBuff
            harmony.Patch(
                  original: AccessTools.Method(typeof(Buff), nameof(Buff.addBuff)),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.addBuff))
               );

            harmony.Patch(
                  original: AccessTools.Method(typeof(Buff), nameof(Buff.removeBuff)),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.removeBuff))
               );

            harmony.Patch(
                  original: AccessTools.Method(typeof(Buff), nameof(Buff.getClickableComponents)),
                  postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.buff_getClickableComponents))
               );

            harmony.Patch(
                  original: AccessTools.Method(typeof(BuffsDisplay), nameof(BuffsDisplay.clearAllBuffs)),
                  prefix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.clearAllBuffs))
                );
        }

        static void drawToolTip(ref Item hoveredItem)
        {
            // replace the hoveredItem with a wrapper class which allows us to
            // control Item.getExtraSpaceNeededForTooltipSpecialIcons
            if (ItemDefinitions.GetEffect(hoveredItem, out IEffect effect))
            {
                if (hoveredItem is Clothing clothing)
                {
                    clothingDrawWrapper.Assign(clothing, effect);
                    hoveredItem = clothingDrawWrapper;
                } else if (hoveredItem is StardewValley.Objects.Hat hat)
                {
                    hatDrawWrapper.Assign(hat, effect);
                    hoveredItem = hatDrawWrapper;
                }
            }
        }

        static int getDescriptionWidth(int __result, Item __instance)
        {
            // increase the width so that effect descriptions stay on one line and do not break
            if (ItemDefinitions.GetEffect(__instance, out IEffect effect))
            {
                return Math.Max(__result, EffectHelper.getDescriptionWidth(effect));
            }

            return __result;
        }

        static void addBuff(Buff __instance)
        {
            if (__instance is ICustomBuff cb)
            {
                cb.ApplyCustomEffect();
            }
        }

        static void removeBuff(Buff __instance)
        {
            if (__instance is ICustomBuff cb)
            {
                cb.RemoveCustomEffect(false);
            }
        }

        static List<ClickableTextureComponent> buff_getClickableComponents(List<ClickableTextureComponent> __result, Buff __instance)
        {
            if (__instance is ICustomBuff cb)
            {
                if (__result == null)
                {
                    __result = new List<ClickableTextureComponent>();
                }

                var addittonalIcons = cb.GetCustomBuffIcons();
                if (addittonalIcons != null)
                {
                    __result.AddRange(addittonalIcons);
                }                
            }

            return __result;
        }

        static void clearAllBuffs(BuffsDisplay __instance)
        {
            // call the removeEffect method of custom buffs, because the
            // base game doe snot call removeBuff of applied 'other buffs'
            foreach(var buff in __instance.otherBuffs.OfType<ICustomBuff>())
            {
                buff.RemoveCustomEffect(true);
            }
        }

        static bool isWearingRing(bool __result, int ringIndex)
        {
            return __result || EffectHelper.ClothingObserver.HasRingEffect(ringIndex);
        }
    }
}
