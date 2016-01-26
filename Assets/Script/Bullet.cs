using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 0.01f;
    public Vector3 dir;
    public float delayToDestroy;
	
    void Start()
    {
        delayToDestroy = 5.0f;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(dir * speed);
        Destroy(gameObject, delayToDestroy);
        
    }

}
