﻿using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseFishingBarByCaughtFish : SingleEffect<NoEffectParameters>
    {
        // height of the bar in which the bobber bar moves
        const int bobberBarSlotTop = 568;

        const int maxBobberBarHeight = 450;        
        const int maxIncrease = 120;
        const int maxAffectingLuck = 10;
        const float maxLowerBoundRation = 0.8f;

        public IncreaseFishingBarByCaughtFish()
            : base(NoEffectParameters.Default)
        {
            // --
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        { 
            EffectHelper.ModHelper.Events.Display.MenuChanged += Display_MenuChanged;
        }

        protected bool IsRealFish(string fishId)
        {
            if (int.TryParse(fishId, out int fishNumericalId))
            {
                // Seaweed, Green & White Algea
                if (fishNumericalId == 152 || fishNumericalId == 153 || fishNumericalId == 157) return false;

                // Lobster, Crab, Oyster, Clam Shrimp
                if (fishNumericalId == 715 || fishNumericalId == 717 || fishNumericalId == 723 || fishNumericalId == 372 || fishNumericalId == 720) return false;

                // Cockle, Mussel, Snail, Crayfish, Periwinkle
                if (fishNumericalId == 718 || fishNumericalId == 719 || fishNumericalId == 721 || fishNumericalId == 716 || fishNumericalId == 722) return false;
            }

            return true;
        }

        private void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            if (e.NewMenu is BobberBar bobberBar)
            {
                Farmer farmer = Game1.player;

                var bobberBarHeight = EffectHelper.ModHelper.Reflection.GetField<int>(bobberBar, "bobberBarHeight");
                int currentHeight = bobberBarHeight.GetValue();                               

                int fishCaught = 0;
                foreach(var fishNumericalId in farmer.fishCaught.Keys)
                {
                    if (IsRealFish(fishNumericalId))
                    {
                        int[] fishStat = farmer.fishCaught[fishNumericalId];
                        if (fishStat != null && fishStat.Length > 0)
                        {
                            fishCaught += fishStat[0];
                        }
                    }
                }

                // maximum increase depends on caught fish
                int maxIncreaseBy = (int)Math.Min(Math.Atan(fishCaught / 500.0) * 100, maxIncrease);
                Logger.Debug($"Current luck: {farmer.LuckLevel}, Daily luck: {farmer.DailyLuck}");
                double luckEffect = Math.Min(maxLowerBoundRation, farmer.LuckLevel / (double)maxAffectingLuck);
                // add daily luck (can be negative!)
                luckEffect = Math.Max(0, Math.Min(1, luckEffect + farmer.DailyLuck));
                Logger.Debug($"max increase: {maxIncreaseBy}, luck effect -> {luckEffect}");
                // actual increase is a random value between 0 and maxIncreaseBy shifted by the current luck level
                int increaseBy = EffectHelper.Random.Next((int)(maxIncreaseBy * luckEffect), maxIncreaseBy);
                
                int newHeight = Math.Min(currentHeight + increaseBy, maxBobberBarHeight);
                bobberBarHeight.SetValue(newHeight);
                Logger.Debug($"increased bobberBarHeight from {currentHeight} to {newHeight} (+{increaseBy}, #fish: {fishCaught})");

                // adjust bobber bar starting pos                
                EffectHelper.ModHelper.Reflection.GetField<int>(bobberBar, "bobberBarPos").SetValue(bobberBarSlotTop - newHeight);
            }
        }

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.SkillFishing, "Increase fishing bar based on caught fish");

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.Display.MenuChanged -= Display_MenuChanged;
        }
    }
}
