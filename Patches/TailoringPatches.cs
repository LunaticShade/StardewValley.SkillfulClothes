using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Patches
{
    class TailoringPatches
    {
        static string DeferredMessage { get; set; }

        public static void Apply(IModHelper modHelper)
        {
            modHelper.Events.Display.MenuChanged += Display_MenuChanged;
        }

        public static void AddTailoringMessage(string message)
        {
            DeferredMessage = message;
        }

        private static void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            if (e.OldMenu is TailoringMenu)
            {
                // Tailoring menu got closed
                if (!String.IsNullOrEmpty(DeferredMessage))
                {
                    // todo: set messageSubject to display item in message
                    Game1.addHUDMessage(new HUDMessage(DeferredMessage));
                    DeferredMessage = null;
                }
            }
        }        
    }
}
