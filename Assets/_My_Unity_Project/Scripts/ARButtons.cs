using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ARAlign()
    {
        HashSet<string> hsTargetNamesMust = new HashSet<string>();
        hsTargetNamesMust.Add("Cave");
        hsTargetNamesMust.Add("Tower1");
        hsTargetNamesMust.Add("Enemy");

        HashSet<string> hsTargetNamesHave = new HashSet<string>();

        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            hsTargetNamesHave.Add(tb.TrackableName);
            Debug.Log("Trackable: " + tb.TrackableName);
        }
        if (hsTargetNamesMust.Count == hsTargetNamesHave.Count)
        {
            Debug.Log("Equal!");
        }
        foreach (string st1 in hsTargetNamesMust) { Debug.Log("Must: " + st1); };
        foreach (string st2 in hsTargetNamesHave) { Debug.Log("Have: " + st2); };
        hsTargetNamesMust.Clear();
        hsTargetNamesHave.Clear();
    }

    public void ARStart()
    {

    }
}
