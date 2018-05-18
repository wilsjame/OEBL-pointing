using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHotspots : MonoBehaviour {

	/* Points to be used in circular array */
	public Transform static_point;

	/* Trigger point prefab */
	public Transform trigger_point;

	/* Another global variable (oh no!) to keep track of list iterations */
	public int itr = 0;

	/* Use this for initialization */
	void Start () {

		/* Generate */
		circularArray ();

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();
	
	}

	/* Update is called once per frame */
	void Update () {
	}

	/* Generate circular array of static points */ 
	public void circularArray ()
	{
		int numberOfObjects = 18;
		float radius = .5f;

		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
			Instantiate(static_point, pos, Quaternion.identity);
		}

	}

	/* This function is called from Hotspot.cs after Start () */
	public void HotSpotTriggerInstantiate ()
	{

		/* Use a list to handle point coordinates */
		List<Vector3> coOrds_collection = new List<Vector3> (); 	

		/* Generate and add circular array coodinate points to the list */
		int numberOfObjects = 18;
		float radius = .5f;

		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
			coOrds_collection.Add (pos);
		}
	
		/* Do only once, Fisher Yates shuffle list to randomize spawn order */
		/*
		if (itr == 0) {

			// Debugging 
			Debug.Log ("Shuffling hotspot spawn points!");

			int random_placeholder;

			for (int i = 0; i < coOrds_collection.Count; i++) {
				random_placeholder = i + Random.Range (0, coOrds_collection.Count - i);

				// Swap 
				coords_temp = coOrds_collection [i];
				coOrds_collection [i] = coOrds_collection [random_placeholder];
				coOrds_collection [random_placeholder] = coords_temp;
			}

		}
		*/
			
		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
			
			/* Debugging */
			Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);

			/* Spawn the point */ 
			Instantiate (trigger_point, coOrds_collection [itr], Quaternion.identity);
			itr++;

			/* Debugging */
			if (itr == coOrds_collection.Count) {
				Debug.Log ("Entire Coords_Collection spawned!");
				Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			}

		}

	}

}