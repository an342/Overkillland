using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour {

    public float fuse;
    public float damage;
    float timer;
    [SerializeField]
    GameObject Grenade;
    [SerializeField]
    GameObject detEffect;
    bool detonated;



	// Use this for initialization
	void Start () 
    {   
        StartCoroutine(Fuse());
        detonated = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter (Collider col)
    {

        Debug.Log("Hit somthing");
        if (col.gameObject.name == "CubeP" && !detonated)
        {
            
            Debug.Log("Hit cube");
            detonated = true;
            StartCoroutine(Detonate());
            
        }
    }

    IEnumerator Fuse()
    {
        float timer = 0;

        while( timer < fuse)
        {

            timer += 0.25f;

            yield return new WaitForSeconds(0.25f);
        }

        detonated = true;
        StartCoroutine(Detonate());

    }
    IEnumerator Detonate()
    {
        float timer = 0;
        Debug.Log("BOOM!");
        Instantiate(detEffect, transform.position, Quaternion.identity);
        Destroy(Grenade);
        while (timer < 2f)
        {
            timer += .25f;
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(detEffect);
    }
}
