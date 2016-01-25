﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 0.01f;
    public Vector3 dir;
    public float delayToDestroy;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(dir * speed);
        Destroy(gameObject, delayToDestroy);
        
    }


}
