using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // TODO : enlever le debug
    public bool Debug = true;


    public GameObject gun;
    public GameObject cameraPlayer;

    public int life;
    public int maxLife;

    public bool showingStats;

    public AudioSource heavyBreathing;
    public AudioSource enterShop;


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
        showingStats = false;
        maxLife = 100;
        life = maxLife;
        currentState = State.alive;
        RefreshUI();

        heavyBreathing = SoundManager.instance.eavyBreathing.GetComponent<AudioSource>();
        enterShop = SoundManager.instance.enterShop.GetComponent<AudioSource>();
    }

    public bool NeedHeal()
    {
        return life == maxLife ? false : true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Action") && !showingStats)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {
                if (hit.transform.gameObject.GetComponent<Shop>())
                {
                    if (currentState == State.menu)
                    {
                        currentState = State.alive;
                        cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(false);
                        gun.GetComponent<Gun>().SetCanShoot(true);
                        hit.transform.gameObject.GetComponent<Shop>().HideUI();

                    }
                    else
                    {
                        currentState = State.menu;
                        cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(true);
                        gun.GetComponent<Gun>().SetCanShoot(false);
                        hit.transform.gameObject.GetComponent<Shop>().ShowUI(gameObject);
                        enterShop.Play();
                    }
                }
            }               
        }

        if (Input.GetButtonDown("Stats") && currentState != State.menu)
        {
            if(showingStats)
            {
                showingStats = false;
                gun.GetComponent<Gun>().SetCanShoot(true);
                // currentState = State.alive;
                UIManager.GetInstance().ShowStats(false);
                cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(false);
            }
            else
            {
                gun.GetComponent<Gun>().SetCanShoot(false);
                showingStats = true;
                //currentState = State.menu;
                UIManager.GetInstance().ShowStats(true);
                cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(true);
                PanelStats.GetInstance().UpdateStats(gameObject);
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

        if (Debug)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (currentState == State.menu)
                {
                    currentState = State.alive;
                    cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(false);
                }
                else
                {
                    currentState = State.menu;
                    cameraPlayer.GetComponent<SimpleSmoothMouseLook>().SetLockView(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                TakeDamage(20);
            }
        }

        if (life <= (maxLife * 0.1))
        {
            heavyBreathing.Play();
        }

        if (life > (maxLife * 0.1))
        {
            heavyBreathing.Stop();        
        }
    }
	
    public State GetState()
    {
        return currentState;
    }

    public void SetState(State theState)
    {
        currentState = theState;
    }

    public void AddLife(int adding)
    {
        if (life < maxLife)
        {
            life += adding;
            if (life > maxLife)
            {
                life = maxLife;
            }
        }
        RefreshUI();
    }

    public void SetLife(int amount)
    {
        life = amount;
    }

    public void SetMaxLife(int amount)
    {
        maxLife = amount;
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life < 0)
        {
            life = 0;
        }
        RefreshUI();
    }

    public void RefreshUI()
    {
        UIManager.GetInstance().SetUIText(UIManager.GetInstance().lifeText, life + " / " + maxLife);
        UIManager.GetInstance().SetUIBar(UIManager.GetInstance().lifeBar, (float)life, (float)  maxLife);
    }

    public void AddMaxLife(int value)
    {
        maxLife += value;
        RefreshUI();
    }
}
