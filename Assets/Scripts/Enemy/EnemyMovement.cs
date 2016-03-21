using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if(playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);//设置怪物自动导航目标
        }
        else
        {
            nav.enabled = false;
        }
       
    }
   
}
