using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeApperance : MonoBehaviour {
    public GameObject flash;
    public GameObject mybamboo;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void flashOn()
    {
        flash.SetActive(true);
        mybamboo.SetActive(false);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
