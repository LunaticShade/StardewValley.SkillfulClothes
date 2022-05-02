using SkillfulClothes.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.Services
{
    interface IDialogService
    {

        void SelectEffectFromLibrary(Action<IEffect> onSelection);

    }
}
