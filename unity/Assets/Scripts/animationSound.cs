using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationSound : MonoBehaviour {
	public AudioSource swing;
    public GameObject Ax;
    public GameObject bamboo;
    public AudioSource suprise;
	public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void soundPlay(){
		swing.Play ();
	}

    void disappearAx()
    {
        Ax.SetActive(false);
        bamboo.SetActive(false);
		agent.enabled = false;
	}
    void suprisevoice()
    {
        suprise.Play();
    }
}
