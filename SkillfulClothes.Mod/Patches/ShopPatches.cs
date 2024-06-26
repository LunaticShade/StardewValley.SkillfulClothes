﻿using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Patches
{
    /// <summary>
    /// Changes shop behavior (e.g. items sold)
    /// </summary>
    class ShopPatches
    {
        static Dictionary<Shop, List<Shirt>> soldShirts;
        static Dictionary<Shop, List<Pants>> soldPants;
        static Dictionary<Shop, List<Hat>> soldHats;

        public static void Apply(IModHelper modHelper)
        {
            modHelper.Events.Display.MenuChanged += Display_MenuChanged;

            soldShirts = ItemDefinitions.ShirtEffects.Where(x => x.Value.SoldBy != Shop.None).GroupBy(x => x.Value.SoldBy).ToDictionary(x => x.Key, x => x.Select(v => v.Key).ToList());
            soldPants = ItemDefinitions.PantsEffects.Where(x => x.Value.SoldBy != Shop.None).GroupBy(x => x.Value.SoldBy).ToDictionary(x => x.Key, x => x.Select(v => v.Key).ToList());
            soldHats = ItemDefinitions.HatEffects.Where(x => x.Value.SoldBy != Shop.None).GroupBy(x => x.Value.SoldBy).ToDictionary(x => x.Key, x => x.Select(v => v.Key).ToList());
        }

        private static void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            if (e.NewMenu is ShopMenu shopMenu)
            {
                Logger.Info($"Opened shop with ID {shopMenu.ShopId}");

                var shop = shopMenu.GetShop();
                if (shop != Shop.None)
                {
                    EditShop(shopMenu, shop);
                }
            }
        }

        private static void EditShop(ShopMenu shopMenu, Shop shop)
        {
            // add the items of this shop

            if (soldShirts.TryGetValue(shop, out List<Shirt> shirts))
            {
                AddItems(shop, shopMenu, shirts);                
            }

            if (soldPants.TryGetValue(shop, out List<Pants> pants))
            {
                AddItems(shop, shopMenu, pants);
            }

            if (soldHats.TryGetValue(shop, out List<Hat> hats))
            {
                AddItems(shop, shopMenu, shirts);
            }

            // Todo: add tab buttons for clothing
            // see ShopMenu.setUpStoreForContext
        }

        private static void AddItems<T>(Shop shop, ShopMenu shopMenu, List<T> items)
            where T: AlphanumericItemId
        {
            foreach (var item in items)
            {
                if (ItemDefinitions.GetExtInfo(item, out ExtItemInfo extInfo) && extInfo.SellingCondition.IsFulfilled(shop)) 
                {
                    Item saleItem = CreateItemInstance<T>(item);
                    shopMenu.forSale.Add(saleItem);
                    shopMenu.itemPriceAndStock.Add(saleItem, new ItemStockInformation(extInfo.Price, 1));
                }
            }
        }

        protected static Item CreateItemInstance<T>(AlphanumericItemId itemId)
        {
            if (typeof(T) == typeof(Shirt) || typeof(T) == typeof(Pants))
            {                
                return new StardewValley.Objects.Clothing(itemId.ItemId);
            }

            if (typeof(T) == typeof(Hat))
            {
                return new StardewValley.Objects.Hat(itemId.ItemId);
            }

            return null;
        }
    }
}
