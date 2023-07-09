using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    public delegate void OnEnemyEnteredDelegate(Transform enemy, bool x);

    public delegate void OnDestroyAllEnemyDelegate();
    
    public delegate void OnEnemyDestroyDelegate(Transform enemyTransform);
    
    public delegate void OnSkillUpgradeDelegate(Enums.CharacterProperty property, float value);

    public delegate bool OnSpendMoneyDelegate(int price);

    public delegate void OnEarnMoneyDelegate(int money);

    public delegate void OnFinishGameDelegate();

    public OnEnemyEnteredDelegate OnEnemyEntered;

    public OnEnemyDestroyDelegate OnEnemyDestroy;

    public OnSkillUpgradeDelegate OnSkillUpgrade;

    public OnSpendMoneyDelegate OnSpendMoney;

    public OnDestroyAllEnemyDelegate OnDestroyAllEnemy;

    public OnEarnMoneyDelegate OnEarnMoney;

    public OnFinishGameDelegate OnFinishGame;

    private void Awake()
    {
        instance = this;
    }
}
