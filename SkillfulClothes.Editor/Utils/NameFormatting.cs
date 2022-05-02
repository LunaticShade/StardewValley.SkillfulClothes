using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Editor.Utils
{
    static class NameFormatting
    {

        public static string FormatEffectDisplayName(string originalName)
        {
            // remove "Effect" at the end, if present
            if (originalName.EndsWith("effect", StringComparison.InvariantCultureIgnoreCase))
            {
                originalName = originalName.Remove(originalName.Length - "effect".Length);
            }

            return UnCamelCase(originalName);
        }

        public static string FormatClosingItemName(string originalName)
        {
            return UnCamelCase(originalName);
        }

        static string UnCamelCase(string str)
        {
            // replace camcle case with spaces
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (i > 0 && Char.IsUpper(c))
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }

            return sb.ToString();
        }

    }
}
