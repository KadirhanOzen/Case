using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackDamageSkill : PasiveSkill, IUsable
{
    private void Start()
    {
        SetUp();
        priceText.text = price.ToString();
        levelText.text = level.ToString();
        valueText.text = value.ToString();
       
    }

    public void UseSkill()
    {
        BuySkill(() => { value *= skillTypes.valueMultiplier; });
    }
}