using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillfulClothes.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Configuration
{
     /// <summary>
     /// JsonConverter for parsing nested effects
     /// </summary>
    class EffectJsonConverter : JsonConverter<IEffect>
    {
        CustomEffectConfigurationParser Owner { get; }

        public EffectJsonConverter(CustomEffectConfigurationParser owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public override IEffect ReadJson(JsonReader reader, Type objectType, [AllowNull] IEffect existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.ReadFrom(reader);            
            return Owner.ParseJsonEffectDefinition(token);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] IEffect value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
