// For use with single plane pointing task.
// TODO fix infinite NullReferenceException after task completion.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Threading;


public class SpawnHotspots : MonoBehaviour {

	/* Parent gameObject to hold generated hotspot collection */
	GameObject parentObject;

	/* Prefabs */
	public Transform static_point;
	public Transform trigger_point;

	/* Encapsulated trial counter coordinates */ 
	public struct CoOrds
	{
		public float x, y, z;
		public string plane;

		// Constructor to initiliaze x, y, and z coordinates
		public CoOrds(float x_coOrd, float y_coOrd, float z_coOrd, string p)
		{
			x = x_coOrd;
			y = y_coOrd;
			z = z_coOrd;
			plane = p;
		}
	}

	List<List<CoOrds>> coOrds_collection = new List<List<CoOrds>> ();	/* Entire point collection */
	List<CoOrds> coOrds_collection_2 = new List<CoOrds> (); /* z = 0.3 frame points */
	public int[] order = {0};				/* Plane spawn order */
	public int itr = 0;					/* Keep track of list iterations */
	public int trial = 0;					/* Keep track of completed trials */

	public string fileName = "pointing_task_time_";
	public Stopwatch stopwatch = new Stopwatch();
	public string path; 

	/* Use this for initialization */
	void Start () {

		// Create unique out file 
		fileName = fileName + System.DateTime.Now + ".txt";
		fileName = fileName.Replace("/","-");
		fileName = fileName.Replace(":",";");
		path = Path.Combine(Application.persistentDataPath, fileName);
		//Test outfile
		//File.WriteAllText(@path, "trace");
		
		/* Generate */
		initializeCoordinates (ref order, ref coOrds_collection, ref coOrds_collection_2);

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();
	
	}

	/* Generate circular arrays of static points */ 
	public void initializeCoordinates (ref int[] order, ref List<List<CoOrds>> coOrds_collection, ref List<CoOrds> coOrds_collection_2)
	{
		int i;
		int temp;
		CoOrds temp_vector;
		int random_placeholder;
		int numberOfObjects = 18;
		float radius = .5f;

		/* z = 0.3 frame */
		for (i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			CoOrds pos = new CoOrds(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0.3f, "middle");
			coOrds_collection_2.Add(pos);
		}

		/* Shuffle */
		for (i = 0; i < numberOfObjects; i++) {
			random_placeholder = i + Random.Range (0, numberOfObjects - i);

			/* Swap */
			temp_vector = coOrds_collection_2[i];
			coOrds_collection_2[i] = coOrds_collection_2[random_placeholder];
			coOrds_collection_2[random_placeholder] = temp_vector;
		}

		/* Add plane to entire collection */
		coOrds_collection.Add(coOrds_collection_2);

		/* Spawn initial static points */ 
		for (i = 0; i < numberOfObjects; i++) {
			temp_vector = coOrds_collection[order[trial]] [i];
			Transform static_pt = Instantiate(static_point, new Vector3 (temp_vector.x, temp_vector.y, temp_vector.z), Quaternion.identity, this.transform); // Make this gameObject the parent
			
		}

	}

	/* Spawn trigger points until 1 plane is completed */
	public void HotSpotTriggerInstantiate ()
	{
		/* check if user has tapped first point */
		if (itr == 1) {
			// Begin timing
			stopwatch.Start();
		}

		CoOrds coords_temp = new CoOrds ();

		/* Spawn trigger points */ 
		if ( itr < coOrds_collection[order[trial]].Count) {
			coords_temp = coOrds_collection[order[trial]] [itr];
			Transform trigger = Instantiate (trigger_point, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity, this.transform); // Make this gameObject the parent
			trigger.localPosition = new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z); // Spawn position relative to parent
			itr++;
		}

		/* Trial is completed */
		else {
			// Stop timing
			System.TimeSpan ts = stopwatch.Elapsed;
			stopwatch.Stop();
			UnityEngine.Debug.Log("Time elapsed: " + ts);
			stopwatch.Reset();

			// Write time to file
			File.WriteAllText(@path, ts.ToString()); 
		}

	}

}
