using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public GameObject bullet;
    public GameObject explosionFX;
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

    public int rotationSpeed;
    bool activateShoot;
    bool canShoot;
    bool isShooting;
    bool isDying;

	// Use this for initialization
	void Start () {
        isDying = false;
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

        rotationSpeed = 5;
    }
	
	// Update is called once per frame
	void Update () {
        if(currentHealth <= 0 && !isDying)
        {
            isDying = true;
            
            transform.GetChild(0).transform.GetComponent<Animator>().SetTrigger("DeathTrigger");
            explosionFX.GetComponent<ParticleSystem>().Play();

            // Destroy(gameObject, 2);
        }
        if(isDying)
            transform.position -= new Vector3(0f, 0.5f, 0f) * Time.deltaTime;
        float step = moveSpeed * Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) <= rangeDetection && !activateShoot)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Avance", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= shootRange)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Avance", false);
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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
            GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().dir = transform.forward;
            yield return new WaitForSeconds(delayBetweenBullet);
        }
        yield return new WaitForSeconds(delayBetweenSpray);
        canShoot = true;
    }

    public void Explosion()
    {
        explosionFX.GetComponent<ParticleSystem>().Play();
    }
}
