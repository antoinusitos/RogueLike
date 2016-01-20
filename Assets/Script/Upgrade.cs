using UnityEngine;
using System.Collections;

public class Upgrade {

    public enum type
    {
        damage,
        cadence,
        reload,
        aim,
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
}
