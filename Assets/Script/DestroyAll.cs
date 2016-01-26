using UnityEngine;
using System.Collections;

public class DestroyAll : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider);
    }
}
