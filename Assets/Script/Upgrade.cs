using UnityEngine;
using System.Collections;

public class Upgrade {

    public enum type
    {
        damage,
        cadence,
        reload,
        ammo,
        stamina,
        life
    }

    type currentType;
    int nivMinPlayer;
    int nivUpgrade;
    int cost;
    int value;

    public Upgrade(type t, int theNivMinPlayer, int theNivUpgrade, int theCost, int theValue)
    {
        currentType = t;
        nivMinPlayer = theNivMinPlayer;
        nivUpgrade = theNivUpgrade;
        cost = theCost;
        value = theValue;
    }

    public string toString()
    {
        return "currentType:" + currentType + " nivMinPlayer:" + nivMinPlayer + " nivUpgrade:" +
            nivUpgrade + " cost:" + cost + " value:" + value;
    }

    public type GetTypeUpgrade()
    {
        return currentType;
    }

    public int GetNivMinPlayer()
    {
        return nivMinPlayer;
    }

    public int GetNivUpgrade()
    {
        return nivUpgrade;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetValue()
    {
        return value;
    }

    public void Apply(GameObject p)
    {
        p.GetComponent<StatPlayer>().RemoveMoney(cost);
        if (currentType == type.life)
        {
            p.GetComponent<StatPlayer>().AddStatLife(value);
        }
        else if (currentType == type.stamina)
        {
            p.GetComponent<StatPlayer>().AddStatStamina(value);
        }
        else if (currentType == type.damage)
        {
            p.GetComponent<StatPlayer>().AddStatDamage(value);
        }
        else if (currentType == type.cadence)
        {
            p.GetComponent<StatPlayer>().AddStatCadence();
        }
        else if (currentType == type.ammo)
        {
            p.GetComponent<StatPlayer>().AddStatAmmo();
        }
        else if (currentType == type.reload)
        {
            p.GetComponent<StatPlayer>().AddStatReload();
        }
    }
}
