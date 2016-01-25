using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public GameObject bullet;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float rangeDetection;

    public float attackRange;
    public float attackSpeed;
    public float shootRange;
    public float delayBetweenSpray;
    public float delayBetweenBullet;
    public int nbOfBullets;

    bool activateShoot;
    bool canShoot;
    bool isShooting;

	// Use this for initialization
	void Start () {
        maxHealth = 50;
        currentHealth = maxHealth;
        moveSpeed = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        isShooting = false;
        activateShoot = false;
        canShoot = true;
        rangeDetection = 7f;
        shootRange = 3f;
        delayBetweenSpray = 2f;
        delayBetweenBullet = 0.3f;
        nbOfBullets = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
            float step = moveSpeed * Time.deltaTime;
            if (Vector3.Distance(player.transform.position, transform.position) <= rangeDetection && !activateShoot)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.LookAt(player.transform.position);
            }
            if (Vector3.Distance(player.transform.position, transform.position) <= shootRange)
            {
            transform.LookAt(player.transform.position);
            transform.position = transform.position;
                activateShoot = true;
                if (canShoot)
                {
                    StartCoroutine(Shoot());
                }
            }else
            {
                activateShoot = false;
            }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        for(int i =0; i< nbOfBullets; i++)
        {
            GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().dir = transform.forward;
            yield return new WaitForSeconds(delayBetweenBullet);
        }
        yield return new WaitForSeconds(delayBetweenSpray);
        canShoot = true;
    }
}
