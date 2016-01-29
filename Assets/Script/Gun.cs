using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Gun : MonoBehaviour {
    RaycastHit hit;
    public int currentAmmo;
    public int maxAmmo = 61;
    public int currentAmmoLoaded;
    public int maxAmmoLoaded = 30;
    public bool canShoot;
    public bool canReload;
    public bool reloading;

    public float reloadTime;
    public float reloadingTime;
    public float fireRate;
    public float range;
    public float damage;

    public GameObject bullet;
    public GameObject spawnBullet;
    public GameObject spawnDouille;
    public GameObject bulletTrail;

    Text bulletIndicator;
	public GameObject bulletTex;

    public AudioSource reloadGun;
    public AudioSource fireGun;


	// Use this for initialization
	void Start () {

        damage = 10;
        range = 5;
        fireRate = .5f;
        reloadTime = 2;
        reloadingTime = 0;
        maxAmmoLoaded = 20;
        maxAmmo = 240;

        canShoot = false;
        canReload = false;
        reloading = false;

        reloadGun = SoundManager.instance.reload.GetComponent<AudioSource>();
        fireGun = SoundManager.instance.playerGun.GetComponent<AudioSource>();


        currentAmmoLoaded = maxAmmoLoaded;
        currentAmmo = maxAmmo;

	    if(currentAmmoLoaded > 0)
        {
            canShoot = true;
            canReload = true;
        }
        UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
        //Reload
        if (Input.GetKeyDown(KeyCode.R) && canReload && (currentAmmoLoaded < maxAmmoLoaded) && currentAmmo > 0)
        {
            reloadGun.Play();
            StartCoroutine(Reload(currentAmmoLoaded));
        }
        //Shoot
        if(Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Fire());
        }

        if (Input.GetMouseButton(0) && currentAmmoLoaded == 0 && !canShoot && canReload && currentAmmo > 0)
        {
            reloadGun.Play();

            StopCoroutine(Fire());
            StartCoroutine(Reload(currentAmmoLoaded));
        }

        if(reloading)
        {
            reloadingTime += Time.deltaTime;
            UIManager.GetInstance().SetUIBar(UIManager.GetInstance().reloadBar, reloadingTime, reloadTime);
        }

    }

    void UpdateUI()
    {
        bulletIndicator = GameObject.FindGameObjectWithTag("BulletIndicator").GetComponent<Text>();
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
    }

    public void SetCanShoot(bool state)
    {
        canShoot = state;
    }

    IEnumerator Reload(int currentAmmoInGun)
    {
        UIManager.GetInstance().reloadBar.SetActive(true);
        reloadingTime = 0;
        reloading = true;
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(reloadTime);
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
        reloading = false;
        UIManager.GetInstance().reloadBar.SetActive(false);
    }

    IEnumerator Fire()
    {
        canShoot = false;
        currentAmmoLoaded--;
        fireGun.Play();
        //b.transform.LookAt(Camera.main.transform);
        bulletIndicator.text = currentAmmoLoaded.ToString() + "/" + currentAmmo.ToString();
        
        GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        if (Physics.Raycast(spawnBullet.transform.position, Camera.main.transform.forward, out hit, range))
        { 
          if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().currentHealth -= damage;
                //b.GetComponent<Bullet>().dir = (hit.point - transform.position).normalized;
            }
            else if (hit.collider.tag == "Wall" || hit.collider.tag == "floor")
            {
                Quaternion startRot = Quaternion.LookRotation(hit.normal);
                Instantiate(bulletTex, hit.point, startRot);
                //b.GetComponent<Bullet>().dir = (hit.point - transform.position).normalized;
            }
            else
            {
                //b.GetComponent<Bullet>().dir = ((Camera.main.transform.position + Camera.main.transform.forward * range) - transform.position).normalized;
            }
        }
        else
        {
            //b.GetComponent<Bullet>().dir = (Camera.main.transform.forward - transform.position).normalized;
            
        }

        yield return new WaitForSeconds(fireRate);
        if (currentAmmoLoaded <= 0)
        {
            canShoot = false;
        }else if(currentAmmoLoaded > 0)
        {
            canShoot = true;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateUI();
    }
}
