﻿using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes
{
    /// <summary>
    /// Manages custom textures of the mod
    /// </summary>
    class ModTextures
    {
        public Texture2D LooseSprites { get; private set; }

        public void Init()
        {
            LooseSprites = LoadFromResource("SkillfulClothes.Textures.loose_sprites.png");
        }   
        
        private Texture2D LoadFromResource(string name)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(name);
                return Texture2D.FromStream(Game1.graphics.GraphicsDevice, stream);
            } catch (Exception exc)
            {
                Logger.Error($"Could not load mod texture file '{name}': {exc.Message}");
                return null;
            }
        }
    }
}
