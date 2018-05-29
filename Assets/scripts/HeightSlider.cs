using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class HeightSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Get slider value, called by slider's event update
	public void getSlider()
	{

		// Get the slider's current value 
		GameObject slider = GameObject.Find("Height_Slider"); // Grab height slider from scene
		SliderGestureControl sliderScript = slider.GetComponent<SliderGestureControl>(); // Grab script off of slider
		float sliderValue = sliderScript.GetSliderValue ();

		// Move the sphere collection according to the slider value
		GameObject sphere_collection = GameObject.Find("GameObject_SpawnHotSpots"); // Grab the collection from scene
		sphere_collection.transform.position = new Vector3(0.0f, sliderValue, 0.0f);
	}
		
}
