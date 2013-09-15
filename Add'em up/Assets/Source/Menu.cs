using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
			renderer.enabled = false;
	}
}
