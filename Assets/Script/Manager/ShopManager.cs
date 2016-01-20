using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {

    private static ShopManager instance = null;

    public static ShopManager GetInstance()
    {
        return instance;
    }

    public List<GameObject> shops;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        shops = new List<GameObject>();
    }

    public void InitAllShop()
    {
        for(int i = 0; i < shops.Count; ++i)
        {
            shops[i].transform.GetChild(0).GetComponent<Shop>().InitShop();
        }
    }

    public void AddShop(GameObject s)
    {
        shops.Add(s);
    }
}