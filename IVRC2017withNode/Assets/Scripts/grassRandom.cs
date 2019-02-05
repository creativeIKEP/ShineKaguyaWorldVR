using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassRandom : MonoBehaviour {
	public GameObject grass1;
	public GameObject grass2;
	public GameObject grass3;
	private GameObject selectedGrass;

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= 30; i++) {
			float xpos = 0, zpos = 0;
			while ((-1.5f <= xpos && xpos <= 1.5f) && (-1.5f <= zpos && zpos <= 1.5f)) {
				xpos = Random.Range (-15.0f, 15.0f);
				zpos = Random.Range (-15.0f, 15.0f);
			}
			Vector3 position = new Vector3 (xpos, -1, zpos);

			int x = (int)(2 * Random.value + 1);
			if (x == 1) {
				selectedGrass = grass1;
			} else if (x == 2) {
				selectedGrass = grass2;
			} else {
				selectedGrass = grass3;
			}
			Instantiate (selectedGrass, position, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
