using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	public AudioClip deathClip;

    PlayerMovement playerMovement;
    Animator anim;
    PlayerShooting playerShooting;

    bool isDead;//是否死亡
    bool damaged;//是否收到伤害


	AudioSource playerAudioSource;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

		playerAudioSource = GetComponent<AudioSource> ();

        currentHealth = startingHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

		playerAudioSource.Play ();


        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");//播放死亡动画

		playerAudioSource.clip = deathClip;
		playerAudioSource.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;//禁用玩家射击
    }

    public void RestartLevel()
    {
        //调用当前已经调用的场景
        Application.LoadLevel(Application.loadedLevel);
    }
}
