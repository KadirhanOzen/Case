using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PasiveSkill : MonoBehaviour
{
    [SerializeField] protected SkillTypes skillTypes;
    [SerializeField] protected Text valueText;
    [SerializeField] protected Text priceText;
    [SerializeField] protected Text levelText;



    protected int level;
    protected float value;
    protected float price;



    protected void SetUp()
    {

        level = skillTypes.level;
        price = skillTypes.price;
        value = skillTypes.value;
        PlayerPrefs.SetFloat(skillTypes.property.ToString() + "Value", value);
        PlayerPrefs.SetInt(skillTypes.property.ToString() + "Level", level);
    }

    public bool BuySkill(Action code)
    {

        if (skillTypes.maxSkillLevel <= level)
            return false;

        if (!GameEvents.instance.OnSpendMoney.Invoke((int)price))
            return false;

        level++;
        if (skillTypes.maxSkillLevel == level)
        {
            Destroy(GetComponent<Button>());
            GetComponent<Image>().color = Color.gray;
        }


        price = Mathf.CeilToInt(price * skillTypes.newPriceMultiplier);

        code.Invoke();
        GameEvents.instance.OnSkillUpgrade?.Invoke(skillTypes.property, value);
        PlayerPrefs.SetFloat(skillTypes.property.ToString() + "Value", value);
        PlayerPrefs.SetInt(skillTypes.property.ToString() + "Level", level);
        valueText.text = value.ToString("F2");

        priceText.text = (level == skillTypes.maxSkillLevel) ? "max" : price.ToString();
        levelText.text = level.ToString();

        return true;
    }
}
