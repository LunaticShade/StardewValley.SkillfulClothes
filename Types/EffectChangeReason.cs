using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    public enum EffectChangeReason
    {
        ItemPutOn,
        ItemRemoved,
        DayEnd,
        DayStart,

        Reset
    }
}
