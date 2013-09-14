using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
	
	public int fadeValue = 100;
	
	private int fadeCounter = 255;
	private Color color;
	private bool bFade = false;
	
	void Start()
	{
		color = renderer.material.color;	
	}
	
	void FixedUpdate()
	{
		if(bFade)
		{
			color.a = fadeCounter;
			renderer.material.color = color;
			fadeCounter--;
			if(fadeCounter == fadeValue)
				collider.enabled = false;
			if(fadeCounter <= 0)
			{
				renderer.enabled = false;
				return;
			}
		}
		Debug.Log (fadeCounter);
	}
	
	public void fade () 
	{
		bFade = true;
	}
}