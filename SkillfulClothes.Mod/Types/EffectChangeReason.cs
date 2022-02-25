using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    /// <summary>
    /// States why an effect should be applied or removed
    /// </summary>
    public enum EffectChangeReason
    {
        ItemPutOn,
        ItemRemoved,
        DayEnd,
        DayStart,

        Reset
    }
}
