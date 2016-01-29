using UnityEngine;
using System.Collections;

public class ammo : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            int r = Random.Range(5, 10);
            other.transform.GetComponent<Player>().gun.GetComponent<Gun>().AddAmmo(r);
            Destroy(gameObject);
        }
    }
}
