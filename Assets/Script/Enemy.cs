﻿using UnityEngine;
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

    public float imprecision;

    public GameObject pop;

    public int degats;

	// Use this for initialization
	void Start ()
    {
        maxHealth = 50;
        currentHealth = maxHealth;
        moveSpeed = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        isShooting = false;
        activateShoot = false;
        canShoot = true;
        rangeDetection = 7f;
        shootRange = 3f;
        delayBetweenSpray = .1f;
        delayBetweenBullet = 0.1f;
        nbOfBullets = 3;
        degats = 1;

        imprecision = .5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(currentHealth <= 0)
        {
            int r = Random.Range(10, 20);
            player.GetComponent<StatPlayer>().AddMoney(r);
            float r2 = Random.Range(10.0f, 30.0f);
            player.GetComponent<StatPlayer>().AddXP(r2);
            Instantiate(pop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        float step = moveSpeed * Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) <= rangeDetection && !activateShoot)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            //transform.LookAt(player.transform.position);
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= shootRange)
        {
           // transform.LookAt(player.transform.position);
            transform.position = transform.position;
            activateShoot = true;
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            activateShoot = false;
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        for(int i =0; i< nbOfBullets; i++)
        {
          //  GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
           // b.GetComponent<Bullet>().dir = (player.transform.position - transform.position).normalized ;
            RaycastHit hit;
            float range = 5.0f;
            Vector3 v = new Vector3(Random.Range(-imprecision, imprecision), Random.Range(-imprecision, imprecision), Random.Range(-imprecision, imprecision));
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized + v, out hit, range) && hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Player>().TakeDamage(degats);
            }
            Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized + v, Color.red, 5);

            yield return new WaitForSeconds(delayBetweenBullet);
        }
        yield return new WaitForSeconds(delayBetweenSpray);
        canShoot = true;
    }
}
