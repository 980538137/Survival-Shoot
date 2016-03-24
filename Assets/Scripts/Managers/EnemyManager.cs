using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float SpawnWaitTime = 2f;//
	public float spawnTime = 3f;//generate zom time;
	public Transform[] spawnPoints;

	void Start()
	{
		InvokeRepeating ("Spawn",SpawnWaitTime,spawnTime);
	}

	//generate zom
	void Spawn()
	{
		if (playerHealth.currentHealth <= 0) {
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);

	}

}
