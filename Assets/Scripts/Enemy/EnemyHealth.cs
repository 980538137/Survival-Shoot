using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    Animator anim;

    bool isDead;

    CapsuleCollider capsuleCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();

    }

    public void TakeDamage(int amount)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Death();
        }

    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;//变成触发器，不会挡住子弹的射线
        anim.SetTrigger("Dead");
    }
}
