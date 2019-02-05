using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieControl : MonoBehaviour {
	//public GameObject movie;
	public VideoPlayer videoPlayer;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    public GameObject plane5;
    public GameObject games;
    public GameObject camera;
    public GameObject percent;
    private float time = 0;

	// Use this for initialization
	void Start () {
        camera.GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = false;
    }

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (videoPlayer.isPlaying == false && time>=5.0f) {
			plane1.gameObject.SetActive (false);
            plane2.gameObject.SetActive(false);
            plane3.gameObject.SetActive(false);
            plane4.gameObject.SetActive(false);
            plane5.gameObject.SetActive(false);
            games.SetActive(true);
            percent.SetActive(true);
            camera.GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = true;
        }

	}
}
