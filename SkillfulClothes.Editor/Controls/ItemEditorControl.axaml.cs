using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkillfulClothes.Editor.Controls
{
    public partial class ItemEditorControl : UserControl
    {
        public ItemEditorControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
