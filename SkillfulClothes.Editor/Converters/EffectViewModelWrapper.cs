﻿using Avalonia.Data.Converters;
using SkillfulClothes.Editor.ViewModels;
using SkillfulClothes.Effects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.Converters
{
    public class EffectViewModelWrapper : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IEffect effect)
            {
                return new EffectViewModel(effect);
            }

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
