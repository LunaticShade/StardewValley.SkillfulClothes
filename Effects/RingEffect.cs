using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    /// <summary>
    /// An effect which "simulates" the player wearing a specific ring
    /// </summary>
    class RingEffect : SingleEffect
    {
        public Ring ringInstance;

        public RingEffectType Ring { get; }

        public RingEffect(RingEffectType ring)
        {
            Ring = ring;
        }

        protected override EffectDescriptionLine GenerateEffectDescription() => Ring.GetEffectDescription();

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            if (ringInstance == null)
            {
                ringInstance = new Ring((int)Ring);
            }

            ringInstance.onEquip(Game1.player, Game1.currentLocation);
            
            // todo: Ring.onMonsterSlay

            EffectHelper.Events.LocationChanged -= Events_LocationChanged;
            EffectHelper.Events.LocationChanged += Events_LocationChanged;

            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked -= GameLoop_UpdateTicked;
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        }

        private void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            ringInstance?.update(Game1.currentGameTime, Game1.currentLocation, Game1.player);
        }

        private void Events_LocationChanged(object sender, ValueChangeEventArgs<GameLocation> e)
        {
            if (e.OldValue != null) ringInstance?.onLeaveLocation(Game1.player, e.OldValue);
            if (e.NewValue != null) ringInstance?.onNewLocation(Game1.player, e.NewValue);
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            ringInstance?.onUnequip(Game1.player, Game1.currentLocation);

            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked -= GameLoop_UpdateTicked;
            EffectHelper.Events.LocationChanged -= Events_LocationChanged;            
        }        
    }
}
