using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float jumping_height = 100.0f;
	public float jumping_speed = 0.5f;
	public float speed = 0.2f;
	public float popUp_Speed = 5.0f;
	
	public static bool bFalling;
	public static bool bJumping;
	public static bool bIdle;
	
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
	
	public void init () 
	{
		playerObject = GameObject.Find ("Player");
		posZ = playerObject.transform.position.z;
		posX = playerObject.transform.position.x;
		posY = playerObject.transform.position.y;
		bJump = bMoveRight = bMoveLeft = bFalling = false;
		bFalling = bJumping = bIdle = false;
		newJumpingSpeed = jumping_speed;
	}
	
	public void updateStatus ()
	{
		updatePlayerState();
		updateInput();
		prevPosX = posX = playerObject.transform.position.x;
		prevPosY = posY = playerObject.transform.position.y;
		if(bJump)
		{
			posY += newJumpingSpeed;
			newJumpingSpeed  = newJumpingSpeed / 1.01f;
		}
		if(bMoveLeft)
			posX -= speed;
		if(bMoveRight)
			posX += speed;
		if(bJump || bMoveLeft || bMoveRight)
		{
			playerObject.transform.position = new Vector3(posX, posY, posZ);
		}
		if(bFalling)
		{
			newJumpingSpeed = jumping_speed;
			bJump = false;
		}
	}
	
	private void popUp()
	{
		if(prevPosX > playerObject.transform.position.x)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x - popUp_Speed, playerObject.transform.position.y, posZ);
		else if(prevPosX < playerObject.transform.position.x)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x + popUp_Speed, playerObject.transform.position.y, posZ);
		else if(prevPosY < playerObject.transform.position.y)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + popUp_Speed, posZ);
		else if(prevPosY > playerObject.transform.position.y)
			playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - popUp_Speed, posZ);
	}
	
	private void updatePlayerState()
	{
		bIdle = (Mathf.Abs(playerObject.transform.position.x - prevPosX) < speed && Mathf.Abs(playerObject.transform.position.y - prevPosY) < speed);
		bFalling = (prevPosY > playerObject.transform.position.y && Mathf.Abs(playerObject.transform.position.y - prevPosY) > 0.1f);
		bJumping = (prevPosY < playerObject.transform.position.y && Mathf.Abs(playerObject.transform.position.y - prevPosY) > 0.1f);
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
		if(Input.GetKeyDown(KeyCode.Space))
			popUp();
	}
}
