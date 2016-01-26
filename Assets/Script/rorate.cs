using UnityEngine;
using System.Collections;

public class rorate : MonoBehaviour {

	void Update()
    {
        transform.Rotate(Vector3.right, 1 * Time.deltaTime);
    }
}
