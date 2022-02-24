using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillfulClothes.Types
{
    /**
     * Interface for parameters of effects
     **/
    public interface IEffectParameters
    {
        
    }    

    public class NoEffectParameters : IEffectParameters
    {
        public static NoEffectParameters Default = new NoEffectParameters();

        // --
    }
}