using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MonoBehaviour 
{

	// Use this for initialization
    [SerializeField] GameObject Player;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitEffect2;
    [SerializeField] GameObject hitEffect3;
    [SerializeField] GameObject hitEffect4;
    [SerializeField] float RoF;
    [SerializeField] string[] Weapons;
    [SerializeField] string equipped;
    private int equippedNum = 0;
    [SerializeField] int[] ammo;
    [SerializeField] int[] ammoMAX;
    public TextMesh ammoDisplay;
    AudioSource audio;
    private float timer;
    private bool semiHold;
    

	void Start () 
    {
        mask = LayerMask.GetMask("Wall");
        equipped = "Pistol";
        equippedNum = 0;
        timer = 0;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        ammoDisplay.text = ammo[equippedNum].ToString();
	}
     
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("R2"))
        {
            switch (equipped)
            {
                case "Pistol":
                    if (ammo[0] > 0)
                        FirePistol();
                    else
                        Debug.Log("Out of Ammo");
                    break;
                case "MG":
                    if (ammo[1] > 0)
                        StartCoroutine(FireMG());
                    else
                        Debug.Log("Out of Ammo");
                    break;
                case "Chain":
                    if(ammo[2] > 0)
                        StartCoroutine(FireChaingun());
                    else
                        Debug.Log("Out of Ammo");
                    break;
                case "Burst":
                    if(ammo[3] > 0)
                        StartCoroutine(FireBurst());
                    else
                        Debug.Log("Out of Ammo");
                    break;
                default:
                    Debug.Log("No weapon equipped!");
                    break;
            }
        }

        if (Input.GetButtonDown("Triangle"))
        {
            ChangeWeapon();
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
        while (Input.GetButton("R2") && !Input.GetButton("Triangle"))
        {
            Debug.Log("fireing MG");
            FireBullet();
            ammo[1]--;
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
        while (Input.GetButton("R2") && !Input.GetButton("Triangle"))
        {
            while (count < 3)
            {
                Debug.Log("pow");
                FireBullet();
                ammo[3]--;
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
        while (semiHold )
        {
            semiHold = false;
            yield return new WaitForSeconds(RoF);
        }
    }


    void FirePistol()
    {
        RoF = 0.5f;
        Debug.Log("fire pistol" );
        if (!semiHold && !Input.GetButton("Triangle"))
        {
            FireBullet();
            ammo[0]--;
            StartCoroutine(SemiAuto(RoF));
        }
    }
    void FireBullet()
    {
        Debug.Log("fire bullet");
        RaycastHit hit;
        Vector3 fwd = Player.transform.forward;
        audio.Play();
        if (Physics.Raycast(transform.position, fwd, out hit, 35) )
        {
            Debug.Log("i hit: " + hit.transform.name + " : " + hit.point);
            if(hit.transform.tag == "Enemy")
            {
                Damage(hit.transform.gameObject, 15);
            }
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
    }
    IEnumerator FireChaingun()
    {
        Debug.Log("fire chain");
        RoF = 0.05f;
        //timer > RoF
        while (Input.GetButton("R2") )
        {
            FireBullet();
            ammo[2]--;
            //timer = 0;
            yield return new WaitForSeconds(RoF);
        }
    }
    void ChangeWeapon()
    {
        switch (equipped)
        {
            case "Pistol":
                equipped = "MG";
                equippedNum = 1;
                break;
            case "MG":
                equipped = "Burst";
                equippedNum = 3;
                break;
            case "Burst":
                equipped = "Pistol";
                equippedNum = 0;
                break;
            default:
                Debug.Log("No weapon equipped!");
                break;
        }
    }

    void Damage(GameObject target, int damage)
    {
        Debug.Log("dealing damage");
        target.GetComponent<SkeletonControler>().HP = target.GetComponent<SkeletonControler>().HP - damage;
    }
 }


