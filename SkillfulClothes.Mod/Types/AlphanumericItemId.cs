﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    public class AlphanumericItemId
    {
        public ClothingItemType ItemType { get; }
        public string ItemName { get; set; }
        public string ItemId { get; }

        public AlphanumericItemId(string itemId, ClothingItemType itemType = ClothingItemType.Undefined)
        {
            ItemId = itemId;
            ItemType = itemType;
        }

        public override bool Equals(object obj)
        {
            if (obj is AlphanumericItemId other)
            {
                return ItemType == other.ItemType && ItemId == other.ItemId;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }

        public override string ToString()
        {
            return ItemName ?? ItemId;
        }
    }
}
