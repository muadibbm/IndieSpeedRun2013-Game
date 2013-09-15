using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {

	public int fadeValue = 300;
	private bool bFade = false;

	void FixedUpdate()
	{
		if(bFade)
		{
			GameLoop.flash(this.renderer);
			fadeValue--;
			if(fadeValue <= 0)
			{
				renderer.enabled = collider.enabled = false;
				return;
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
    	if(collision.collider.Equals(GameObject.Find("Player").collider))
		{
			bFade = true;
		}
	}
}