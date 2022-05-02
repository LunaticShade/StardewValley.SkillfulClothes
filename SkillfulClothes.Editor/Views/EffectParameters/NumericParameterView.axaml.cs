using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkillfulClothes.Editor.Views.EffectParameters
{
    public partial class NumericParameterView : UserControl
    {
        public NumericParameterView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
