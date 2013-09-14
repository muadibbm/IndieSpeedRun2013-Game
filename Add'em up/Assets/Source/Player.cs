using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float jumping_height = 2.0f;
	public float speed = 1.0f;
	
	private GameObject playerObject;
	private float posZ;
	private float posX;
	private float posY;
	private bool bJump;
	private bool bMoveLeft;
	private bool bMoveRight;
	
	public void init () 
	{
		playerObject = GameObject.Find ("Player");
		posZ = playerObject.transform.position.z;
		posX = playerObject.transform.position.x;
		posY = playerObject.transform.position.y;
		bJump = bMoveRight = bMoveLeft = false;
	}
	
	public void updateStatus () 
	{
		updateInput();
		posX = playerObject.transform.position.x;
		posY = playerObject.transform.position.y;
		if(bJump)
			posY += jumping_height;
		if(bMoveLeft)
			posX -= speed;
		if(bMoveRight)
			posX += speed;
		if(bJump || bMoveLeft || bMoveRight)
			playerObject.transform.position = new Vector3(posX, posY, posZ);
		if(bMoveLeft)
			Debug.Log ("left");
		if(bMoveRight)
			Debug.Log ("Right");
		if(bJump)
			Debug.Log("jump");
	}
	
	private void updateInput()
	{
		if(Input.GetKeyDown(KeyCode.UpArrow))
			bJump = true;
		if(Input.GetKeyUp(KeyCode.UpArrow))
			bJump = false;
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			bMoveLeft = true;
		if(Input.GetKeyUp(KeyCode.LeftArrow))
			bMoveLeft = false;
		if(Input.GetKeyDown(KeyCode.RightArrow))
			bMoveRight = true;
		if(Input.GetKeyUp(KeyCode.RightArrow))
			bMoveRight = false;
	}
}
