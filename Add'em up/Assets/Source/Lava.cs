using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour 
{
	public int damage = 10;
	
	private bool bKill = false;
	
	void FixedUpdate()
	{
		if(bKill)
			GameLoop.player.health -= damage;
	}
	
	public void kill (bool pKill) 
	{
		bKill = pKill;
	}
}