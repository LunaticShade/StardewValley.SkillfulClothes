using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    public static class SeasonExtensions
    {
        public static string GetEffectDescriptionSuffix(this Season season)
        {
            return " in " + season.ToString();
        }

        /// <summary>
        /// Returns if the current location has the specified season
        /// </summary>        
        public static bool IsActive(this Season season)
        {
            var currentSeason = Game1.GetSeasonForLocation(Game1.currentLocation);
            return currentSeason.ToString().ToLower() == season.ToString().ToLower();
        }
    }
}
