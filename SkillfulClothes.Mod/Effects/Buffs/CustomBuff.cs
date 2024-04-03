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
        public CustomBuff(string id, string description, string source, int durationMinutes)
            : base(id)
        {
            this.description = description;
            this.source = source;
            if (durationMinutes == Buff.ENDLESS)
            {
                this.millisecondsDuration = Buff.ENDLESS;
                this.totalMillisecondsDuration = Buff.ENDLESS;
            } else
            {
                this.millisecondsDuration = durationMinutes * 1000;
                this.totalMillisecondsDuration = durationMinutes * 1000;
            }         

            this.displaySource = source;
        }        
    }
}
