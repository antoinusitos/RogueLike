using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Gun : MonoBehaviour {

    public int currentAmmo;
    public int maxAmmo = 61;
    public int currentAmmoLoaded;
    public int maxAmmoLoaded = 30;
    public bool canShoot;
    public bool canReload;

    public float reloadTime = 1.0f;
    public float fireRate = 0.1f;

    public GameObject bullet;
    public GameObject spawnBullet;
    public GameObject spawnDouille;

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

        if (Input.GetMouseButton(0) && currentAmmoLoaded == 0 && !canShoot && canReload && currentAmmo > 0)
        {
            StopCoroutine(Fire());
            StartCoroutine(Reload(currentAmmoLoaded));
        }

            Debug.Log(canShoot);
    }

    IEnumerator Reload(int currentAmmoInGun)
    {
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(reloadTime);
        Debug.Log(currentAmmo);
        if((maxAmmoLoaded - currentAmmoInGun) > currentAmmo)
        {
            currentAmmoLoaded = currentAmmoInGun + currentAmmo;
            currentAmmo = 0;
        }
        else
        {
            currentAmmo = currentAmmo - (maxAmmoLoaded - currentAmmoInGun);
            currentAmmoLoaded = maxAmmoLoaded;
        }
        
        
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
        canShoot = true;
        canReload = true;
    }

    IEnumerator Fire()
    {
        canShoot = false;
        currentAmmoLoaded--;
        //Vector3 rotationVector = bullet.transform.rotation.eulerAngles;
        //rotationVector.x = 90;
        GameObject b = (GameObject)Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().dir = transform.forward;
        b.transform.LookAt(transform.forward);
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
        Debug.Log(fireRate);
        yield return new WaitForSeconds(fireRate);
        if (currentAmmoLoaded <= 0)
        {
            canShoot = false;
        }else if(currentAmmoLoaded > 0)
        {
            canShoot = true;
        }
        


    }
}
