using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {

    private static UpgradeManager instance = null;

    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    public int addLevelMax;

    public List<Upgrade> upgrades;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        addLevelMax = 3;
       // CreateUpgrade();
    }

    public void CreateUpgrade()
    {
        int level = GameManager.GetInstance().player.GetComponent<StatPlayer>().level;

        upgrades = new List<Upgrade>();

        for (int i = 0; i < 100; ++i)
        {
            upgrades.Add(CreateItem(level));
        }

        //int rand = Random.Range(0, 6);

        /*int r = Random.Range(10, 40);
        Upgrade u1 = new Upgrade(Upgrade.type.damage, 1, 1, r, r);
        r = Random.Range(10, 40);
        Upgrade u2 = new Upgrade(Upgrade.type.life, 1, 1, r, r);
        r = Random.Range(10, 40);
        Upgrade u3 = new Upgrade(Upgrade.type.reload, 1, 1, r, r);
        r = Random.Range(10, 40);
        Upgrade u5 = new Upgrade(Upgrade.type.ammo, 1, 1, r, r);
        r = Random.Range(10, 40);
        Upgrade u6 = new Upgrade(Upgrade.type.stamina, 1, 1, r, r);
        r = Random.Range(10, 40);
        Upgrade u7 = new Upgrade(Upgrade.type.cadence, 1, 1, r, r);
        r = Random.Range(10, 40);

        upgrades.Add(u1);
        upgrades.Add(u2);
        upgrades.Add(u3);
        upgrades.Add(u5);
        upgrades.Add(u6);
        upgrades.Add(u7);

        r = Random.Range(40, 80);
        Upgrade u8 = new Upgrade(Upgrade.type.damage, r/10, 2, r, r);
        r = Random.Range(40, 80);
        Upgrade u9 = new Upgrade(Upgrade.type.life, r / 10, 2, r, r);
        r = Random.Range(40, 80);
        Upgrade u10 = new Upgrade(Upgrade.type.reload, r / 10, 2, r, r);
        r = Random.Range(40, 80);
        Upgrade u11 = new Upgrade(Upgrade.type.ammo, r / 10, 2, r, r);
        r = Random.Range(40, 80);
        Upgrade u12 = new Upgrade(Upgrade.type.stamina, r / 10, 2, r, r);
        r = Random.Range(40, 80);
        Upgrade u13 = new Upgrade(Upgrade.type.cadence, r / 10, 2, r, r);
        r = Random.Range(40, 80);

        upgrades.Add(u8);
        upgrades.Add(u9);
        upgrades.Add(u10);
        upgrades.Add(u11);
        upgrades.Add(u12);
        upgrades.Add(u13);*/
    }

    Upgrade CreateItem(int level)
    {
        int levelMax = level + addLevelMax;
        int rand = Random.Range(0, 6);
        Upgrade upgrade;
        int r = Random.Range(level * 10, levelMax * 10);
        int lvl = r / 10;
        if (rand == 0)
        {
            upgrade = new Upgrade(Upgrade.type.damage, lvl, lvl, r, r);
        }
        else if (rand == 1)
        {
            upgrade = new Upgrade(Upgrade.type.life, lvl, lvl, r, r);
        }
        else if (rand == 2)
        {
            upgrade = new Upgrade(Upgrade.type.reload, lvl, lvl, r, r);
        }
        else if (rand == 3)
        {
            upgrade = new Upgrade(Upgrade.type.ammo, lvl, lvl, r, r);
        }
        else if (rand == 4)
        {
            upgrade = new Upgrade(Upgrade.type.stamina, lvl, lvl, r, r);
        }
        else
        {
            upgrade = new Upgrade(Upgrade.type.cadence, lvl, lvl, r, r);
        }
        return upgrade;
    }

    public Upgrade GetItem()
    {
        Upgrade retour = null;
        int level = GameManager.GetInstance().player.GetComponent<StatPlayer>().level;
        int levelMax = level + addLevelMax;
        int rand = -1;
        int tries = 0;
        int triesMax = 20;

        while (rand == -1 && tries <= triesMax)
        {
            rand = Random.Range(0, 100);
            if (upgrades[rand].GetNivMinPlayer() <= level)
            {
                rand = -1;
                tries++;
            }
            else
                retour = upgrades[rand];
        }
        if(retour == null)
        {
            return CreateItem(GameManager.GetInstance().player.GetComponent<StatPlayer>().level);
        }
        return retour;
    }
}
