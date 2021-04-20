using Harmony;
using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Effects;
using SkillfulClothes.Types;
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
        static ClothingDrawWrapper clothingDrawWrapper = new ClothingDrawWrapper();

        public static void Apply(String modId)
        {
            HarmonyInstance harmony = HarmonyInstance.Create(modId);

            // patch IClickableMenu.drawToolTip, since Harmony (1.2.0.1) is unable to patch Item.getExtraSpaceNeededForTooltipSpecialIcons() correctly (returns a struct)
            // see https://github.com/pardeike/Harmony/issues/159 and https://github.com/pardeike/Harmony/issues/77
            // seems to be fixed in Harmony 2.0.4.0
            harmony.Patch(
                  original: typeof(IClickableMenu).GetMethod(nameof(IClickableMenu.drawToolTip), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public),
                  prefix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.drawToolTip))
                );            
        }

        static void drawToolTip(ref Item hoveredItem)
        {
            // replace the hoveredItem with a wrapper class which allows us to
            // control Item.getExtraSpaceNeededForTooltipSpecialIcons
            if (PredefinedEffects.GetEffect(hoveredItem, out IEffect effect))
            {
                if (hoveredItem is Clothing clothing)
                {
                    clothingDrawWrapper.Assign(clothing, effect);
                    hoveredItem = clothingDrawWrapper;
                }
            }
        }
    }
}
