using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkillfulClothes.Editor.Views.EffectParameters
{
    public partial class NestedEffectParameterView : UserControl
    {
        public NestedEffectParameterView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
