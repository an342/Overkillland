using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MonoBehaviour 
{

	// Use this for initialization
    [SerializeField] GameObject Player;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float RoF;
    [SerializeField] string[] Weapons;
    [SerializeField] string equipped;
    private float timer;
    private bool semihold;

	void Start () 
    {
        mask = LayerMask.GetMask("Wall");
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
     
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("R2"))
        {
            switch (equipped)
            {
                case "Pistol":
                    FirePistol();
                    break;
                case "MG":
                    FireMG();
                    break;
                case "Chain":
                    FireChaingun();
                    break;
                default:
                    Debug.Log("No weapon equipped!");
                    break;
            }
        }
        if (Input.GetButtonUp("R2"))
        {
            Debug.Log("button up");
            semihold = false;
        }
    }

    void FireMG()
    {
        Debug.Log("fire MG");
        RoF = 0.2f;
        if (timer > RoF)
        {
            FireBullet();
            timer = 0;
        }

   }
    void FirePistol()
    {
        Debug.Log("fire pistol");
        Debug.Log("semihold: " + semihold);
        if (timer > RoF && !semihold)
        {
            FireBullet();
            timer = 0;
            semihold = true;
        }
    }
    void FireBullet()
    {
        Debug.Log("fire bullet");
        RaycastHit hit;
        Vector3 fwd = Player.transform.forward;
        if (Physics.Raycast(transform.position, -fwd, out hit, 25, mask.value) )
        {
            Debug.Log("i hit: " + hit.transform.name + " : " + hit.point);
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
    }
    void FireChaingun()
    {
        Debug.Log("fire chain");
        RoF = 0.02f;
        if (timer > RoF)
        {
            FireBullet();
            timer = 0;
        }
    }
    void ChangeWeapon()
    {

    }
 }


