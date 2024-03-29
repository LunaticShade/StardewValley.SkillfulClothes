﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    public interface ICustomizableEffect : IEffect
    {
        public Type ParameterType { get; }        

        public object ParameterObject { get; set; }

        public void ReloadParameters();
    }
}
