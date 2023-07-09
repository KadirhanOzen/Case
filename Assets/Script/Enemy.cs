using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyTypes enemyTypes;
    [SerializeField]
    Transform player;

    [SerializeField]
    float health;
    [SerializeField]
    float speed;

    [SerializeField]
    float damage;

    [SerializeField]
    float attackCoolDown;

    

    [SerializeField]
    bool hit;
    bool isAttacking;

    Vector3 direction;

    int money;

    Animator animator;




    private void SetUp()
    {

        int minute = Mathf.CeilToInt((Time.time - GameManager.instance.startTime) / 60);
        //Debug.Log(Time.time - startTime/60 );   
        health = enemyTypes.healthMultiplier * (Mathf.Pow(minute, 1.9f) + 5 + Mathf.Sin(minute));
        speed = enemyTypes.speed;
        damage = enemyTypes.damage;
        attackCoolDown = enemyTypes.attackCoolDown;
        money = enemyTypes.money;
        player = PlayerController.instance.transform;
        
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        
        SetUp();
       

    }

    private void OnEnable()
    {
        GameEvents.instance.OnDestroyAllEnemy += Death;
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnDestroyAllEnemy -= Death;
    }

    public void TakeDamage(float damage)
    {

        health -= damage;
        if (health > 0)
        {
            animator.SetBool("isDamaged", true);
        }
        else
        {
            Death();
        }

    }

    public void Death()
    {
        GameEvents.instance.OnEnemyDestroy?.Invoke(transform);
        GameEvents.instance.OnEarnMoney?.Invoke(money);
        animator.SetBool("isDeath", true);
        




    }

    public void EndHitEnemyAnimation()
    {
        animator.SetBool("isDamaged", false);

    }

    public void EndDeathAnimation()
    {
        Destroy(gameObject);
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime);




        }

    }

    private void Update()
    {
        FollowPlayer();
        ChangeDirection();

    }

    private void ChangeDirection()
    {
        Vector2 tempDirection = transform.localScale;

        if (direction.x > 0)
        {
            tempDirection.x = 1f;
        }
        else if (direction.x < 0)
        {
            tempDirection.x = -1f;
        }

        transform.localScale = tempDirection;

    }


    IEnumerator AttackingRoutine(PlayerController playerController)
    {
        isAttacking = true;
        while (hit)
        {
            playerController.TakeDamage(damage);
            yield return new WaitForSeconds(attackCoolDown);


        }
        isAttacking = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            hit = true;
            if (!isAttacking)
            {
                StartCoroutine(AttackingRoutine(other.gameObject.GetComponent<PlayerController>()));

            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            hit = false;
        }
    }
}
