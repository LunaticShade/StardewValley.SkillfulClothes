using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SkillfulClothes.Editor.Services;
using SkillfulClothes.Editor.ViewModels;
using SkillfulClothes.Editor.Views;
using Splat;

namespace SkillfulClothes.Editor
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            ServiceKernel serviceKernel = new ServiceKernel();
            serviceKernel.RegisterServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(Locator.Current.GetService<IDialogService>()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
