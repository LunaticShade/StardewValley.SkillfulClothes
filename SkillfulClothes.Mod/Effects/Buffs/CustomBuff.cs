using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Buffs
{
    abstract class CustomBuff : Buff, ICustomBuff
    {
        public CustomBuff(string description, string source, int durationMinutes)
            : base(-1)
        {
            this.description = description;
            this.source = source;
            this.millisecondsDuration = durationMinutes * 1000;
            this.totalMillisecondsDuration = durationMinutes * 1000;
            this.displaySource = source;
        }

        public abstract void ApplyCustomEffect();

        public abstract List<ClickableTextureComponent> GetCustomBuffIcons();

        public abstract void RemoveCustomEffect(bool clearingAllBuffs);        
    }
}
