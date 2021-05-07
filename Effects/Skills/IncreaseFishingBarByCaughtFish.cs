using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Skills
{
    class IncreaseFishingBarByCaughtFish : SingleEffect
    {
        // height of the bar in which the bobber bar moves
        const int bobberBarSlotTop = 568;

        const int maxBobberBarHeight = 450;        
        const int maxIncrease = 120;
        const int maxAffectingLuck = 10;
        const float maxLowerBoundRation = 0.8f;

        Farmer farmer;        

        public override void Apply(Farmer farmer, EffectChangeReason reason)
        {
            this.farmer = farmer;

            EffectHelper.ModHelper.Events.Display.MenuChanged += Display_MenuChanged;
        }

        protected Boolean IsRealFish(int index)
        {
            // Seaweed, Green & White Algea
            if (index == 152 || index == 153 || index == 157) return false;

            // Lobster, Crab, Oyster, Clam Shrimp
            if (index == 715 || index == 717 || index == 723 || index == 372 || index == 720) return false;

            // Cockle, Mussel, Snail, Crayfish, Periwinkle
            if (index == 718 || index == 719 || index == 721 || index == 716 || index == 722) return false;

            return true;
        }

        private void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            if (e.NewMenu is BobberBar bobberBar)
            {
                var bobberBarHeight = EffectHelper.ModHelper.Reflection.GetField<int>(bobberBar, "bobberBarHeight");
                int currentHeight = bobberBarHeight.GetValue();                               

                int fishCaught = 0;
                foreach(var fishidx in farmer.fishCaught.Keys)
                {
                    if (IsRealFish(fishidx))
                    {
                        int[] fishStat = farmer.fishCaught[fishidx];
                        if (fishStat != null && fishStat.Length > 0)
                        {
                            fishCaught += fishStat[0];
                        }
                    }
                }

                // maximum increase depends on caught fish
                int maxIncreaseBy = (int)Math.Min(Math.Atan(fishCaught / 500.0) * 100, maxIncrease);
                Logger.Debug($"Current luck: {farmer.LuckLevel}");
                float luckEffect = Math.Min(maxLowerBoundRation, farmer.LuckLevel / (float)maxAffectingLuck);
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

        public override void Remove(Farmer farmer, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.Display.MenuChanged -= Display_MenuChanged;
        }
    }
}
