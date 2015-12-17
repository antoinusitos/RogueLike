using UnityEngine;
using System.Collections;

public class UpgradeManager : MonoBehaviour {

    private static UpgradeManager instance = null;

    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
