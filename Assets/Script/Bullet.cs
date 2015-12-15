using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 32000.0f;
    public Vector3 dir;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += dir * speed ;
    }


}
