using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {

    private static UpgradeManager instance = null;

    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    public List<Upgrade> upgrades;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        CreateUpgrade();
    }

    void CreateUpgrade()
    {
        upgrades = new List<Upgrade>();

        Upgrade u1 = new Upgrade(Upgrade.type.damage, 1, 1, 40, 10);
        Upgrade u2 = new Upgrade(Upgrade.type.life, 1, 2, 40, 10);
        Upgrade u3 = new Upgrade(Upgrade.type.reload, 1, 1, 40, 10);
        Upgrade u4 = new Upgrade(Upgrade.type.aim, 1, 1, 40, 10);
        Upgrade u5 = new Upgrade(Upgrade.type.ammo, 1, 1, 40, 10);
        Upgrade u6 = new Upgrade(Upgrade.type.stamina, 1, 1, 40, 10);
        Upgrade u7 = new Upgrade(Upgrade.type.cadence, 1, 1, 40, 10);

        upgrades.Add(u1);
        upgrades.Add(u2);
        upgrades.Add(u3);
        upgrades.Add(u4);
        upgrades.Add(u5);
        upgrades.Add(u6);
        upgrades.Add(u7);
    }
}
