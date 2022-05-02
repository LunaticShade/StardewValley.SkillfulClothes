using SkillfulClothes.Editor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.ViewModels
{
    class EffectLibraryViewModel : ViewModelBase
    {

        IDialogService DialogService { get; }

        public EffectLibraryViewModel(IDialogService dialogService)
        {
            DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

    }
}
