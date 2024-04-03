using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    public abstract class AlphanumericItemId
    {
        public abstract ClothingItemType ItemType { get; }
        public string ItemName { get; set; }
        public string ItemId { get; }

        public AlphanumericItemId(string itemId)
        {
            ItemId = itemId;
        }       
    }
}
