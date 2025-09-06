using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AIState : ScriptableObject
    {
        public virtual AIState Tick(AICharacterManager aiCharacter)
        {
            return this;
        }
    }
}
