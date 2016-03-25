using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	public AudioClip deathClip;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed;
	public Color flashColor = new Color (1f,0f,0f,0.1f);

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
		if (damaged) {
			damageImage.color = flashColor;
		} 
		else
		{
			damageImage.color = Color.Lerp(damageImage.color,Color.clear,flashSpeed);
		}
		damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

		healthSlider.value = currentHealth;

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
