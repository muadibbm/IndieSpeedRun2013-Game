using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {
	
	public static Player player;
	
	private static List <Renderer> flashObjects;
	private static List <int> flashCounters;
	private float aFlashCounter;
	private bool aFlashCounterIncrement;
	
	void Start () 
	{
		player = GameObject.Find ("Player").GetComponent<Player>();
		player.init();
		Time.fixedDeltaTime = 0.01f;
		flashObjects = new List<Renderer>();
		flashCounters = new List<int>();
		aFlashCounter = 255f;
	}
	
	void FixedUpdate ()
	{
		player.updateStatus();
		updateFlash();
	}
	
	public static void flash(Renderer pRenderer)
	{
		flashObjects.Add (pRenderer);
		flashCounters.Add (0);
	}
	
	private void updateFlash()
	{
		if(aFlashCounter >= 255f)
			aFlashCounterIncrement = false;
		else if(aFlashCounter <= 0)
			aFlashCounterIncrement = true;
		if(aFlashCounterIncrement)
			aFlashCounter += 20f;
		else
			aFlashCounter -= 20f;
		for (int i = 0; i < flashObjects.Count; i++)
		{
			if(255f/(aFlashCounter*10f) <= 1f && 
				255f/(aFlashCounter*10f) > 0.1f)
				flashObjects[i].material.color = new Color(flashObjects[i].material.color.r,
											 flashObjects[i].material.color.g,
											 flashObjects[i].material.color.b,
											 255f/(aFlashCounter*10f));
			if(flashCounters[i] <= 79)
				flashCounters[i]++;
			else
			{
				flashObjects[i].material.color = new Color(flashObjects[i].renderer.material.color.r,
											 flashObjects[i].material.color.g,
											 flashObjects[i].material.color.b,
											 1.0f);
				flashObjects.RemoveAt(i);
				flashCounters.RemoveAt(i);
			}
		}
	}
	
	public static bool hasFlashStopped(Renderer pRenderer)
	{
		return 	!flashObjects.Contains(pRenderer);
	}
}