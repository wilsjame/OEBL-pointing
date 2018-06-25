using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHotspots : MonoBehaviour {

	/* Parent gameObject to hold generated hotspot collection */
	GameObject parentObject;

	/* Points to be used in circular array */
	public Transform static_point;

	/* Trigger point prefab */
	public Transform trigger_point;

	List<Vector3> coOrds_collection = new List<Vector3> ();	/* Entire point collection */
	public int itr = 0;					/* Keep track of list iterations */

	/* Use this for initialization */
	void Start () {

		/* Generate */
		initializeCircularArray (ref coOrds_collection);

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();
	
	}

	/* Generate circular array of static points */ 
	public void initializeCircularArray (ref List<Vector3> coOrds_collection)
	{
		int numberOfObjects = 18;
		float radius = .5f;

		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
			coOrds_collection.Add (pos);

			Instantiate(static_point, pos, Quaternion.identity, this.transform); // Make this gameObject the parent
		}

	}

	/* This function is called from Hotspot.cs after Start () */
	public void HotSpotTriggerInstantiate ()
	{

		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
			
			/* Debugging */
			//Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);

			/* Spawn the point at random */ 
			Transform trigger = Instantiate (trigger_point, coOrds_collection [Random.Range (0, coOrds_collection.Count)], Quaternion.identity, this.transform); // Make this gameObject the parent
			trigger.localPosition = coOrds_collection [Random.Range (0, coOrds_collection.Count)]; // Spawn position relative to parent

			itr++;

			/* Debugging and spawn points forever */
			if (itr == coOrds_collection.Count) {
				Debug.Log ("Entire Coords_Collection spawned! Starting over.");
				Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
				itr = 0;
			}

		}

	}

}
