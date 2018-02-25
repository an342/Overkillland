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
        //Look2();
            
	}

    //point player front direction with right analog stick
    void Look()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("RSH"), 0, Input.GetAxisRaw("RSV"));
        Vector3 rightMovement = right * Input.GetAxisRaw("RSH");
        Vector3 upMovement = forward * Input.GetAxisRaw("RSV");
        //Debug.Log("RSH2: " + Input.GetAxis("RSH2") + " " + Input.GetAxisRaw("RSH2"));
        //Debug.Log("RSV2: " + Input.GetAxis("RSV2") + " " + Input.GetAxisRaw("RSV2"));
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        if(direction != new Vector3 (0,0,0))
            transform.forward = heading;
    }
    void Look2()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("RSH"), 0, Input.GetAxisRaw("RSV"));
        Vector3 rightMovement = right * Input.GetAxisRaw("RSH");
        Vector3 upMovement = forward * Input.GetAxisRaw("RSV");
        Debug.Log("RSH: " + Input.GetAxis("RSH") + " " + Input.GetAxisRaw("RSH"));
        Debug.Log("RSV: " + Input.GetAxis("RSV") + " " + Input.GetAxisRaw("RSV"));
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        if (direction != new Vector3(0, 0, 0))
            transform.forward = heading;
    }

    //move player with left analog stick
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("LSH"), 0, Input.GetAxis("LSV"));
        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxis("LSH");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxis("LSV");

        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}

