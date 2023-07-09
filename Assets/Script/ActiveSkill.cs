using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ActiveSkill : MonoBehaviour
{
    [SerializeField] protected SkillTypes skillTypes;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected Text priceText;

    int usedTimes = 0;

    private float price;
    private float coolDown;

    private void Start()
    {
        SetUp();
        priceText.text = price.ToString();
    }

    private void SetUp()
    {
        price = skillTypes.price;
        PlayerPrefs.SetInt(skillTypes.property.ToString() + "Times", usedTimes);

    }

    public bool BuySkill(Action code)
    {

        if (coolDown > 0)
            return false;

        if (!GameEvents.instance.OnSpendMoney.Invoke((int)price))
            return false;

        fillImage.fillAmount = 1;

        usedTimes++;
        
        price = Mathf.CeilToInt(price * skillTypes.newPriceMultiplier);

        
        PlayerPrefs.SetInt(skillTypes.property.ToString() + "Times", usedTimes);

        


        coolDown = skillTypes.coolDown;
        StartCoroutine(CoolDownCounter());

        code.Invoke();

        return true;

    }

    IEnumerator CoolDownCounter()
    {
        float baseCoolDown = coolDown;
        while (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            fillImage.fillAmount -= Time.deltaTime / baseCoolDown;
            yield return null;
        }
        coolDown = 0;
    }
}
