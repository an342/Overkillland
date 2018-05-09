using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChar : MonoBehaviour {

    [SerializeField]
    GameObject Cube;
    [SerializeField]
    Vector3 offest;
	// Use this for initialization

	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = Cube.transform.position + offest;
	}
}
