using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int life;

    public enum State
    {
        alive,
        dead,
        menu
    }

    State currentState = State.alive;

	// Use this for initialization
	void Start () 
	{
        life = 90;
        currentState = State.alive;
    }

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {
                if (hit.transform.gameObject.GetComponent<Shop>())
                {
                    if (currentState == State.menu)
                    {
                        currentState = State.alive;

                        hit.transform.gameObject.GetComponent<Shop>().HideUI();
                    }
                    else
                    {
                        currentState = State.menu;

                        hit.transform.gameObject.GetComponent<Shop>().ShowUI(gameObject);
                    }
                }
            }               
        }

        if (currentState == State.menu || currentState == State.dead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
	
    public State GetState()
    {
        return currentState;
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
        }
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life < 0)
        {
            life = 0;
        }
    }
}
