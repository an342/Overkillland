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
    private bool semiHold;

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
        if (Input.GetButtonDown("R2"))
        {
            switch (equipped)
            {
                case "Pistol":
                    FirePistol();
                    break;
                case "MG":
                    StartCoroutine(FireMG());
                    break;
                case "Chain":
                    StartCoroutine(FireChaingun());
                    break;
                case "Burst":
                    StartCoroutine(FireBurst());
                    break;
                default:
                    Debug.Log("No weapon equipped!");
                    break;
            }
        }
        /*if (Input.GetButtonUp("R2"))
        {
            Debug.Log("button up");
            semiHold = false;
        }*/
    }

    IEnumerator FireMG()
    {
        Debug.Log("fire MG");
        RoF = 0.5f;
        //timer > RoF
        while(Input.GetButton("R2"))
        {
            Debug.Log("fireing MG");
            FireBullet();
            //timer = 0;
            yield return new WaitForSeconds(RoF);
        }

    }

    IEnumerator FireBurst()
    {
        Debug.Log("Fire Burst");
        RoF = 0.5f;
        int count = 0;
        //timer > RoF
        while (Input.GetButton("R2"))
        {
            while (count < 3)
            {
                Debug.Log("pow");
                FireBullet();
                count++;
                yield return new WaitForSeconds(0.1f);
            }
            count = 0;
            Debug.Log("wait FB");
            yield return new WaitForSeconds(RoF);
        }

    }


    IEnumerator SemiAuto( float RoF)
    {
        Debug.Log("SemiAuto");
        while(semiHold)
        {
            semiHold = false;
            yield return new WaitForSeconds(RoF);
        }
    }

    /*
    void FirePistol()
    {
        Debug.Log("fire pistol");
        Debug.Log("semihold: " + semiHold);
        if (timer > RoF && !semiHold)
        {
            FireBullet();
            timer = 0;
            semiHold = true;
        }
    }*/
    void FirePistol()
    {
        RoF = 0.5f;
        Debug.Log("fire pistol");
        if(!semiHold)
        {
            FireBullet();
            StartCoroutine(SemiAuto(RoF));
        }
    }
    void FireBullet()
    {
        Debug.Log("fire bullet");
        RaycastHit hit;
        Vector3 fwd = Player.transform.forward;
        if (Physics.Raycast(transform.position, fwd, out hit, 25, mask.value) )
        {
            Debug.Log("i hit: " + hit.transform.name + " : " + hit.point);
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
    }
    IEnumerator FireChaingun()
    {
        Debug.Log("fire chain");
        RoF = 0.02f;
        //timer > RoF
        while (Input.GetButton("R2"))
        {
            FireBullet();
            //timer = 0;
            yield return new WaitForSeconds(RoF);
        }
    }
    void ChangeWeapon()
    {

    }
 }


