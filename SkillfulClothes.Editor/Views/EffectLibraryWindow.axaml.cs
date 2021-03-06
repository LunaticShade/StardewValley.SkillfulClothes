using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkillfulClothes.Editor.Views
{
    public partial class EffectLibraryWindow : Window
    {
        public EffectLibraryWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
