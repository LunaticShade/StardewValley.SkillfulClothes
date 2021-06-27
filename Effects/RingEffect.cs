using SkillfulClothes.Types;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Effects
{
    /// <summary>
    /// An effect which "simulates" the player wearing a specific ring
    /// (if the game checks if the player is wearing a given ring the result will be true
    ///  if a RingEffect for this ring is worn by the player, see HarmonyPatches.isWearingRing)    
    ///  Other ring's effects will not be replicated by this implementation
    /// </summary>
    class RingEffect : SingleEffect
    {
        public RingType Ring { get; }

        public RingEffect(RingType ring)
        {
            Ring = ring;
        }

        protected override EffectDescriptionLine GenerateEffectDescription() => Ring.GetEffectDescription();

        public override void Apply(Item sourceItem, EffectChangeReason reason)
        {
            // not needed
        }

        public override void Remove(Item sourceItem, EffectChangeReason reason)
        {            
            // not needed
        }        
    }

    enum RingType
    {        
        SlimeCharmerRing = 520,
        YobaRing = 524,
        SturdyRing = 525,
        BurglarsRing = 526        

    }

    static class RingTypeExtensions
    {
        public static EffectDescriptionLine GetEffectDescription(this RingType ring)
        {
            // todo: get description from original item

            switch (ring)
            {
                case RingType.SlimeCharmerRing:
                    return new EffectDescriptionLine(EffectIcon.None, "Previne danos de gosmas"); // EffectIcon.Slime
                case RingType.YobaRing:
                    return new EffectDescriptionLine(EffectIcon.Yoba, "Ocasionalmente protege o usuário de danos");
                case RingType.SturdyRing:
                    return new EffectDescriptionLine(EffectIcon.None, "Corta a duração dos efeitos de status negativo pela metade ");
                case RingType.BurglarsRing:
                    return new EffectDescriptionLine(EffectIcon.None, "Monstros têm uma chance maior de soltar itens");                
                default:
                    return new EffectDescriptionLine(EffectIcon.None, "Efeito Desconhecido");
            }
        }      
    }
}
