using UnityEngine;
using System.Collections;

public class HoverButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         
         if (Physics.Raycast(ray))
         {
             Debug.Log(ray);
         }
               
    }
}

     

