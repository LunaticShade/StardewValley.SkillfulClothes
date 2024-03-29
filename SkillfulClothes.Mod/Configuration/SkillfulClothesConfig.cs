﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Configuration
{
    class SkillfulClothesConfig
    {
        public bool EnableShirtEffects { get; set; } = true;
        public bool EnablePantsEffects { get; set; } = true;
        public bool EnableHatEffects { get; set; } = true;

        public bool AllItemsCanBeTailored { get; set; } = false;

        public bool LoadCustomEffectDefinitions { get; set; } = false;

        public bool verboseLogging { get; set; } = false;
    }
}
