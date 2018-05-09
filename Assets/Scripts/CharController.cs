using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

	// Gernal variables 
    Vector3 forward, right;
    public  float dashStrength;
    public  CharacterController CharCont;
    public  DashState dashState;
    public  float dashTime;
    public  float dashCD;
    public  float iterate = 1f;
            float timer;
    public  float HPDuration;

    // Directional info
    Vector3 heading;
    Vector3 mHeading;

    // Player Stats
    public  float   HP;
    public  TextMesh HPText;
    public  float   MaxHP;
    public  float   moveSpeed = 4f;
    public  float   engery;

    // weapon Stats
    public  string  equipedWeapon;


	void Start () 
    {
        forward     = Camera.main.transform.forward;
        forward.y   = 0;
        forward     = Vector3.Normalize(forward);

        heading     = new Vector3(0, 0, 0);
        mHeading    = new Vector3(0, 0, 0);

        dashState   = DashState.Ready;

        right       = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        StartCoroutine(UpdateHealth());
	}
	
	// Update is called once per frame
	void Update() 
    {
        
        if (dashState != DashState.Dashing)
        {
            Move();
            Look();
        }

        if (Input.GetButtonDown("Square"))
        {
            if(dashState == DashState.Ready)
            {
                timer = 0;
                StartCoroutine(DashCR());
            }
            else
            {
                Debug.Log("Dash on cooldown");
            }
        }
        
        if (Input.GetButton("Cross"))
        {
            Debug.Log("Hit Cross");
            StartCoroutine(HealthKit());
        }
        
	}

    //point player front direction with right analog stick
    void Look()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("RSH"), 0, Input.GetAxisRaw("RSV"));
        Vector3 rightMovement = -right * Input.GetAxisRaw("RSH");
        Vector3 upMovement = -forward * Input.GetAxisRaw("RSV");

        heading = Vector3.Normalize(rightMovement + upMovement);

        if(direction != new Vector3 (0,0,0))
            transform.forward = heading;
    }

    //move player with left analog stick
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("LSH"), 0, Input.GetAxis("LSV"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("LSH");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("LSV");

        mHeading = Vector3.Normalize(rightMovement + upMovement);

        //transform.forward = heading;
        CharCont.Move(rightMovement);
        CharCont.Move(upMovement);

        // for gravity, player will float when they move verticly without this
        CharCont.Move(new Vector3(0,-1,0));
 
    }

    IEnumerator UpdateHealth()
    {
        while (HP > 0)
        {
            HPText.text = "HP: " + HP;
            yield return new WaitForSeconds(.5f);
        }
    }
    void UseItem()
    {

    }

    // Start of Abilities
    IEnumerator DashCR()
    {
        dashState = DashState.Dashing;
        Debug.Log("in DashCR");
        while (dashState == DashState.Dashing)
        {
            Debug.Log("Dash while");

            if (timer > dashTime)
            {
                dashState = DashState.Cooldown;
                Debug.Log("Dashstate now CD");
                break;
            }

            CharCont.Move(mHeading * dashStrength * 0.01f);
            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
            

        }
        timer = 0;

        while (timer < dashCD)
        {
            //Debug.Log("CD while");
            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("Dashstate ready");

        dashState = DashState.Ready;
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    // End of Abilities



    // Start of Items and Equipment

    IEnumerator HealthKit()
    {
        Debug.Log("Health Kit");
        float timer = 0;
        while (timer < HPDuration)
        {
            Debug.Log("Healing");
            timer += .2f;
            if (HP < MaxHP - 10)
            {
                HP += 10;
            }
            else if (HP >= MaxHP - 10 && HP < MaxHP)
            {
                HP += 100 - HP;
            }
            else
            {
                Debug.Log("Health Full");
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator Grenade()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Mine()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Turret()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Bait()
    {
        yield return new WaitForSeconds(1);
    }


    // End of Items and Equipment 
}

