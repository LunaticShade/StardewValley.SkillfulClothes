using SkillfulClothes.Effects;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
    class ClothingTextEditor : IAssetEditor
    {
        const string iconPlaceholder = "    ";

        string clothingAssetName = "Data\\ClothingInformation";
        string hatAssetName = "Data\\hats";

        public bool CanEdit<T1>(IAssetInfo asset)
        {
            return asset.AssetNameEquals(clothingAssetName) || asset.AssetNameEquals(hatAssetName);
        }

        public void Edit<T1>(IAssetData asset)
        {
            Logger.Debug($"Editing {asset.AssetName}");
            var dict = asset.AsDictionary<int, string>().Data;

            if (asset.AssetNameEquals(clothingAssetName))
            {
                // Shirts and Hats
                UpdateTexts(PredefinedEffects.ShirtEffects, dict, 2);
                UpdateTexts(PredefinedEffects.PantsEffects, dict, 2);
            } else
            {
                // Hats
                UpdateTexts(PredefinedEffects.HatEffects, dict, 1);
            }       
        }    
        
        private void UpdateTexts<T>(Dictionary<T, IEffect> effects, IDictionary<int, string> dict, int descriptionIndex)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum");
            }

            foreach (var effect in effects)
            {
                int index = Convert.ToInt32(effect.Key);
                if (dict.ContainsKey(index))
                {
                    // directly add the effects to the description text of the item, and leave space for the icons
                    // since Harmony fails to override the Item.getExtraSpaceNeededForTooltipSpecialIcons(...)
                    string value = dict[index];
                    var pp = value.Split('/');
                    pp[descriptionIndex] += "\n";
                    for (int i = 0; i < effect.Value.EffectDescription.Count; i++)
                    {
                        // add additional space at the end to avoid that the text clings to the textbox's border
                        pp[descriptionIndex] = $"{pp[descriptionIndex]}\n{iconPlaceholder}{effect.Value.EffectDescription[i].Text} ";
                    }

                    dict[index] = String.Join("/", pp);
                }
            }
        }
    }
}
