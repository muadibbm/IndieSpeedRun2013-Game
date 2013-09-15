using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumping_speed = 0.5f;
	public float speed = 0.3f;
	public float popUp_Speed = 20f;
	public int popUp_CoolDown = 200;
	public int health = 10000;
	public int regen_rate = 1;
	public int damage = 50;
	
	private int max_health;
	private int prevHealth;
	
	private bool bIsR;
	
	private AnimatedSpritesheet animation;

	public Material blink_sprite_R;
	public Material blink_sprite_L;
	public Material damage_sprite_R;
	public Material damage_sprite_L;
	public Material death_sprite_R;
	public Material death_sprite_L;
	public Material idle_sprite_R;
	public Material idle_sprite_L;
	public Material jump_sprite_R;
	public Material jump_sprite_L;
	public Material punch_sprite_R;
	public Material punch_sprite_L;
	public Material runLoop_sprite_R;
	public Material runLoop_sprite_L;
	
	public static bool bFalling;
	public static bool bJumping;
	public static bool bIdle;
	public static bool bPopUp;
	public static bool bTakeDamage;
	public static bool bAttacking;
	
	private int popUpAnimCounter;
	
	private GameObject playerObject;
	private float newJumpingSpeed;
	private float posZ;
	private float posX;
	private float posY;
	private bool bJump;
	private bool bMoveLeft;
	private bool bMoveRight;
	private int popUpCounter;
	private float prevPosX;
	private float prevPosY;
	
	private float rotX;
	private float rotY;
	private float rotZ;
	private float rotW;
	
	private int attackCounter;
	private int deathCounter;
	private int popUpCoolDownCounter;
	
	public void init () 
	{
		playerObject = GameObject.Find ("Player");
		animation = playerObject.GetComponent<AnimatedSpritesheet>();
		posZ = playerObject.transform.position.z;
		posX = playerObject.transform.position.x;
		posY = playerObject.transform.position.y;
		rotX = playerObject.transform.rotation.x;
		rotY = playerObject.transform.rotation.y;
		rotZ = playerObject.transform.rotation.z;
		rotW = playerObject.transform.rotation.w;
		bJump = bMoveRight = bMoveLeft = bFalling = false;
		bFalling = bJumping = bIdle = bPopUp = bTakeDamage = bAttacking = false;
		newJumpingSpeed = jumping_speed;
		bIsR = true;
		popUpAnimCounter = 0;
		max_health = health;
		attackCounter = 0;
		popUpCoolDownCounter = popUp_CoolDown;
	}
	
	public void updateStatus ()
	{
		transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
		updatePlayerState();
		updateAnimation();
		updateInput();
		prevPosX = posX = playerObject.transform.position.x;
		prevPosY = posY = playerObject.transform.position.y;
		prevHealth = health;
		if(bJump)
		{
			posY += newJumpingSpeed;
			newJumpingSpeed  = newJumpingSpeed / 1.01f;
		}
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
		if(bJump || bMoveLeft || bMoveRight)
		{
			playerObject.transform.position = new Vector3(posX, posY, posZ);
		}
		if(bFalling)
		{
			newJumpingSpeed = jumping_speed;
			bJump = false;
		}
		if(health >= max_health)
			health = max_health;
		else
			health += regen_rate;
		if(bAttacking)
			attackCounter++;
		if(attackCounter > 40)
		{
			bAttacking = false;
			attackCounter = 0;
		}
		if(deathCounter > 40)
		{
			renderer.enabled = collider.enabled = false;
		}
		if(deathCounter > 60)
		{
			Application.LoadLevel("Beat'em Adds");
		}
		if(popUpCoolDownCounter < popUp_CoolDown)
			popUpCoolDownCounter++;
		else
			popUpCoolDownCounter = popUp_CoolDown;
	}
	
	private void updatePlayerState()
	{
		bIdle = (Mathf.Abs(playerObject.transform.position.x - prevPosX) < speed && Mathf.Abs(playerObject.transform.position.y - prevPosY) < speed);
		bFalling = (prevPosY > playerObject.transform.position.y && Mathf.Abs(playerObject.transform.position.y - prevPosY) > 0.1f);
		bJumping = (prevPosY < playerObject.transform.position.y && Mathf.Abs(playerObject.transform.position.y - prevPosY) > 0.1f);
		bTakeDamage = (prevHealth > health);
//		if(bIdle)
//			Debug.Log ("is Idle");
//		if(bJumping)
//			Debug.Log ("is Jumping");
//		if(bFalling)
//			Debug.Log ("is Falling");
//		if(bMoveLeft)
//			Debug.Log ("is Moving left");
//		if(bMoveRight)
//			Debug.Log ("is Moving right");
	}
	
	private void updateAnimation()
	{
		if(bIsR)
		{
			if(health < 0)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = death_sprite_R;	
				deathCounter++;
			}
			else if(bPopUp)
			{
				popUpAnimCounter++;
				if(popUpAnimCounter <= 30)
				{
					animation.columns = 3;
					animation.rows = 2;
					animation.framesPerSecond = 24;
					renderer.material = blink_sprite_R;
				}
				if(popUpAnimCounter > 30)
				{
					popUp();
					popUpAnimCounter = 0;
					bPopUp = false;
				}
			}
			else if(bAttacking)
			{
				animation.columns = 4;
				animation.rows = 2;
				animation.framesPerSecond = 16;
				renderer.material = punch_sprite_R;
			}
			else if(bTakeDamage)
			{
				animation.columns = 4;
				animation.rows = 2;
				animation.framesPerSecond = 8;
				renderer.material = damage_sprite_R;	
			}
			else if(bJumping)
			{
				animation.columns = 5;
				animation.rows = 3;
				animation.framesPerSecond = 7;
				renderer.material = jump_sprite_R;	
			}
			else if(bMoveRight)
			{
				animation.columns = 2;
				animation.rows = 1;
				animation.framesPerSecond = 2;
				renderer.material = runLoop_sprite_R;
			}
			else if(bIdle)
			{
				if(health > 0)
				{
					animation.columns = 3;
					animation.rows = 2;
					animation.framesPerSecond = 6;
					renderer.material = idle_sprite_R;
				}
			}
		}
		else
		{
			if(health < 0)
			{
				animation.columns = 3;
				animation.rows = 2;
				animation.framesPerSecond = 6;
				renderer.material = death_sprite_L;
				deathCounter++;
			}
			else if(bPopUp)
			{
				popUpAnimCounter++;
				if(popUpAnimCounter <= 30)
				{
					animation.columns = 3;
					animation.rows = 2;
					animation.framesPerSecond = 24;
					renderer.material = blink_sprite_L;
				}
				if(popUpAnimCounter > 30)
				{
					popUp();
					popUpAnimCounter = 0;
					bPopUp = false;
				}
			}
			else if(bAttacking)
			{
				animation.columns = 4;
				animation.rows = 2;
				animation.framesPerSecond = 16;
				renderer.material = punch_sprite_L;
			}
			else if(bTakeDamage)
			{
				animation.columns = 4;
				animation.rows = 2;
				animation.framesPerSecond = 8;
				renderer.material = damage_sprite_L;	
			}
			else if(bJumping)
			{
				animation.columns = 5;
				animation.rows = 3;
				animation.framesPerSecond = 7;
				renderer.material = jump_sprite_L;	
			}
			else if(bMoveLeft)
			{
				animation.columns = 2;
				animation.rows = 1;
				animation.framesPerSecond = 2;
				renderer.material =  runLoop_sprite_L;
			}
			else if(bIdle)
			{
				if(health > 0)
				{
					animation.columns = 3;
					animation.rows = 2;
					animation.framesPerSecond = 6;
					renderer.material = idle_sprite_L;		
				}
			}
		}
	}
	
	private void updateInput()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			if(bIdle)
				bJump = true;
		}
		if(Input.GetKeyDown(KeyCode.A))
			bMoveLeft = true;
		if(Input.GetKeyUp(KeyCode.A))
			bMoveLeft = false;
		if(Input.GetKeyDown(KeyCode.D))
			bMoveRight = true;
		if(Input.GetKeyUp(KeyCode.D))
			bMoveRight = false;
		if(Input.GetKeyDown(KeyCode.Space) && popUpCoolDownCounter == popUp_CoolDown)
			bPopUp = true;
		if(Input.GetKeyDown(KeyCode.Return))
			bAttacking = true;
	}
	
	private void popUp()
	{
		popUpCoolDownCounter = 0;
		if(bIsR)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x + popUp_Speed, playerObject.transform.position.y, posZ);
		else
			playerObject.transform.position = new Vector3(playerObject.transform.position.x - popUp_Speed, playerObject.transform.position.y, posZ);
		if(bJumping)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + popUp_Speed, posZ);
	}
	
	void OnCollisionStay(Collision collision) {
		if(collision.gameObject.GetComponent<ClickMe>()!=null)
		{
			if(bAttacking)
				collision.gameObject.GetComponent<ClickMe>().health -= damage;
		}
	}
}