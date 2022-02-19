using SkillfulClothes.Types;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects.Special
{
    class PetAnimalOnTouch : SingleEffect
    {
        AnimalType AnimalType { get; }

        public PetAnimalOnTouch()
            : this(AnimalType.Any)
        {

        }

        public PetAnimalOnTouch(AnimalType whichAnimal)
        {
            AnimalType = whichAnimal;
        }

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked -= GameLoop_UpdateTicked;
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        }

        private void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            if (!Context.IsPlayerFree) return;

            if (Game1.currentLocation is IAnimalLocation loc)
            {
                CheckPetAnimal(loc, Game1.player);
            }
        }

        private bool canPet(FarmAnimal animal)
        {
            // petting after 19:00 will display a dialogue box, soft-locking the game
            return Game1.timeOfDay < 1900 && !animal.wasPet;
        }

        private void CheckPetAnimal(IAnimalLocation location, Farmer who)
        {
            foreach (KeyValuePair<long, FarmAnimal> kvp in location.Animals.Pairs)
            {
                FarmAnimal animal = kvp.Value;
                if (AnimalType == AnimalType.Any || animal.GetAnimalType() == AnimalType)
                {
                    if (canPet(animal) && animal.GetCursorPetBoundingBox().Contains((int)who.position.X, (int)who.position.Y))
                    {                        
                        animal.pet(who, false);
                    }
                }
            }
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {
            EffectHelper.ModHelper.Events.GameLoop.UpdateTicked -= GameLoop_UpdateTicked;
        }

        protected override EffectDescriptionLine GenerateEffectDescription()
        {                     
           return new EffectDescriptionLine(AnimalType.GetPetEffectIcon(), AnimalType.GetPetEffectDescription());            
        }
    }
}
