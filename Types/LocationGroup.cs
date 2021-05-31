using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    public enum LocationGroup
    {
        None,
        DesertPlaces
    }

    public static class LocationGroupsExtensions
    {
        static HashSet<string> desertLocationNames = new HashSet<string>() { "desert", "sandyhouse", "skullcave", "club" }; // club = Qi's casino

        public static string GetEffectDescriptionSuffix(this LocationGroup location)
        {
            switch (location)
            {
                case LocationGroup.DesertPlaces: return " at desert places";
                default: return "";
            }
        }

        /// <summary>
        /// Returns if the current location belongs to the given location group
        /// </summary>        
        public static bool IsActive(this LocationGroup location)
        {
            switch (location)
            {
                case LocationGroup.DesertPlaces: return desertLocationNames.Contains(Game1.currentLocation?.Name.ToLower());
                default: return false;
            }            
        }
    }
}
