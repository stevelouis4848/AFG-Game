using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public HUD hud;

	public AudioClip sfxDestory;
	public AudioClip sfxDownGrade;
	public AudioClip sfxHit;


	public GameObject prefab;
	public float repeatTime = 10f;
	public float firstSpawnTime = 100f;
	public int maxSpawn = 10;

    private GameObject player;
	public int hitsToChange = 3;
	public Sprite lvlThreeSprite;
	public Sprite lvlTwoSprite;
	public Sprite lvlOneSprite;

	public int spawningDistance;

	private int currentSpawn = 0;
    private bool spawning = false;
	private int recivedHits = 0;
	private int currentLvl = 3;

	private SpriteRenderer spriteR;

	private GameObject testing;

	void Start()
	{
        player = GameObject.Find("Player(Clone)");
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		spriteR.sprite = lvlThreeSprite;
	}

	void Update()
	{
        // Cancel all Invoke calls
        float dist = Vector2.Distance(this.transform.position, player.transform.position);
        if(dist <= spawningDistance && !spawning)
        {
            spawning = true;
            InvokeRepeating("Spawn", firstSpawnTime, repeatTime);
        }
		if (currentSpawn > maxSpawn)
		{
			CancelInvoke();
		}
	}

	void Spawn()
	{
		Instantiate(prefab, transform.position, Quaternion.identity);
		currentSpawn++;
	}

	public void TakeDamage()
	{
		
		float distanceToTarget = Vector2.Distance(transform.position, player.transform.position);

		if(distanceToTarget <= player.GetComponent<Player_Movement>().attackRange){
			int timeToChange = recivedHits % hitsToChange;

			if (timeToChange == 0 && !(recivedHits == 0))
			{
				recivedHits = 0;
				currentLvl--;
				if (currentLvl == 2)
				{
					spriteR.sprite = lvlTwoSprite;
					SoundManager.instance.PlaySingle(sfxDownGrade);
				} else if (currentLvl == 1)
				{
					spriteR.sprite = lvlOneSprite;
					SoundManager.instance.PlaySingle(sfxDownGrade);
				} else
				{
					spriteR.sprite = null;
					SoundManager.instance.PlaySingle(sfxDownGrade);
					SoundManager.instance.PlaySingle(sfxDestory);
					hud.subGeneratorToHUD();
					Destroy(gameObject);
					
				}
			} else
			{
				recivedHits++;
				SoundManager.instance.PlaySingle(sfxHit);
			}
		}
	}
}
