using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    PlayerMovement playerMovement;
    Animator anim;

    bool isDead;//是否死亡
    bool damaged;//是否收到伤害

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        currentHealth = startingHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");//播放死亡动画

        playerMovement.enabled = false;
    }
}
