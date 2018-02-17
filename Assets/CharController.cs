using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	// Use this for initialization
    [SerializeField]
    float movespeed = 4f;
    Vector3 forward, right;
	void Start () 
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Move();    
        Look();
            
	}

    //point player front direction with right analog stick
    void Look()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("H2"), 0, Input.GetAxisRaw("V2"));
        Vector3 rightMovement = right * Input.GetAxisRaw("H2");
        Vector3 upMovement = forward * Input.GetAxisRaw("V2");
        Debug.Log("H2: "+Input.GetAxis("H2") + " " + Input.GetAxisRaw("H2"));
        Debug.Log("V2: "+Input.GetAxis("V2") + " " + Input.GetAxisRaw("V2"));
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        
        transform.forward = heading;
    }

    //move player with left analog stick
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxis("Vertical");

        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}

