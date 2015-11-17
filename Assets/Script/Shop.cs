using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	GameObject shopGUI;
	public GameObject shopGUIPrefab;
	bool opened = false;
    Player player = null;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(opened)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
                player.AddLife(10);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				Debug.Log("amelioration bought !");
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			shopGUI = Instantiate(shopGUIPrefab);
            player = other.transform.GetComponent<Player>();
			opened = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			Destroy(shopGUI.gameObject);
            player = null;
			opened = false;
		}
	}
}
