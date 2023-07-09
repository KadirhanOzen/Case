using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    FireManager fireManager;
    public static PlayerController instance;

    [SerializeField]
    Image healthBar;

    [SerializeField]
    float speed;

    [SerializeField]
    float health, maxHealth;

    [SerializeField]
    float invincibilityTime;

    float invincibilityCounter;

    Animator animator;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        GameEvents.instance.OnSkillUpgrade += UseSkill;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        fireManager = GetComponent<FireManager>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        invincibilityCounter -= Time.deltaTime;
        ChangeDirection();

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        if ((rb.velocity.magnitude) > 0)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        HealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (invincibilityCounter <= 0)
        {
            health -= damage;
            if (health > 0)
            {
                invincibilityCounter = invincibilityTime;
                animator.SetBool("isDamage", true);
            }
            else
            {
                GameEvents.instance.OnFinishGame.Invoke();
                
            }
        }
    }

    void HealthBar()
    {
        healthBar.fillAmount = health/maxHealth;
    }

    public void EndHitPAnimation()
    {
        animator.SetBool("isDamage", false);
    }

    void UseSkill(Enums.CharacterProperty property, float value)
    {
        switch (property)
        {
            case Enums.CharacterProperty.AttackDamage:
                fireManager.damage = value;
                break;
            case Enums.CharacterProperty.AttackSpeed:
                if (fireManager.isActiveLethal)
                {
                    fireManager.tempReloudTime = value;
                }
                else
                {
                    fireManager.reloudTime = value;
                }
                break;
            case Enums.CharacterProperty.AttackCount:
                fireManager.bulletCount = (int)value;
                break;
            case Enums.CharacterProperty.AttackDiagonal:
                fireManager.isActiveDiagonal = true;
                break;
            case Enums.CharacterProperty.LethalTempo:
                StartCoroutine(fireManager.SkillFireTimer());
                break;
        }
    }

    private void ChangeDirection()
    {
        Vector2 tempDirection = transform.localScale;

        if (rb.velocity.x > 0)
        {
            tempDirection.x = 1f;
        }
        else if (rb.velocity.x < 0)
        {
            tempDirection.x = -1f;
        }

        transform.localScale = tempDirection;
    }
}