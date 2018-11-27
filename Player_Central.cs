using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Central : MonoBehaviour {

	public HUD hud;

	public AudioClip sfxDamange;

	private int state;
	private int health;

	enum playerState{
		Alive,
  		DEAD
	}

	// Use this for initialization
	void Start () {

		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
	}

	// Called by the NPC when attacking player.
	public void TakeDamage(int damage){
		hud.hit();

		health -= damage;
		SoundManager.instance.PlaySingle(sfxDamange);

		//Debug.Log("Player Damage Received: " + damage + " Health: " + health);
		// GetComponent<HUD>().hit();

		if(health <= 0){			
			state = (int)playerState.DEAD;

			//Debug.Log("Player DEAD!!");
			//Destroy(gameObject);
		}
	}

	public int GetPlayerState(){

		return state;
	}
	public int GetPlayerHealth(){

		return health;;
	}
}