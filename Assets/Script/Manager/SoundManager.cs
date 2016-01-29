using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != null)
        {
            Destroy(this);
        }
    }



    public AudioSource buttonPressed;
    public AudioSource playerGun;
    public AudioSource reload;
    public AudioSource eavyBreathing;
    public AudioSource ennemiMachinegun;
    public AudioSource enterShop;
    public AudioSource buyShop;
    public AudioSource explosion;
    public AudioSource crashAlarm;

   

}
