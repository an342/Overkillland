using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager instance;
    public GameObject[] pool;
	// Use this for initialization
	void Start () {
		if(Manager.instance == null)
        {
            Manager.instance = this;

        }
        /*
        pool = new GameObject[12];
        Resources.Load(GameObject)
        for(int i = 0; i < 12; int++)
        {
            pool[i] 
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
