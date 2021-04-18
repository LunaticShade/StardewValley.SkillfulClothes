using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    public class EffectDescriptionLine
    {
        public EffectIcon Icon { get; }
        public string Text { get; }

        public EffectDescriptionLine(EffectIcon icon, string text)
        {
            Icon = icon;
            Text = text;
        }
    }   
}
