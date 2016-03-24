using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    Animator anim;

    bool isDead;

    CapsuleCollider capsuleCollider;
    ParticleSystem hitParticles;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();

    }

    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;//设置例子效果播放的位置
        hitParticles.Play();
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

    public void StartSinking()
    {
        print("StartSinking");
        GetComponent<NavMeshAgent>().enabled = false;//去除导航效果
        Destroy(gameObject, 2f);//销毁物体
    }
}
