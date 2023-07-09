using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LethalTempoSkill : ActiveSkill, IUsable
{
    public void UseSkill()
    {   
       BuySkill(() => { GameEvents.instance.OnSkillUpgrade?.Invoke(skillTypes.property, skillTypes.value);});

              

    }

   
    
}

