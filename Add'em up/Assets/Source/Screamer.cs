using UnityEngine;
using System.Collections;

public class Screamer : MonoBehaviour {
	
	public float speed = 0.05f;
	public int damage = 50;
	public float detectionRadius = 60f;
	public float fightRadius = 2f;
	
	private int max_health;
	private int prevHealth;
	
	private bool bIsR;
	
	private AnimatedSpritesheet animation;
	
	public Material idle_sprite_R;
	public Material idle_sprite_L;
	public Material runLoop_sprite_R;
	public Material runLoop_sprite_L;
	
	public static bool bIdle;
	public static bool bAttacking;
	public static bool bMoveLeft;
	public static bool bMoveRight;
	
	private float posZ;
	private float posX;
	private float posY;
	
	private float floatingSpeed;
	private bool bFloatUp = true;
	private bool bFloatDown = false;
	
	private float rotX;
	private float rotY;
	private float rotZ;
	private float rotW;
	
	void Start () 
	{
		animation = GetComponent<AnimatedSpritesheet>();
		posZ = transform.position.z;
		posX = transform.position.x;
		posY = transform.position.y;
		rotX = transform.rotation.x;
		rotY = transform.rotation.y;
		rotZ = transform.rotation.z;
		rotW = transform.rotation.w;
		bIdle =  bAttacking = bMoveLeft = bMoveRight = false;
		bIsR = true;
	}
	
	void FixedUpdate () 
	{
		transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
		updateAIState();
		updateAnimation();
		if(bAttacking)
		{
			GameLoop.player.health -= damage;
		}
		if(bFloatUp)
		{
			floatingSpeed += 0.003f;
			if(floatingSpeed >= 0.1f)
			{
				floatingSpeed = 0.1f;	
				bFloatUp = false;
				bFloatDown = true;
			}
		}
		if(bFloatDown)
		{
			floatingSpeed -= 0.003f;
			if(floatingSpeed <= -0.1f)
			{
				floatingSpeed = 0;	
				bFloatUp = true;
				bFloatDown = false;
			}
		}
		if(bFloatUp)
			posY += floatingSpeed;
		if(bMoveLeft)
		{
			posX -= speed;
			bIsR = false;
		}
		if(bMoveRight)
		{
			posX += speed;
			bIsR = true;
		}
		transform.position = new Vector3(posX, posY, posZ);
	}
	
	private void updateAIState()
	{
		if((Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.x - transform.position.x,2)) < fightRadius) &&
			(Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.y - transform.position.y,2)) < fightRadius))
		{
			bIsR = (GameObject.Find("Player").transform.position.x > transform.position.x);
			bAttacking = true;
		}
		else if((Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.x - transform.position.x,2)) < detectionRadius &&
			Mathf.Sqrt(Mathf.Pow(GameObject.Find ("Player").transform.position.y - transform.position.y,2)) < detectionRadius/2))
		{
			bMoveRight = (GameObject.Find ("Player").transform.position.x > transform.position.x);
			bMoveLeft = (GameObject.Find ("Player").transform.position.x < transform.position.x);
			bAttacking = false;
		}
		else
		{
			bMoveRight = bMoveLeft = false;
			bAttacking = false;
		}
		bIdle = (!bMoveRight && !bMoveLeft);
	}
	
	private void updateAnimation()
	{
		if(bIsR)
		{
			if(bAttacking)
			{
				animation.columns = 4;
				animation.rows = 3;
				animation.framesPerSecond = 12;
				renderer.material = idle_sprite_R;
			}
			else if(bMoveRight)
			{
				animation.columns = 2;
				animation.rows = 2;
				animation.framesPerSecond = 4;
				renderer.material = runLoop_sprite_R;
			}
			else if(bIdle)
			{
				animation.columns = 4;
				animation.rows = 3;
				animation.framesPerSecond = 12;
				renderer.material = idle_sprite_R;
			}
		}
		else
		{
			if(bAttacking)
			{
				animation.columns = 4;
				animation.rows = 3;
				animation.framesPerSecond = 12;
				renderer.material = idle_sprite_L;
			}
			else if(bMoveLeft)
			{
				animation.columns = 2;
				animation.rows = 2;
				animation.framesPerSecond = 4;
				renderer.material = runLoop_sprite_L;
			}
			else if(bIdle)
			{
				animation.columns = 4;
				animation.rows = 3;
				animation.framesPerSecond = 12;
				renderer.material = idle_sprite_L;
			}
		}
	}
}
