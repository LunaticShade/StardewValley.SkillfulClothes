﻿using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Events;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;

namespace SkillfulClothes
{
    /// <summary>
    /// Additional events to which effects can register
    /// </summary>
    class EffectHelperEvents
    {
        GameLocation lastLocation;

        /// <summary>
        /// Raised when the current game location changed
        /// </summary>
        public event EventHandler<ValueChangeEventArgs<GameLocation>> LocationChanged;

        /// <summary>
        /// Raised when the palyer defeated a monster
        /// </summary>
        public event EventHandler<MonsterSlainEventArgs> MonsterSlain;

        /// <summary>
        /// Raised when the player interacted with an NPC
        /// </summary>
        public event EventHandler<InteractedWithNPCEventArgs> InteractedWithNPC;

        protected void RaiseLocationChanged(GameLocation oldLocation, GameLocation newLocation)
        {
            Logger.Debug($"RaiseLocationChanged {oldLocation?.Name ?? "none"} -> {newLocation?.Name ?? "none"}");
            LocationChanged?.Invoke(this, new ValueChangeEventArgs<GameLocation>(oldLocation, newLocation));
        }

        public void RaiseMonsterSlain(Farmer who, Monster monster)
        {
            Logger.Debug($"RaiseMonsterSlain: {who.Name} defeated {monster.Name}");
            MonsterSlain?.Invoke(this, new MonsterSlainEventArgs(who, monster));
        }

        public void RaiseInteractedWithNPC(NPC npc)
        {
            Logger.Debug($"RaiseInteractedWithNPC: {npc.Name}");
            InteractedWithNPC?.Invoke(this, new InteractedWithNPCEventArgs(npc));
        }

        public void Watch(IModHelper modHelper)
        {
            modHelper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        }

        private void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            // location
            if (Game1.currentLocation != null && Game1.currentLocation.Name != "none") // avoid two events for old location -> none and none -> new location
            {
                if (lastLocation != Game1.currentLocation)
                {
                    RaiseLocationChanged(lastLocation, Game1.currentLocation);
                }

                lastLocation = Game1.currentLocation;
            }
        }
    }

    class ValueChangeEventArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public ValueChangeEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    class MonsterSlainEventArgs : EventArgs
    {
        public Farmer Who { get; }
        public Monster Monster { get; }

        public MonsterSlainEventArgs(Farmer who, Monster monster)
        {
            Who = who;
            Monster = monster;
        }
    }

    class InteractedWithNPCEventArgs : EventArgs
    {
        public NPC Npc { get; }

        public InteractedWithNPCEventArgs(NPC npc)
        {
            Npc = npc;
        }
    }
}
