using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject endUI;

    [SerializeField]
    Text endText;

    
    int money = 0;

    [SerializeField]
    Image timeBarImage;

    [SerializeField]
    Text timeCounter;

    [SerializeField]
    Text moneyText;

    int seconds;

    int minute;

    float remaningTime =900;

    public float startTime;



    

    private void OnEnable()
    {
        GameEvents.instance.OnSpendMoney += SpendMoney;
        GameEvents.instance.OnEarnMoney += EarnMoney;
        GameEvents.instance.OnFinishGame += FinishGame;

    }

    private void OnDisable()
    {
        GameEvents.instance.OnSpendMoney -= SpendMoney;
        GameEvents.instance.OnEarnMoney -= EarnMoney;
        GameEvents.instance.OnFinishGame += FinishGame;
    }

    private void Start()
    {
        
        instance = this;

        moneyText.text = money.ToString();

        Time.timeScale = 1;

        startTime = Time.time;
    }
    bool SpendMoney(int price)
    {
        

        if (money >= price)
        {
            
            money -= price;
            moneyText.text =money.ToString();
            PlayerPrefs.SetInt("Money", money);
            return true;
            
        }
        return false;
    }

    void EarnMoney(int value)
    {
        
        money += value;

        moneyText.text =money.ToString();

    }

    void FinishGame()
    {
        

        float elapsedTime  = 900 - remaningTime;

        int elapsedTimeminute = Mathf.FloorToInt(elapsedTime / 60);

        int elapsedTimeseconds = Mathf.FloorToInt(elapsedTime % 60);

        string elapsedTimeString = elapsedTimeminute.ToString() + " : " + elapsedTimeseconds.ToString();
        
        PlayerPrefs.SetInt("Money", money);

        PlayerPrefs.SetString("Time", elapsedTimeString);

        endUI.SetActive(true);

        if(remaningTime > 0)
        {
            PlayerPrefs.SetInt("WinOrLose", 0);
            endText.text = "Game Over";
        }else{

            PlayerPrefs.SetInt("WinOrLose", 1);
            endText.text = "Congratulations! Win";
        }
        Time.timeScale = 0;
    }


    

    private void Update() 
    {

        if(Input.GetKeyDown(KeyCode.K))
        {
            remaningTime = 0;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            GameEvents.instance.OnEarnMoney.Invoke(10000);
        }

       

        minute = Mathf.FloorToInt(remaningTime / 60);

        seconds = Mathf.FloorToInt(remaningTime % 60);

        timeCounter.text = "Remaning Time: " + minute.ToString() +":" + seconds.ToString();

        timeBarImage.fillAmount = remaningTime/900;

         if(remaningTime <= 0)
        {
            remaningTime = 0;
            GameEvents.instance.OnFinishGame.Invoke();
        }
        else{
            remaningTime -= Time.deltaTime;
        }

        


        


    } 

    
        

    

}
