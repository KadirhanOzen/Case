using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{

    [SerializeField]
    Text money;

    [SerializeField]
    Text time;

    [SerializeField]
    Text damage;

    [SerializeField]
    Text attackSpeed;

    [SerializeField]
    Text winOrLose;

    [SerializeField]
    Enums.CharacterProperty[] characterProperties;

    List<SkillData> skillDatas = new List<SkillData>();

    [SerializeField]
    List<SkillDataUI> skillDataUIs = new List<SkillDataUI>();



    private void Start()
    {   
       // Debug.Log(characterProperties[4].ToString() + "Value");
        //Debug.Log(PlayerPrefs.GetFloat(characterProperties[4].ToString() + "Value"));

        //Debug.Log(PlayerPrefs.GetInt(characterProperties[3].ToString()) + " "+ characterProperties[3].ToString() );
       
        for (int i = 2; i < characterProperties.Length ; i++)
        {

            skillDatas.Add(new SkillData(PlayerPrefs.GetInt(characterProperties[i].ToString() + "Level"), PlayerPrefs.GetFloat(characterProperties[i].ToString() + "Value")));
            
        }

        for (int i = 0; i < 2; i++)
        {
            skillDatas.Add(new SkillData(PlayerPrefs.GetInt(characterProperties[i].ToString() + "Times"), 0));

        }

        Load();

       

    }

    

    public void Load()
    {
        money.text ="Money:  " + PlayerPrefs.GetInt("Money", 0).ToString();
        time.text ="Time:  " + PlayerPrefs.GetString("Time", "0").ToString();
        damage.text ="Damage:  " + PlayerPrefs.GetFloat("Damage", 0).ToString();
        attackSpeed.text ="Rate Time:  "+ PlayerPrefs.GetFloat("AttackSpeed", 0).ToString();
        if(PlayerPrefs.GetInt("WinOrLose") == 1)
        {
            winOrLose.text = "Congratulations! Win";
        }
        else
        {
            winOrLose.text = "Game Over";
        }
        for (int i = 0; i < skillDatas.Count-2; i++)
        {
            
            skillDataUIs[i].levelText.text ="Level:  " + skillDatas[i].level.ToString();
            skillDataUIs[i].valueText.text ="Value:  " + skillDatas[i].value.ToString();
        }

        for (int i = 4; i < 6; i++)
        {
            skillDataUIs[i].valueText.text ="Used " + skillDatas[i].level.ToString() + " times  ";
        }
        if(skillDatas[0].level == 1)
        {
            skillDataUIs[0].valueText.text = "Active";

        }
        else
        {
            skillDataUIs[0].valueText.text = "Deactive";
        }
    }
}




public class SkillData
{
    public int level;
    public float value;
    

    public SkillData(int level, float value)
    {
        this.level = level;
        this.value = value;
       
    }
}

[System.Serializable]
public class SkillDataUI
{
    public Text levelText;

    public Text valueText;

    public SkillDataUI(Text levelText, Text valueText)
    {
        this.levelText = levelText;
        this.valueText = valueText;
    }
}
