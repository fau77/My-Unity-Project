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

        //GameObject MenuText = GameObject.Find("MenuText");

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
        }
       
        hsTargetNamesMust.ExceptWith(hsTargetNamesHave);
        if (hsTargetNamesMust.Count > 0)
        {
            //MenuText.SetActive(true);
            GUI.Label(new Rect(0, 0, 120, 100), "Предметов собрано");
        }

        hsTargetNamesMust.Clear();
        hsTargetNamesHave.Clear();
    }

    public void ARStart()
    {

    }
}
