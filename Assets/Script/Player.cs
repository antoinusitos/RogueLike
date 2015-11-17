using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int life;
    public int money;


	// Use this for initialization
	void Start () 
	{
        life = 90;
        money = 100;
	}
	
    public void AddLife(int adding)
    {
        if (life < 100)
        {
            life += adding;
            if (life > 100)
            {
                life = 100;
            }
            money -= 50;
        }
    }
}
