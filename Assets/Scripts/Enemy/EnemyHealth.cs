using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	public float sinkSpeed = 2.5f;
	public AudioClip deathClip;

    Animator anim;

    bool isDead;

	bool isSinking;

    CapsuleCollider capsuleCollider;
    ParticleSystem hitParticles;
	AudioSource enemyAudioSource;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
		enemyAudioSource = GetComponent<AudioSource> ();

    }

	void Update()
	{
		if (isSinking) {
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= amount;
		enemyAudioSource.Play ();

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

		enemyAudioSource.clip = deathClip;
		enemyAudioSource.Play ();
    }

    public void StartSinking()
    {
		ScoreManager.score += 10;
        GetComponent<NavMeshAgent>().enabled = false;//去除导航效果
		GetComponent<Rigidbody> ().isKinematic = true;
		isSinking = true;
        Destroy(gameObject, 2f);//销毁物体
    }
}
