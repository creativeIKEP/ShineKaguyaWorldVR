using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSound : MonoBehaviour {
	public AudioSource swing;
    public GameObject Ax;
    public GameObject bamboo;
    public AudioSource suprise;

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
    }
    void suprisevoice()
    {
        suprise.Play();
    }
}
