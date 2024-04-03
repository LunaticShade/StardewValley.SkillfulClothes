using SkillfulClothes.Types;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewValley.Tools;
using static StardewValley.Menus.CarpenterMenu;

namespace SkillfulClothes.Effects.Special
{
    /// <summary>
    /// Grants a discount when constructing buildings
    /// </summary>
    class ConstructDiscount : SingleEffect<ConstructDiscountParameters>
    {
        public ConstructDiscount(ConstructDiscountParameters parameters)
            : base(parameters)
        {
            // --
        }

        public ConstructDiscount(double discount)
            : base(ConstructDiscountParameters.With(discount))
        {
            // --
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.Display.MenuChanged += Display_MenuChanged;
        }        
        
        int applyDiscount(int value)
        {
            return (int)Math.Max(0, value * (1 - Parameters.Discount));
        }

        private void Display_MenuChanged(object sender, StardewModdingAPI.Events.MenuChangedEventArgs e)
        {
            if (Game1.currentLocation?.NameOrUniqueName == "ScienceHouse" && e.NewMenu is CarpenterMenu carpenterMenu)
            {                
                foreach(var blueprint in carpenterMenu.Blueprints)
                {
                    if (blueprint.Skin != null && blueprint.Skin.BuildCost.HasValue)
                    {
                        blueprint.Skin.BuildCost = applyDiscount(blueprint.Skin.BuildCost.Value);
                    }
                    blueprint.Data.BuildCost = applyDiscount(blueprint.Data.BuildCost);

                    foreach(var material in blueprint.BuildMaterials)
                    {
                        material.Amount = applyDiscount(material.Amount);
                    }
                }
                carpenterMenu.SetNewActiveBlueprint(0);

                EffectHelper.Overlays.AddSparklingText(new SparklingText(Game1.dialogueFont, $"You received a discount ({Parameters.Discount * 100:0}%)", Color.LimeGreen, Color.Azure), new Vector2(64f, Game1.uiViewport.Height - 64));
            }
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.Display.MenuChanged -= Display_MenuChanged;
        }

        protected override EffectDescriptionLine GenerateEffectDescription() => new EffectDescriptionLine(EffectIcon.Money, $"Get a slight discount when constructing buildings ({Parameters.Discount * 100:0}%)");
    }

    public class ConstructDiscountParameters : IEffectParameters
    {
        public double Discount { get; set; }

        public static ConstructDiscountParameters With(double discount)
        {
            return new ConstructDiscountParameters() { Discount = discount };
        }
    }
}
