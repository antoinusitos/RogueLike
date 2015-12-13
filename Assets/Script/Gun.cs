using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Gun : MonoBehaviour {

    public int currentAmmo;
    public int maxAmmo = 240;
    public int currentAmmoLoaded;
    public int maxAmmoLoaded = 30;
    public bool canShoot;
    public bool canReload;

    public float reloadTime = 1.0f;
    public float fireRate = 0.1f;

    Text bulletIndicator;
    


	// Use this for initialization
	void Start () {
        currentAmmoLoaded = maxAmmoLoaded;
        currentAmmo = maxAmmo;
	    if(currentAmmoLoaded > 0)
        {
            canShoot = true;
            canReload = true;
        }
        bulletIndicator = GameObject.FindGameObjectWithTag("BulletIndicator").GetComponent<Text>();
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/"+currentAmmo.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        //Reload
        if (Input.GetKeyDown(KeyCode.R) && canReload && (currentAmmoLoaded < maxAmmoLoaded) && currentAmmo > 0)
        {
            StartCoroutine(Reload(currentAmmoLoaded));
        }
        //Shoot
        if(Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Fire());
        }
        Debug.Log(currentAmmoLoaded);
    }

    IEnumerator Reload(int currentAmmoInGun)
    {
        canShoot = false;
        canReload = false;
        currentAmmo = currentAmmo - (maxAmmoLoaded - currentAmmoInGun);
        yield return new WaitForSeconds(reloadTime);
        currentAmmoLoaded = maxAmmoLoaded;
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
        canShoot = true;
        canReload = true;
    }

    IEnumerator Fire()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.1f);
        currentAmmoLoaded--;
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
        Debug.Log("Je tir");
        if (currentAmmoLoaded <= 0)
        {
            canShoot = false;
        }else
        {
            canShoot = true;
        }
       
    }
}
