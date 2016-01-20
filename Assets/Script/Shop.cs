using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

	GameObject shopGUI;
	public GameObject shopGUIPrefab;
	bool opened = false;
    GameObject player = null;

    Upgrade[] content;
	
	// Update is called once per frame
	void Update () 
	{
		if(opened)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
                player.GetComponent<Player>().AddLife(10);
                player.GetComponent<StatPlayer>().RemoveMoney(50);
            }
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				Debug.Log("amelioration bought !");
			}
		}
	}

    public void ShowUI(GameObject p)
    {
        shopGUI = Instantiate(shopGUIPrefab);
        player = p;
        opened = true;
    }

    public void InitShop()
    {
        
        content = new Upgrade[5];
        List<Upgrade> upgrades = UpgradeManager.GetInstance().upgrades;

        if (upgrades.Count >= 7)
        {

            int[] rands = new int[5];

            for (int i = 0; i < 5; ++i)
            {
                int rand = -1;
                while (rand == -1)
                {
                    rand = Random.Range(0, upgrades.Count);
                    for (int j = 0; j < rands.Length; ++j)
                    {
                        if (rand == rands[j])
                        {
                            rand = -1;
                            break;
                        }
                    }
                }
                rands[i] = rand;

                content[i] = upgrades[rand];
                Debug.Log("it:" + i + content[i].toString());
            }

        }
        else
        {
            Debug.Log("ERREUR : PAS ASSEZ D'UPGRADE POUR INITIALISER LES SHOP");
        }
    }

    public void HideUI()
    {
        Destroy(shopGUI.gameObject);
        player = null;
        opened = false;
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			
		}
	}
}
