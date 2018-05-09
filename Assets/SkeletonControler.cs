using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonControler : MonoBehaviour {
    Transform player;
    NavMeshAgent nav;
    public int HP;
    Animator m_Animator;
    bool alive;
    float despawnCount;
    AudioSource audio;

	// Use this for initialization
	void Awake () 
    {
        m_Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        alive = true;
        HP = 50;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //Debug.Log("player position: "+ player.position);
        if (HP <= 0 && despawnCount == 0)
        {
            audio.Play();
            alive = false;
            m_Animator.SetTrigger("Dead");
        }

        if(alive)
            nav.SetDestination(player.position);
        else
        {
            despawnCount += Time.deltaTime;
            Debug.Log("death timer: " + despawnCount);
        }

        if (despawnCount > 5)
        {
            Destroy(this);
        }
        
	}
}
