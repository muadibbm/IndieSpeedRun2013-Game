using UnityEngine;
using System.Collections;

public class Text_Trigger : MonoBehaviour {
	
	private bool bTouched = false;
	public GameObject refObject;
	
	void OnTriggerEnter(Collider other) 
	{
        if(other.Equals(GameObject.Find("Player").collider))
		{
			bTouched = true;
			refObject.GetComponent<Text>().fade();
		}
    }
}