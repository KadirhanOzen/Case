using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIElements : MonoBehaviour
{
    [SerializeField] GameObject skillObject;

    void BuySkill()
    {
        skillObject.GetComponent<IUsable>().UseSkill();
    }
   
    
}
