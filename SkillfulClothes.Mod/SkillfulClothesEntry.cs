using Microsoft.Xna.Framework.Graphics;
using SkillfulClothes.Configuration;
using SkillfulClothes.Effects;
using SkillfulClothes.Patches;
using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.IO;

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
     * [x] Move faster (Speed +)
     * Weapon Speed + 
     * [x] Experience Multiplicator
     * [x] Save from death (consume shirt)
     * [x] Health regen when not moving
     * [x] Energy regen when not moving     
     * No Chain Damage
     * [x] Increase size of fishing bar based on number of fish caught
     * 
     **/

    public class SkillfulClothesEntry : Mod
    {

        ClothingObserver clothingObserver;

        public override void Entry(IModHelper helper)
        {            
            Logger.Init(this.Monitor);
            EffectHelper.Init(helper, helper.ReadConfig<SkillfulClothesConfig>());

            if (EffectHelper.Config.LoadCustomEffectDefinitions)
            {
                LoadCustomEffectDefinitions();
            }

            HarmonyPatches.Apply(this.ModManifest.UniqueID);
            ShopPatches.Apply(helper);
            TailoringPatches.Apply(helper);
            
            clothingObserver = EffectHelper.ClothingObserver;

            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            
            helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;

            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            helper.Events.GameLoop.DayEnding += GameLoop_DayEnding;

            helper.Events.GameLoop.ReturnedToTitle += GameLoop_ReturnedToTitle;            
        }

        private void LoadCustomEffectDefinitions()
        {
            if (EffectHelper.Config.EnableShirtEffects)
            {
                LoadCustomEffectDefinitions("custom_shirts.json", ItemDefinitions.ShirtEffects);
            }

            if (EffectHelper.Config.EnablePantsEffects)
            {
                LoadCustomEffectDefinitions("custom_pants.json", ItemDefinitions.PantsEffects);
            }

            if (EffectHelper.Config.EnableHatEffects)
            {
                LoadCustomEffectDefinitions("custom_hats.json", ItemDefinitions.HatEffects);
            }
        }

        private void LoadCustomEffectDefinitions<TItem>(string filename, Dictionary<TItem, ExtItemInfo> target)
        {
            string filepath = Path.Combine(EffectHelper.ModHelper.DirectoryPath, filename);

            if (File.Exists(filepath))
            {
                Logger.Info($"Loading custom effect definitions from {filename}");

                target.Clear();
                ReadItemDefinitions(filepath, target);
            }
            else
            {
                // export the current definitions
            }
        }

        private static void ReadItemDefinitions<TItem>(string filepath, Dictionary<TItem, ExtItemInfo> target)
        {
            List<CustomEffectItemDefinition> definitions;

            CustomEffectConfigurationParser parser = new CustomEffectConfigurationParser();
            using (FileStream fStream = new FileStream(filepath, FileMode.Open))
            {
                definitions = parser.Parse(fStream);
            }

            foreach (var def in definitions)
            {
                TItem itemId = (TItem)Enum.Parse(typeof(TItem), def.ItemIdentifier);

                target.Add(itemId, ExtendItem.With.Effect(def.Effect));
            }
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
