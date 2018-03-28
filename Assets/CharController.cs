using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

	// Gernal variables 
    Vector3 forward, right;
    public float dashStrength;
    public Rigidbody rb;
    public DashState dashState;
    public float dashTime;
    public float dashCD;
    public float iterate = 1f;
    float timer;
    public float HPtimer;

    // Player Stats
    public  float   HP;
    public  Text    HPText;
    public  float   MaxHP;
    public  float   moveSpeed = 4f;
    public  float   engery;
    public  string  equipedWeapon;


	void Start () 
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        StartCoroutine(UpdateHealth());
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (dashState == DashState.Ready || dashState == DashState.Cooldown)
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
        
        if (Input.GetButtonDown("Triangle"))
        {
            Debug.Log("Hit Triangle");
            StartCoroutine(HealthKit());
        }
        
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

    //move player with left analog stick
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("LSH"), 0, Input.GetAxis("LSV"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("LSH");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("LSV");

        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    IEnumerator UpdateHealth()
    {
        while (HP > 0)
        {
            HPText.text = "HP: " + HP + "/" + MaxHP;
            yield return new WaitForSeconds(.1f);
        }
    }
    void UseItem()
    {

    }
    //Dash player, locks movement
    void Dash()
    {
        Debug.Log("dash");
        dashState = DashState.Dashing;
        rb.AddForce(transform.forward * dashStrength);
    }

    //undos dash speed boost
    void Hsad()
    {
        Debug.Log("Hsad");
        rb.AddForce(-transform.forward * dashStrength);
    }

    IEnumerator HealthKit()
    {
        Debug.Log("Health Kit");
        float timer = 0;
        while(timer < HPtimer)
        {
            Debug.Log("Healing");
            timer += .2f;
            HP += 10;
            yield return new WaitForSeconds(.2f);
        }
    }




    IEnumerator DashCR()
    {
        Debug.Log("in DashCR");
        Dash();

        while (dashState == DashState.Dashing)
        {
            Debug.Log("Timer: " + timer + "  Dashtime" + dashTime);
            if (timer > dashTime)
            {
                dashState = DashState.Cooldown;
                Debug.Log("Dashstate now CD");
                break;
            }
            Debug.Log("Dash while");
            timer += 0.1f;
            Debug.Log("Dash while2");
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("before Force removed");
        Hsad();
        Debug.Log("Force removed");

        timer = 0;

        while (timer < dashCD)
        {
            Debug.Log("CD while");
            timer += 1;
            yield return new WaitForSeconds(1);
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
}

