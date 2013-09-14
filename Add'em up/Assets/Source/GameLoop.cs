using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {
	
	public static Player player;
	
	void Start () 
	{
		player = GameObject.Find ("Player").GetComponent<Player>();
		player.init();
		Time.fixedDeltaTime = 0.01f;
	}
	
	void FixedUpdate ()
	{
		player.updateStatus();
	}
}