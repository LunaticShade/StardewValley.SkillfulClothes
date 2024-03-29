﻿using Microsoft.Xna.Framework;
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
    class SeasonalEffect : CustomizableEffect<SeasonalEffectParameters>
    {
        List<EffectDescriptionLine> effectDescription;

        bool isApplied = false;

        public override List<EffectDescriptionLine> EffectDescription => effectDescription;

        Item SourceItem { get; set; }

        public override void ReloadParameters()
        {
            effectDescription = Parameters.Effect.EffectDescription.Select(x => new EffectDescriptionLine(x.Icon, x.Text + Parameters.Season.GetEffectDescriptionSuffix())).ToList();
        }

        public SeasonalEffect(SeasonalEffectParameters parameters)
            : base(parameters)
        {
            // --
        }

        public SeasonalEffect(Season season, IEffect actualEffect)
            : base(SeasonalEffectParameters.With(season, actualEffect))
        {
            // --
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
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

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            if (isApplied)
            {
                isApplied = false;
                Parameters.Effect.Remove(sourceItem, reason);

                if (reason == EffectChangeReason.DayEnd && Game1.dayOfMonth == 28)
                {
                    // end of season info
                    Game1.addHUDMessage(new CustomHUDMessage($"The effect of {SourceItem} wore off", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                }
            }

            EffectHelper.Events.LocationChanged -= Events_LocationChanged;
            SourceItem = null;
        }

        private void RevalidateConditions(Item sourceItem, EffectChangeReason reason)
        {
            if (Parameters.Season.IsActive())
            {
                if (!isApplied)
                {
                    if (SourceItem != null && (reason == EffectChangeReason.Reset || (reason == EffectChangeReason.DayStart && Game1.dayOfMonth == 1)))
                    {
                        Game1.addHUDMessage(new CustomHUDMessage($"The effect of {SourceItem} is now active", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                    }

                    isApplied = true;
                    Parameters.Effect.Apply(sourceItem, reason);
                }
            } else
            {
                if (isApplied)
                {
                    if (SourceItem != null && reason == EffectChangeReason.Reset)
                    {
                        Game1.addHUDMessage(new CustomHUDMessage($"The effect of {SourceItem} wore off", SourceItem, Color.White, TimeSpan.FromSeconds(5)));
                    }

                    isApplied = false;
                    Parameters.Effect.Remove(sourceItem, reason);
                }
            }
        }
    }

    public class SeasonalEffectParameters : IEffectParameters
    {
        public Season Season { get; set; } = Season.Spring;
        public IEffect Effect { get; set; } = new NullEffect();

        public static SeasonalEffectParameters With(Season season, IEffect actualEffect)
        {
            return new SeasonalEffectParameters() { Season = season, Effect = actualEffect };
        }
    }
}
