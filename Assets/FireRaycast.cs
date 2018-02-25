using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MonoBehaviour {

	// Use this for initialization
    [SerializeField]
    GameObject Player;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    GameObject hitEffect;
    [SerializeField]
    float RoF;
    private float timer;


	void Start () {
        mask = LayerMask.GetMask("Wall");
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    void FixedUpdate()
    {

        RaycastHit hit;
        Vector3 fwd = Player.transform.forward;
        //Debug.Log(mask + " " + mask.value);
        timer = timer + Time.deltaTime;
       // Debug.Log(timer);
        //Debug.Log("Time: " + Time.time + " Delta Time: " + Time.deltaTime + " Sub: " + (Time.time - Time.deltaTime));
        if (timer >= RoF)
        {
            if (Physics.Raycast(transform.position, -fwd, out hit, 10, mask.value))
            {
                timer = 0;
                Debug.Log("i hit: " + hit.transform.name + " : " + hit.point);
                Instantiate(hitEffect, hit.point, Quaternion.identity);


            }
        }
    }
}
