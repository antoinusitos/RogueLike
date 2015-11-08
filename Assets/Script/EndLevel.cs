using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			parent.GetComponent<Labyrinthe>().Generate();
		}
	}
}
