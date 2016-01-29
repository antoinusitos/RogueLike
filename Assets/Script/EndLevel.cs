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
            GameObject go = GameObject.Find("AllEnemies");
            GameObject go2 = GameObject.Find("AllPlants");
            go.SetActive(false);
            go2.SetActive(false);

            new GameObject("AllEnemies");
            new GameObject("AllPlants");

            Destroy(go);
            Destroy(go2);

            parent.GetComponent<Labyrinthe>().Generate();
            UpgradeManager.GetInstance().CreateUpgrade();
            ShopManager.GetInstance().InitAllShop();
        }
	}
}
