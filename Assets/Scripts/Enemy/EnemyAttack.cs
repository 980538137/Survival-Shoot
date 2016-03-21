using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks =1f;
    public int attackDamage = 20;//攻击的伤害值

    Animator anim;
    GameObject player;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    bool playerInRange;//是否在攻击的范围
    float timer;//计时器

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        //如果触发了触发器的是主角，这样的话设置playerInrange为真
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (playerInRange && timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
            Attack();
        } 
        //如果角色死亡,怪物切换到空闲动画
        if (playerHealth.currentHealth <= 0)
        {
            print("PlayerDead");
            anim.SetTrigger("PlayerDead");//播放怪物空闲动画
        }
    }

    //实现怪物攻击
    void Attack()
    {
        timer = 0;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

}
