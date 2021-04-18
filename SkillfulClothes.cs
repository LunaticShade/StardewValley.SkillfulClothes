using SkillfulClothes.Effects;
using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using System.Collections.Generic;

namespace SkillfulClothes
{
    /** Effect Ideas
     * 
     * [x] Increase Max Health
     * [x] Increase Max Energy (= Stamina)
     * [x] Attack + 
     * [x] Defense +  (= Resilience)
     * [x] Skill + 
     * [x] Immunity +
     * Weapon Speed + 
     * [x]Experience Multiplicator
     * [x] Save from death (consume shirt)
     * [x] Health regen when not moving
     * [x] Energy regen when not moving
     * Move faster
     * No Chain Damage
     * [x] Increase size of fishing bar based on numbe rof fish caught
     * 
     **/

    public class SkillfulClothes : Mod
    {
        ShirtObserver shirtObserver;
        PantsObserver pantsObserver;
        HatObserver hatObserver;

        public override void Entry(IModHelper helper)
        {
            Logger.Init(this.Monitor);
            EffectHelper.Init(helper);

            HarmonyPatches.Apply(this.ModManifest.UniqueID);

            shirtObserver = new ShirtObserver(helper);
            pantsObserver = new PantsObserver(helper);
            hatObserver = new HatObserver(helper);                  

            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;

            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            helper.Events.GameLoop.DayEnding += GameLoop_DayEnding;

            helper.Events.GameLoop.ReturnedToTitle += GameLoop_ReturnedToTitle;
        }

        private void GameLoop_ReturnedToTitle(object sender, ReturnedToTitleEventArgs e)
        {
            shirtObserver.Reset(Game1.player);
            pantsObserver.Reset(Game1.player);
            hatObserver.Reset(Game1.player);
        }

        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {             
            // update description texts of clothing and hats
            // to make room for effect descriptions
            Helper.Content.AssetEditors.Add(new ClothingTextEditor());                      
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e)
        {
            // restore active effects
            shirtObserver?.Restore(Game1.player);
            pantsObserver?.Restore(Game1.player);
            hatObserver?.Restore(Game1.player);
        }

        private void GameLoop_DayEnding(object sender, DayEndingEventArgs e)
        {
            // remove active effects, so that value changes do not get saved
            shirtObserver.Suspend(Game1.player);
            pantsObserver?.Suspend(Game1.player);
            hatObserver?.Suspend(Game1.player);
        }

        private void GameLoop_UpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;            
            
            shirtObserver.Update(Game1.player);
            pantsObserver.Update(Game1.player);
            hatObserver.Update(Game1.player);
        }
    }
}
