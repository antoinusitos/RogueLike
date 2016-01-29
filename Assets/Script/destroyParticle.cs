using UnityEngine;
using System.Collections;

public class destroyParticle : MonoBehaviour {

	void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}
