using SkillfulClothes.Effects;
using SkillfulClothes.Effects.Special;
using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
    /// <summary>
    /// Watches an equipped clothing slot
    /// and replaces items if necessary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class EquippedClothingItemObserver<T>
    {
        bool isSuspended = false;

        string clothingName;

        /// <summary>
        /// Index of the piece of clothing which
        /// is currently equipped by the player
        /// </summary>
        PerScreen<int?> _currentIndex { get; } = new PerScreen<int?>();

        protected int? CurrentIndex
        {
            get => _currentIndex.Value;
            set => _currentIndex.Value = value;
        }

        PerScreen<Item> _currentItem { get; } = new PerScreen<Item>();

        protected Item CurrentItem
        {
            get => _currentItem.Value;
            set => _currentItem.Value = value;
        }

        /// <summary>
        /// The effect of the currently equipped piece of clothing         
        /// </summary>
        PerScreen<IEffect> _currentEffect { get; } = new PerScreen<IEffect>();

        public IEffect CurrentEffect
        {
            get => _currentEffect.Value;
            set => _currentEffect.Value = value;
        }

        public EquippedClothingItemObserver()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum");
            }

            clothingName = typeof(T).Name;
        }

        public void Update(Farmer farmer)
        {
            int newIndex = GetCurrentIndex(farmer);

            if (!CurrentIndex.HasValue || CurrentIndex.Value != newIndex)
            {
                ClothingChanged(farmer, newIndex);
            }            
        }

        protected abstract int GetCurrentIndex(Farmer farmer);

        protected abstract Item GetCurrentItem(Farmer farmer);

        protected void ClothingChanged(Farmer farmer, int newIndex)
        {
            bool initialUpdate = !CurrentIndex.HasValue;

            CurrentIndex = newIndex;

            T ev = (T)(object)CurrentIndex.Value;
            Logger.Debug($"{farmer.Name}'s {clothingName} changed to {newIndex} {Enum.GetName(typeof(T), ev)}.");

            // remove old effect
            if (CurrentEffect != null)
            {
                CurrentEffect.Remove(CurrentItem, EffectChangeReason.ItemRemoved);
                CurrentEffect = null;
            }

            CurrentItem = GetCurrentItem(farmer);


            if (ItemDefinitions.GetEffectByIndex<T>(CurrentIndex ?? -1, out IEffect cEffect)) {
                CurrentEffect = cEffect;
                if (!isSuspended)
                {
                    CurrentEffect.Apply(CurrentItem, initialUpdate ? EffectChangeReason.DayStart : EffectChangeReason.ItemPutOn);                    
                }
            } else
            {
                CurrentEffect = null;
                Logger.Debug($"Equipped {clothingName} has no effects");
            }
        }

        /// <summary>
        /// Disable the currently active effect
        /// </summary>
        public void Suspend(Farmer farmer, EffectChangeReason reason)
        {
            if (!isSuspended)
            {
                Logger.Debug($"Suspend {clothingName} effects");
                isSuspended = true;
                CurrentEffect?.Remove(CurrentItem, reason);
            }
        }

        /// <summary>
        /// Re-apply the current effects (after having them suspended)
        /// </summary>
        /// <param name="farmer"></param>
        public void Restore(Farmer farmer, EffectChangeReason reason)
        {
            if (isSuspended)
            {
                Logger.Debug($"Restore {clothingName} effects");
                isSuspended = false;
                CurrentEffect?.Apply(CurrentItem, reason);                
            }
        }

        public void Reset(Farmer farmer)
        {
            CurrentIndex = null;
            CurrentEffect?.Remove(CurrentItem, EffectChangeReason.Reset);
            CurrentEffect = null;
        }

        public bool HasRingEffect(int ringIndex)
        {
            if (isSuspended) return false;

            if(CurrentEffect is EffectSet set)
            {
                return set.Effects.Any(x => (x is RingEffect re) && (int)re.Parameters.Ring == ringIndex);
            } else 
            { 
                return (CurrentEffect is RingEffect re) && (int)re.Parameters.Ring == ringIndex;
            }
        }
    }

    class ShirtObserver : EquippedClothingItemObserver<Shirt>
    {
        protected override int GetCurrentIndex(Farmer farmer)
        {
            return farmer.shirtItem.Value?.ParentSheetIndex ?? -1;
        }

        protected override Item GetCurrentItem(Farmer farmer)
        {
            return farmer.shirtItem.Value;
        }
    }

    class PantsObserver : EquippedClothingItemObserver<Pants>
    {
        protected override int GetCurrentIndex(Farmer farmer)
        {
            return farmer.pantsItem.Value?.ParentSheetIndex ?? -1;
        }

        protected override Item GetCurrentItem(Farmer farmer)
        {
            return farmer.pantsItem.Value;
        }
    }

    class HatObserver : EquippedClothingItemObserver<Types.Hat>
    {
        protected override int GetCurrentIndex(Farmer farmer)
        {
            return int.Parse(farmer.hat.Value?.ItemId ?? "-1");
        }

        protected override Item GetCurrentItem(Farmer farmer)
        {
            return farmer.hat.Value;
        }
    }
}
