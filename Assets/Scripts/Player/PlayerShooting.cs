using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;//攻击伤害值
    public float timeBetweenBullets = 0.15f;//每发子弹的时间间隔
    public float range = 100f;//开火的范围

    float timer;//计时器
    Ray shootRay;//射线
    RaycastHit shootHit;//保存射线碰撞的结果信息
    int shootableMask;

    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;//枪支开火时播放效果的持续时间的百分比

	AudioSource playerShootAudio;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
		playerShootAudio = GetComponent<AudioSource> ();

    }

    void Update()
    {
        timer += Time.deltaTime;
#if !MOBILE_INPUT
		if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot();
		}

#else
		if(CrossPlatformInputManager.GetAxisFaw("Mouse X") != 0 && CrossPlatformInputManager.GetAxisFaw("Mouse Y") != 0 && timer >= timeBetweenBullets)
		{
			Shoot ();
		}
#endif
        

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffect();
        }
    }

    //关闭枪支特效
    public void DisableEffect()
    {
        gunLine.enabled = false;//关闭线的渲染器
        gunLight.enabled = false;//关闭灯光的效果
    }

    void Shoot()
    {
		playerShootAudio.Play ();
        timer = 0f;
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);//设置线渲染器的第一个点的位置信息

        shootRay.origin = transform.position;//构建射线的起始点位置
        shootRay.direction = transform.forward;//构建射线的方向

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)//如果EnemyHealth不为空，说明碰到敌人，
            {
                enemyHealth.TakeDamage(damagePerShot,shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);//设置线渲染器的第二个点的位置信息
        }
        else
        {
            //设置线渲染器的第二个点的位置信息，能够到达最远的位置
            gunLine.SetPosition(1,shootRay.origin + shootRay.direction * range);
        }
        
    }


    
}
