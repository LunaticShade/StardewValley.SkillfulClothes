using SkillfulClothes.Editor.Services.Default;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.Services
{
    class ServiceKernel
    {

        public void RegisterServices()
        {
            Locator.CurrentMutable.RegisterConstant(new DefaultDialogService(), typeof(IDialogService));
        }

    }
}
