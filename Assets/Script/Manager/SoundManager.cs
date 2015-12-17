using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance = null;
    
    public static SoundManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
