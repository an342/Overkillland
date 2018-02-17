using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward + new Vector3(0, 45, 0));

        if (Physics.Raycast(transform.position, fwd, 10))
            Debug.Log("i hit something");
    }
}
