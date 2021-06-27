using SkillfulClothes.Effects;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    enum AnimalType
    {
        Unknown = -1,
        Any = 0,
        Chicken,
        Duck,
        Rabbit,
        Dinosaur,
        Cow,
        Goat,
        Pig,
        Hog,
        Sheep,
        Ostrich
    }

    static class AnimalTypeExtensions
    {
        public static AnimalType GetAnimalType(this FarmAnimal animal)
        {
            foreach(AnimalType at in Enum.GetValues(typeof(AnimalType)))
            {
                if (StrContains(animal.type, at.ToString()))
                {
                    return at;
                }
            }

            return AnimalType.Unknown;
        }

        public static EffectIcon GetPetEffectIcon(this AnimalType animalType)
        {
            switch (animalType)
            {
                case AnimalType.Chicken: return EffectIcon.Animal_Chicken;
                case AnimalType.Cow: return EffectIcon.Animal_Cow;
                default: return EffectIcon.None;
            }
        }

        public static string GetPetEffectDescription(this AnimalType animalType)
        {
            switch (animalType)
            {
                case AnimalType.Any:
                    return "Acaricia o animal ao tocá-lo";
                case AnimalType.Chicken:
                    return $"Acaricia galinhas ao tocá-las";
                case AnimalType.Duck:
                    return $"Acaricia patos ao tocá-los";
                case AnimalType.Rabbit:
                    return $"Acaricia coelhos ao tocá-los";
                case AnimalType.Dinosaur:
                    return $"Acaricia dinossauros ao tocá-los";
                case AnimalType.Cow:
                    return $"Acaricia vacas ao tocá-las";
                case AnimalType.Goat:
                    return $"Acaricia cabras ao tocá-las";
                case AnimalType.Pig:
                    return $"Acaricia porcos ao tocá-los";
                case AnimalType.Hog:
                    return $"Acaricia porcas ao tocá-las"; ;
                case AnimalType.Sheep:
                    return "Acaricia ovelhas ao tocá-las";
                case AnimalType.Ostrich:
                    return "Acaricia avestruzes ao tocá-las";
                default:
                    return "";

            }
        }

        /// <summary>
        /// Case-insensitive contains
        /// </summary>
        /// <param name="s"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        static bool StrContains(string s, string word)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, word, CompareOptions.IgnoreCase) >= 0;
        }
    }
}
