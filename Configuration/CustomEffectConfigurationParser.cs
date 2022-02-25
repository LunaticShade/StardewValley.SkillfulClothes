using Newtonsoft.Json.Linq;
using SkillfulClothes.Effects;
using SkillfulClothes.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Configuration
{
    public class CustomEffectConfigurationParser
    {

        Dictionary<string, Type> availableEffects = new Dictionary<string, Type>();

        protected void DiscoverEffects()
        {            
            var lst = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(typeof(IEffect)) && !x.IsAbstract && !x.IsInterface);
            foreach(var type in lst)
            {
                availableEffects.Add(type.Name.ToLower(), type);
            }
        }

        public CustomEffectConfigurationParser()
        {
            DiscoverEffects();
        }

        /// <summary>
        /// Parse a json description of custom item effect definitions
        /// </summary>
        /// </summary>
        /// <param name="jsonStream"></param>
        /// <returns></returns>
        public List<CustomEffectItemDefinition> Parse(Stream jsonStream)
        {            
            List<CustomEffectItemDefinition> lst = new List<CustomEffectItemDefinition>();

            JObject root;
            using (StreamReader reader = new StreamReader(jsonStream))
            {
                root = JObject.Parse(reader.ReadToEnd());
            }

            foreach(var entry in root)
            {
                IEffect effect = ParseJsonEffectDefinition(entry.Value);
                CustomEffectItemDefinition def = new CustomEffectItemDefinition(entry.Key, effect);
                lst.Add(def);
            }

            return lst;
        }

        protected IEffect ParseJsonEffectDefinition(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    return CreateEffectInstance(token.ToObject<String>());                    
                case JTokenType.Array:
                    return ParseJsonEffectSet(token);
                case JTokenType.Object:
                    return ParseJsonEffectObject(token.Value<JObject>().First.Value<JProperty>());
                case JTokenType.Property:
                    return ParseJsonEffectObject(token.Value<JProperty>());
                default:
                    Logger.Error("Unexpected value: " + token.ToString());
                    return new NullEffect();                    
            }
        }

        /// <summary>
        /// Create an Instance of the effect with the given name/identifier
        /// </summary>
        /// <param name="effectName"></param>
        /// <returns></returns>
        public IEffect CreateEffectInstance(string effectName)
        {
            effectName = effectName.ToLower();

            Type effectType;
            if (availableEffects.TryGetValue(effectName, out effectType) || availableEffects.TryGetValue(effectName + "effect", out effectType))
            {
                var constr = effectType.GetConstructors().Where(x => x.GetParameters().Count() == 1 && x.GetParameters().First().ParameterType.IsAssignableTo(typeof(IEffectParameters))).FirstOrDefault();

                if (constr == null)
                {
                    // there is no constructor with parameters, us ethe parameter-less one
                    return (IEffect)Activator.CreateInstance(effectType);
                }

                // pass null as parameters to use the default parameters                
                return (IEffect)constr.Invoke(new object[] { null });
            }

            Logger.Error($"Unknown effect: {effectName}");
            return new NullEffect();
        }

        EffectSet ParseJsonEffectSet(JToken arrayToken)
        {
            if (arrayToken.Type != JTokenType.Array)
            {
                throw new ArgumentException("The specified token is not a JSON array", nameof(arrayToken));
            }

            List<IEffect> effects = new List<IEffect>();
            foreach(var child in arrayToken.Values())
            {
                effects.Add(ParseJsonEffectDefinition(child));
            }

            return EffectSet.Of(effects.ToArray());
        }

        IEffect ParseJsonEffectObject(JProperty jproperty)
        {
            try
            {
                string effectName = jproperty.Name;

                var effect = CreateEffectInstance(effectName);
                if (effect is ICustomizableEffect customizableEffect)
                {
                    var parameterDefinition = jproperty.Value;
                    IEffectParameters parameters = parameterDefinition.ToObject(customizableEffect.ParameterType) as IEffectParameters;
                    customizableEffect.SetParameterObject(parameters);
                }
                return effect;
            } catch (Exception e)
            {
                Logger.Error($"Encountered an invalid effect definition at {jproperty.Path}");
            }

            return new NullEffect();
        }
    }

    public class CustomEffectItemDefinition
    {
        /// <summary>
        /// Numerical id of the item or a well-known name
        /// </summary>
        public string ItemIdentifier { get; }
        public IEffect Effect { get; }

        public CustomEffectItemDefinition(string id, IEffect effect)
        {
            ItemIdentifier = id;
            Effect = effect;
        }
    }
}
