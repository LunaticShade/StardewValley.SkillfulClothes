using Microsoft.Xna.Framework;
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
    /// Activates an encapsulated effect only in a given season
    /// </summary>
    class SeasonalEffect : IEffect
    {
        Season Season { get; }
        IEffect ActualEffect { get; }

        List<EffectDescriptionLine> effectDescription;

        bool isApplied = false;

        public List<EffectDescriptionLine> EffectDescription => effectDescription;

        Item SourceItem { get; set; }

        public SeasonalEffect(Season season, IEffect effect)
        {
            Season = season;
            ActualEffect = effect;
            effectDescription = ActualEffect.EffectDescription.Select(x => new EffectDescriptionLine(x.Icon, x.Text + Season.GetEffectDescriptionSuffix())).ToList();
        }

        public void Apply(Item sourceItem, EffectChangeReason reason)
        {
            SourceItem = sourceItem;

            RevalidateConditions(sourceItem, reason);

            EffectHelper.Events.LocationChanged -= Events_LocationChanged;
            EffectHelper.Events.LocationChanged += Events_LocationChanged;
        }

        private void Events_LocationChanged(object sender, ValueChangeEventArgs<GameLocation> e)
        {
            // as locations can have different season, revalidate conditions
            RevalidateConditions(null, EffectChangeReason.Reset);
        }

        public void Remove(Item sourceItem, EffectChangeReason reason)
        {
            if (isApplied)
            {
                isApplied = false;
                ActualEffect.Remove(sourceItem, reason);

                if (reason == EffectChangeReason.DayEnd && Game1.dayOfMonth == 28)
                {
                    // end of season info
                    Game1.addHUDMessage(new CustomHUDMessage($"O efeito de {SourceItem} está desativado", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                }
            }

            EffectHelper.Events.LocationChanged -= Events_LocationChanged;
            SourceItem = null;
        }

        private void RevalidateConditions(Item sourceItem, EffectChangeReason reason)
        {
            if (Season.IsActive())
            {
                if (!isApplied)
                {
                    if (SourceItem != null && (reason == EffectChangeReason.Reset || (reason == EffectChangeReason.DayStart && Game1.dayOfMonth == 1)))
                    {
                        Game1.addHUDMessage(new CustomHUDMessage($"O efeito de {SourceItem} agora está ativo", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                    }

                    isApplied = true;
                    ActualEffect.Apply(sourceItem, reason);
                }
            } else
            {
                if (isApplied)
                {
                    if (SourceItem != null && reason == EffectChangeReason.Reset)
                    {
                        Game1.addHUDMessage(new CustomHUDMessage($"O efeito de {SourceItem} está desativado", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                    }

                    isApplied = false;
                    ActualEffect.Remove(sourceItem, reason);
                }
            }
        }
    }
}
