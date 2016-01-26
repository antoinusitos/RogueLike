using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

	GameObject shopGUI;
	public GameObject shopGUIPrefab;
	bool opened = false;
    GameObject player = null;
    public int refillLifeCost;
    public int refillLifeAmount;

    Upgrade[] content;
	
    void Start()
    {
        refillLifeCost = 20;
        refillLifeAmount = 30;
    }

	// Update is called once per frame
	void Update () 
	{
		if(opened)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
                if (player.GetComponent<Player>().NeedHeal() && player.GetComponent<StatPlayer>().GetMoney() >= refillLifeCost)
                {
                    player.GetComponent<Player>().AddLife(refillLifeAmount);
                    player.GetComponent<StatPlayer>().RemoveMoney(refillLifeCost);
                }
            }
			if(Input.GetKeyDown(KeyCode.Alpha2) && player.GetComponent<StatPlayer>().GetMoney() >= content[0].GetCost() && player.GetComponent<StatPlayer>().level >= content[0].GetNivMinPlayer())
			{
                Debug.Log(content[0].toString());
                content[0].Apply(player);
                ChangeItem(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && player.GetComponent<StatPlayer>().GetMoney() >= content[1].GetCost() && player.GetComponent<StatPlayer>().level >= content[1].GetNivMinPlayer())
            {
                Debug.Log(content[1].toString());
                content[1].Apply(player);
                ChangeItem(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && player.GetComponent<StatPlayer>().GetMoney() >= content[2].GetCost() && player.GetComponent<StatPlayer>().level >= content[2].GetNivMinPlayer())
            {
                Debug.Log(content[2].toString());
                content[2].Apply(player);
                ChangeItem(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && player.GetComponent<StatPlayer>().GetMoney() >= content[3].GetCost() && player.GetComponent<StatPlayer>().level >= content[3].GetNivMinPlayer())
            {
                Debug.Log(content[3].toString());
                content[3].Apply(player);
                ChangeItem(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && player.GetComponent<StatPlayer>().GetMoney() >= content[4].GetCost() && player.GetComponent<StatPlayer>().level >= content[4].GetNivMinPlayer())
            {
                Debug.Log(content[4].toString());
                content[4].Apply(player);
                ChangeItem(4);
            }
        }
	}

    void ChangeItem(int place)
    {
        content[place] = UpgradeManager.GetInstance().GetItem();
        RefreshUI();

    }

    public void ShowUI(GameObject p)
    {
        shopGUI = Instantiate(shopGUIPrefab);
        RefreshUI();
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
                //Debug.Log("it:" + i + content[i].toString());
            }

        }
        else
        {
            Debug.Log("ERREUR : PAS ASSEZ D'UPGRADE POUR INITIALISER LES SHOP");
        }
    }

    void RefreshUI()
    {
        ShopCanvas s = shopGUI.transform.GetChild(0).transform.GetComponent<ShopCanvas>();
        s.text1.GetComponent<Text>().text = "1 - Refill Life ( cost:"+refillLifeAmount+")";
        s.text2.GetComponent<Text>().text = "2 - Upgrade " + content[0].GetTypeUpgrade() + " (cost:" + content[0].GetCost() + ", lvl :" + content[0].GetNivMinPlayer() + ")";
        s.text3.GetComponent<Text>().text = "3 - Upgrade " + content[1].GetTypeUpgrade() + " (cost:" + content[1].GetCost() + ", lvl :" + content[1].GetNivMinPlayer() + ")";
        s.text4.GetComponent<Text>().text = "4 - Upgrade " + content[2].GetTypeUpgrade() + " (cost:" + content[2].GetCost() + ", lvl :" + content[2].GetNivMinPlayer() + ")";
        s.text5.GetComponent<Text>().text = "5 - Upgrade " + content[3].GetTypeUpgrade() + " (cost:" + content[3].GetCost() + ", lvl :" + content[3].GetNivMinPlayer() + ")";
        s.text6.GetComponent<Text>().text = "6 - Upgrade " + content[4].GetTypeUpgrade() + " (cost:" + content[4].GetCost() + ", lvl :" + content[4].GetNivMinPlayer() + ")";
    }

    public void HideUI()
    {
        Destroy(shopGUI.gameObject);
        player = null;
        opened = false;
    }
}
