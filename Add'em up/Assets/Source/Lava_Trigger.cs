using UnityEngine;
using System.Collections;

public class Lava_Trigger : MonoBehaviour 
{
	//private bool bTouched = false;
	public GameObject refObject;
	
	void OnTriggerEnter(Collider other) 
	{
        if(other.Equals(GameObject.Find("Player").collider))
		{
			//bTouched = true;
			refObject.GetComponent<Lava>().kill(true);
		}
    }
	
	void OnTriggerExit(Collider other) 
	{
        if(other.Equals(GameObject.Find("Player").collider))
		{
			//bTouched = false;
			refObject.GetComponent<Lava>().kill(false);
		}
    }
}
