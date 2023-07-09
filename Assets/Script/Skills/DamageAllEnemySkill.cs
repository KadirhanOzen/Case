using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAllEnemySkill : ActiveSkill, IUsable
{
    

    public void UseSkill()
    {   
        BuySkill(() => { GameEvents.instance.OnDestroyAllEnemy?.Invoke();});

        

    }
}
