using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Effects;
using SkillfulClothes.Patches;
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

    public class SkillfulClothesEntry : Mod
    {

        ClothingObserver clothingObserver;

        public override void Entry(IModHelper helper)
        {            
            Logger.Init(this.Monitor);
            EffectHelper.Init(helper, helper.ReadConfig<SkillfulClothesConfig>());

            HarmonyPatches.Apply(this.ModManifest.UniqueID);
            
            clothingObserver = EffectHelper.ClothingObserver;

            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            
            helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;

            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            helper.Events.GameLoop.DayEnding += GameLoop_DayEnding;

            helper.Events.GameLoop.ReturnedToTitle += GameLoop_ReturnedToTitle;            
        }

        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            Helper.Content.AssetEditors.Add(new ClothingTextEditor());
        }

        private void GameLoop_ReturnedToTitle(object sender, ReturnedToTitleEventArgs e)
        {
            clothingObserver.Reset(Game1.player);            
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e)
        {
            clothingObserver.Restore(Game1.player, EffectChangeReason.DayStart);
        }

        private void GameLoop_DayEnding(object sender, DayEndingEventArgs e)
        {
            clothingObserver.Suspend(Game1.player, EffectChangeReason.DayEnd);
        }

        private void GameLoop_UpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;            
            
            clothingObserver.Update(Game1.player);            
        }
    }
}
