using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3 prevMousePos;

	// Use this for initialization
	void Start () 
	{
		prevMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Z))
		{
			transform.Translate(Vector3.forward* Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(-Vector3.forward* Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.Q))
		{
			transform.Translate(-Vector3.right* Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right* Time.deltaTime);
		}
		if((Input.mousePosition.x - prevMousePos.x) > 3 || (Input.mousePosition.x - prevMousePos.x) < -3)
			transform.Rotate (Vector3.up, (Input.mousePosition.x - prevMousePos.x) * Time.deltaTime * .5f);
	}
}
