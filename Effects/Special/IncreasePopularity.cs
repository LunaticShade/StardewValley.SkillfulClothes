using SkillfulClothes.Types;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Special
{
    /// <summary>
    /// Increase gained friendship points by a given factor
    /// </summary>
    class IncreasePopularity : SingleEffect
    {
        const float factor = 1.2f;

        // caches the friendship points
        Dictionary<string, int> currentPoints = new Dictionary<string, int>();

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.Popularity, "Aumenta ligeiramente a sua popularidade");

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicking += GameLoop_UpdateTicking;            
        }

        private void GameLoop_UpdateTicking(object sender, StardewModdingAPI.Events.UpdateTickingEventArgs e)
        {
            foreach (var npcName in Game1.player.friendshipData.Keys)
            {
                Friendship fdata = Game1.player.friendshipData[npcName];

                if (currentPoints.TryGetValue(npcName, out int p))
                {
                    if (fdata.Points > p)
                    {
                        int gainedPoints = fdata.Points - p;
                        int additionalPoints = (int)(gainedPoints * (factor - 1));
                        Game1.player.changeFriendship(additionalPoints, Game1.getCharacterFromName(npcName));
                        Logger.Debug($"Friendship increased for {npcName} from {p} to {fdata.Points} + {additionalPoints}");
                    }
                }

                currentPoints[npcName] = fdata.Points;
            }
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicking -= GameLoop_UpdateTicking;                 
        }
    }
}
