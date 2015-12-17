using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager instance = null;

    public static PlayerManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
