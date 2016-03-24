using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public PlayerHealth playerHealth;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
	}
}
