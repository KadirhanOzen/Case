using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills")]

public class SkillTypes : ScriptableObject
{
    public Enums.AblityStatus ablityStatus;

    public int level;

    public float value;

    public float price;

    public float valueMultiplier;

    public int maxSkillLevel;

    public float newPriceMultiplier;

    public int coolDown;

    public Enums.CharacterProperty property;








  
}
