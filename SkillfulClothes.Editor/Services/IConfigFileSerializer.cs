using SkillfulClothes.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.Services
{
    interface IConfigFileSerializer
    {

        IList<ClothingItemViewModel<T>> LoadFromFile<T>(string filename);

        IList<ClothingItemViewModel<T>> Load<T>(Stream stream);

    }
}
