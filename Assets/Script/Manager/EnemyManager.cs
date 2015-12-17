using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    private static EnemyManager instance = null;

    public static EnemyManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
