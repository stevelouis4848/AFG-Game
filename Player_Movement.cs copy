using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	public AudioClip sfxAttack;

	public Animator playerAnimator;
	public float maxSpeed = 4f;
	bool facingRight = true;

	public float timeBtwAttacks ;
	public float attackRange;
	public LayerMask blockingLayer;
	public int enemyDamage;
	public int spawnDamage;
	private float timeUntilNxtAttack;
	private Rigidbody2D rb;
	private float lastGoodVelocityX;
	private	float lastGoodVelocityY;


	// Use this for initialization
	void Start () {
		SetParameters();
	}

	void SetParameters(){
		playerAnimator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		timeBtwAttacks = .01f;
		attackRange = 1f;
		enemyDamage = 25;
		spawnDamage = 10;
		lastGoodVelocityX = 0.0f;
		lastGoodVelocityY = 1f;
		blockingLayer = LayerMask.GetMask("BlockingLayer");
	}
	
	void FixedUpdate () {
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		
		Movement(moveX, moveY);

		float velocityX = rb.velocity.x;
		float velocityY = rb.velocity.y;

		// changes the direction that the player faces and attacks in.
		if(velocityX < .05 && velocityX > -.05 && velocityY < .05 && velocityY > -.05 ){
			velocityX = lastGoodVelocityX;
			velocityY = lastGoodVelocityY;

		}
		else{

			lastGoodVelocityX = velocityX;
			lastGoodVelocityY = velocityY;
		}

		FacingDirection(velocityX, velocityY);
		Attack(velocityX, velocityY);
	}

	void Movement(float moveX, float moveY){

		//animator = gameObject.GetComponent<Animator>();
		Vector2 velocity =  new Vector2(moveX * maxSpeed, moveY * maxSpeed);
		GetComponent<Rigidbody2D>().velocity = velocity;
		//animator.SetFloat("speed", velocity.magnitude);
	}
	void FacingDirection(float velocityX, float velocityY){
		

		//animation set  the sprite to face upward
		if(velocityY > 0 && velocityX == 0){

			// Runs correct animation for idle or move
			
				playerAnimator.SetBool("playerUpIdle", true);
				
				//Debug.Log("facing up");

				//Set everything else to false
				playerAnimator.SetBool("playerDiagUpIdle", false);
				playerAnimator.SetBool("playerRightIdle", false);
				playerAnimator.SetBool("playerDiagDownIdle", false);
				playerAnimator.SetBool("playerDownIdle", false);
		}

		// animate sprite diagonal up
		else if(velocityY > 0 && velocityX != 0 ){

			playerAnimator.SetBool("playerDiagUpIdle", true);
			//Debug.Log("facing Diag UP");
			//Set everything else to false
			playerAnimator.SetBool("playerUpIdle", false);
			playerAnimator.SetBool("playerRightIdle", false);
			playerAnimator.SetBool("playerDiagDownIdle", false);
			playerAnimator.SetBool("playerDownIdle", false);
		}

		// animate sprite right
		else if(velocityY == 0 && velocityX != 0 ){

			playerAnimator.SetBool("playerRightIdle", true);

			//Debug.Log("facing Right");
			//Set everything else to false
			playerAnimator.SetBool("playerUpIdle", false);
			playerAnimator.SetBool("playerDiagUpIdle", false);
			playerAnimator.SetBool("playerDiagDownIdle", false);
			playerAnimator.SetBool("playerDownIdle", false);
		}

		// animate sprite diagnol down
		else if(velocityY < 0 && velocityX != 0 ){

			playerAnimator.SetBool("playerDiagDownIdle", true);
			//Debug.Log("facing Diag Down");
			//Set everything else to false
			playerAnimator.SetBool("playerUpIdle", false);
			playerAnimator.SetBool("playerDiagUpIdle", false);
			playerAnimator.SetBool("playerRightIdle", false);
			playerAnimator.SetBool("playerDownIdle", false);


		}

		//animation set set the sprite to face downward 
		else if(velocityY < 0 && velocityX == 0){

		playerAnimator.SetBool("playerDownIdle", true);
		//Debug.Log("facing Down");
		//Set everything else to false
		playerAnimator.SetBool("playerUpIdle", false);
		playerAnimator.SetBool("playerDiagUpIdle", false);
		playerAnimator.SetBool("playerRightIdle", false);
		playerAnimator.SetBool("playerDiagDownIdle", false);
		}
	
		if(velocityX > 0 && !facingRight){
			flip();
		}
		else if(velocityX < 0 && facingRight){
			flip();
		}

		playerAnimator.SetFloat("Speed", rb.velocity.magnitude);
	}

	void Attack(float velocityX, float velocityY){
		// Debug.Log("attackX: " + velocityX + " attackY: " + velocityY);
		if(timeUntilNxtAttack <= 0 && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){

			SoundManager.instance.PlaySingle(sfxAttack);

			playerAnimator.SetTrigger("Attacking");
			// Resets attack interval time
			timeUntilNxtAttack = timeBtwAttacks;
			// distance between Npc and player.
			AttackHelper(velocityX, velocityY);			
		}	
		else{			
			// Not enought time has passed for next attack and interval time is decremented.
			timeUntilNxtAttack -= Time.deltaTime;
			//Debug.Log("Attack Interval decreased to" + timeUntilNxtAttack);		
		}		
	}
	
	/*
	void AttackHelper(){
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...

			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D  hit;
			hit = Physics2D.Raycast(transform.position + offSetRay, rayDirection, attackRange, blockingLayer);
			if (Physics.Raycast(ray, out hit)){
		       // the object identified by hit.transform was clicked
		       // do whatever you want
	     	}
   }
 }
	}

	*/

	void AttackHelper(float velocityX, float velocityY){
		Vector3 rayDirection = new Vector3(velocityX, velocityY, 0);
	
		RaycastHit2D  hit;
		rayDirection = rayDirection.normalized;

		Vector3 offSetRay = new Vector3( rayDirection.x * .5f, rayDirection.y *.5f, 0);
		Debug.DrawRay(transform.position, rayDirection * attackRange, Color.green); 
		hit = Physics2D.Raycast(transform.position + offSetRay, rayDirection, attackRange, blockingLayer);

		


		if (hit.collider != null){

			Debug.Log("colllider that ray hits" + hit.collider.name);

			if(hit.collider.tag == "NPC"){
				hit.collider.gameObject.GetComponent<NPC_AI>().TakeDamage(enemyDamage);
			}
			else if(hit.collider.tag == "SpawnPoint"){
				hit.collider.gameObject.GetComponent<SpawnPoint>().TakeDamage();
			}
		}	
		else{
			//Debug.Log("Did not hit an enemy");
		}
	}


	

	void flip(){

		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *=-1;
		transform.localScale = theScale;
	}
}