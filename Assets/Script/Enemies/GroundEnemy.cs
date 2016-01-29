using UnityEngine;
using System.Collections;

public class GroundEnemy : MonoBehaviour {

    public GameObject player;
    public GameObject bullet;
    public GameObject explosionFX;
    public GameObject spawnBullet;
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
    //bool isShooting;
    bool isDying;

    public float imprecision;

    public GameObject pop;

    public int degats;

    public AudioSource machineGun;
    public AudioSource explosion;

    // Use this for initialization
    void Start()
    {
        isDying = false;
        maxHealth = 50;
        currentHealth = maxHealth;
        moveSpeed = 1f;
        // player = GameObject.FindGameObjectWithTag("Player");
        //isShooting = false;
        activateShoot = false;
        canShoot = true;
        rangeDetection = 10f;
        shootRange = 8f;
        delayBetweenSpray = .1f;
        delayBetweenBullet = 0.8f;
        nbOfBullets = 3;
        degats = 3;
        imprecision = .5f;
        rotationSpeed = 5;

        machineGun = SoundManager.instance.ennemiMachinegun.GetComponent<AudioSource>();
        explosion = SoundManager.instance.explosion.GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHealth <= 0 && !isDying)
        {
            isDying = true;

            int r = Random.Range(10, 20);
            player.GetComponent<StatPlayer>().AddMoney(r);
            float r2 = Random.Range(10.0f, 30.0f);
            player.GetComponent<StatPlayer>().AddXP(r2);
            transform.GetChild(0).transform.GetComponent<Animator>().SetTrigger("TriggerDeath");

            float r3 = Random.Range(0.0f, 1.0f);
            if(r3 > 0.33f)
            {
                Invoke("PopAmmo", 0.9f);
            }
            Invoke("Explosion", 1f);
           
            Destroy(gameObject, 1f);
           
        }
        if (isDying)
            transform.position -= new Vector3(0f, 0.5f, 0f) * Time.deltaTime;
        float step = moveSpeed * Time.deltaTime;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        RaycastHit hit;
        if (Vector3.Distance(player.transform.position, transform.position) <= rangeDetection && !activateShoot)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
           
            if (Physics.Raycast(transform.position+new Vector3(0, 0.5f, 0), transform.forward, out hit, rangeDetection))
            {
                if (hit.collider.tag == "Player")
                {
                    transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Avance", true);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step);
                }
                else
                {
                    transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Avance", false);
                }
            }
        }
        else
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Avance", false);
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= shootRange)
        {
           
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position = transform.position;
            activateShoot = true;
            if (canShoot)
            {
                transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Attack", true);
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
        for (int i = 0; i < nbOfBullets; i++)
        {
            RaycastHit hit;
            float range = 5.0f;
            Vector3 v = new Vector3(Random.Range(-imprecision, imprecision), Random.Range(-imprecision, imprecision), Random.Range(-imprecision, imprecision));
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized + v, out hit, range) && hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Player>().TakeDamage(degats);
            }
           
            spawnBullet.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(delayBetweenBullet);
            spawnBullet.GetComponent<ParticleSystem>().Stop();
            machineGun.Play();
        }
        yield return new WaitForSeconds(delayBetweenSpray);
        transform.GetChild(0).transform.GetComponent<Animator>().SetBool("Attack", false);

        canShoot = true;
    }

    public void Explosion()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        explosion.Play();
    }

    public void PopAmmo()
    {
        Instantiate(pop, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
    }

}
