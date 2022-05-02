﻿using Avalonia.Collections;
using ReactiveUI;
using SkillfulClothes.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.ViewModels
{
    class NestedEffectParameterViewModel : EffectParameterViewModel
    {
        public bool IsRoot { get; }

        public AvaloniaList<EffectViewModel> NestedEffects { get; } = new AvaloniaList<EffectViewModel>();

        public NestedEffectParameterViewModel(string parameterName, Type parameterType, object defaultValue, object value)
           : this(false, parameterName, parameterType, defaultValue, value)
        {
            // --
        }

        private NestedEffectParameterViewModel(bool isRoot, string parameterName, Type parameterType, object defaultValue, object value)
             : base(parameterName, parameterType, defaultValue, value)
        {
            IsRoot = isRoot;
            NestedEffects.CollectionChanged += NestedEffects_CollectionChanged;

            if (value is EffectSet effectSet)
            {
                foreach (var effect in effectSet.Effects)
                {
                    NestedEffects.Add(new EffectViewModel(effect));
                }
            }
            else
            if (value is IEffect effect)
            {
                NestedEffects.Add(new EffectViewModel(effect));
            }
        }

        private void NestedEffects_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach(var item in e.OldItems.OfType<EffectViewModel>())
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<EffectViewModel>())
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EffectViewModel.Description))
            {
                this.RaisePropertyChanged(nameof(Value));
            }
        }

        public static NestedEffectParameterViewModel CreateRoot()
        {
            return new NestedEffectParameterViewModel(true, "root", typeof(IEffect), null, null);
        }

    }
}
