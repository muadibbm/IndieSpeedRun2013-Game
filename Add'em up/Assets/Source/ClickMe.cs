using UnityEngine;
using System.Collections;

public class ClickMe : MonoBehaviour {
	
	public float speed = 0.15f;
	public int health = 100;
	public int damage = 5;
	public float detectionRadius = 50f;
	public float fightRadius = 5f;
	
	private int max_health;
	private int prevHealth;
	
	private bool bIsR;
	
	private AnimatedSpritesheet animation;
	
	public Material punch_sprite_R;
	public Material punch_sprite_L;
	public Material damage_sprite_R;
	public Material damage_sprite_L;
	public Material death_sprite_R;
	public Material death_sprite_L;
	public Material idle_sprite_R;
	public Material idle_sprite_L;
	public Material runLoop_sprite_R;
	public Material runLoop_sprite_L;
	
	public static bool bIdle;
	public static bool bTakeDamage;
	public static bool bAttacking;
	public static bool bMoveLeft;
	public static bool bMoveRight;
	
	private float posZ;
	private float posX;
	private float posY;
	
	private float rotX;
	private float rotY;
	private float rotZ;
	private float rotW;
	
	void Start () 
	{
		animation = GetComponent<AnimatedSpritesheet>();
		
		posZ = transform.position.z;
		posX = transform.position.x;
		
		rotX = transform.rotation.x;
		rotY = transform.rotation.y;
		rotZ = transform.rotation.z;
		rotW = transform.rotation.w;
		
		bIdle = bTakeDamage = bAttacking = bMoveLeft = bMoveRight = false;
		bIsR = true;
		max_health = health;
	}
	
	void FixedUpdate () 
	{
		transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
		updateAIState();
		updateAnimation();
		prevHealth = health;
		if(bAttacking && !bTakeDamage)
		{
			GameLoop.player.health -= damage;
		}
		if(bMoveLeft && !bAttacking && !bTakeDamage)
		{
			posX -= speed;
			bIsR = false;
		}
		if(bMoveRight && !bAttacking && !bTakeDamage)
		{
			posX += speed;
			bIsR = true;
		}
		if((bMoveLeft || bMoveRight) && !bAttacking && !bTakeDamage)
		{
			transform.position = new Vector3(posX, transform.position.y, posZ);
		}
	}
	
	private void updateAIState()
	{
		bTakeDamage = (prevHealth > health);
		if((Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.x - transform.position.x,2)) < fightRadius))
		{
			bIsR = (GameObject.Find("Player").transform.position.x > transform.position.x);
			bAttacking = true;
		}
		else if((Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.x - transform.position.x,2)) < detectionRadius &&
			Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.y - transform.position.y,2)) < detectionRadius/2) &&
			!bTakeDamage && !bAttacking && health > 0)
		{
			bMoveRight = (GameObject.Find ("Player").transform.position.x > transform.position.x);
			bMoveLeft = (GameObject.Find ("Player").transform.position.x < transform.position.x);
			bAttacking = bTakeDamage = false;
			health = prevHealth = max_health;
		}
		else
		{
			bMoveRight = bMoveLeft = false;
			bAttacking = bTakeDamage = false;
			health = prevHealth = max_health;
		}
		bIdle = (!bTakeDamage && !bAttacking && health > 0 && !bMoveRight && !bMoveLeft);
	}
	
	private void updateAnimation()
	{
		Debug.Log (bAttacking);
		if(bIsR)
		{
			if(bTakeDamage)
			{
				animation.columns = 2;
				animation.rows = 2;
				animation.framesPerSecond = 4;
				renderer.material = damage_sprite_R;
			}
			else if(bAttacking)
			{
				
			}
			else if(bMoveRight)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = runLoop_sprite_R;
			}
			else if(bIdle)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = idle_sprite_R;
			}
		}
		else
		{
			if(bTakeDamage)
			{
				animation.columns = 2;
				animation.rows = 2;
				animation.framesPerSecond = 4;
				renderer.material = damage_sprite_L;
			}
			else if(bAttacking)
			{
				
			}
			else if(bMoveLeft)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = runLoop_sprite_L;
			}
			else if(bIdle)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = idle_sprite_L;
			}
		}
	}
}
