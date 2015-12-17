using UnityEngine;
using System.Collections;

public class FoliageManager : MonoBehaviour {

    private static FoliageManager instance = null;

    public static FoliageManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
