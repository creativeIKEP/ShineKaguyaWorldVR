using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bambooSet : MonoBehaviour {
	public GameObject bamboo;
	public int count;
    public float renge;
  //  public float renge1;
    private int i;

    // Use this for initialization
    void Start()
    {
        for (int i = 1; i <= count; i++)
        {
            float x = 0, z = 0;
            while ((-3.0f <= x && x <= 3.0f) && (-3.0f <= z && z <= 3.0f))
            {
                x = Random.Range(-renge, renge);
                z = Random.Range(-renge, renge);
            }

            Vector3 position = new Vector3(x, -0.05f, z);
            Instantiate(bamboo, position, Quaternion.identity);

            //        for(int i=1; i<=count*2/3; i++)
            //       {
            //            float x = 0, z = 0;
            //            	while ((-1.5f <= x && x <= 1.5f) && (-1.5f <= z && z <= 1.5f)) {
            //            		x = Random.Range (-renge, renge);
            //            		z = Random.Range (-renge, renge);
            //            	}
            //            Vector3 position = new Vector3(x, 0, z);
            //            Instantiate (bamboo, position, Quaternion.identity);
            //        }
            //        for(; i <= count; i++){
            //            float x = 0, z = 0;
            //            while ((-renge <= x && x <= renge) && (-renge <= z && z <= renge))
            //            {
            //                x = Random.Range(-renge1, renge1);
            //                z = Random.Range(-renge1, renge1);
            //            }
            //            Vector3 position = new Vector3(x, 0, z);
            //          Instantiate(bamboo, position, Quaternion.identity);
            //        } 

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
