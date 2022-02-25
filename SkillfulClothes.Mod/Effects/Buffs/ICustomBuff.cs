using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Buffs
{
    interface ICustomBuff
    {
        void ApplyCustomEffect();

        void RemoveCustomEffect(bool clearingAllBuffs);

        List<ClickableTextureComponent> GetCustomBuffIcons();
    }
}
