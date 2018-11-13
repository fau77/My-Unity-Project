using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARButtons : MonoBehaviour {

    public GameObject MenuText;
    [SerializeField] public GameTargets[] sf_Targets;
    [Serializable]
    public struct GameTargets
    {
        public string Name;
        public GameObject Target;
    }

    public void ARAlign()
    {
        HashSet<string> hsTargetNamesMust = new HashSet<string>();
        //hsTargetNamesMust.Add("Cave");
        //hsTargetNamesMust.Add("Tower1");
        //hsTargetNamesMust.Add("Enemy");
        foreach (var el in sf_Targets)
        {
            hsTargetNamesMust.Add(el.Name);
        }

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
            MenuText.SetActive(true);
        }
        else
        {
            MenuText.SetActive(false);
            Debug.Log("All targets ready!");
        foreach (var el in sf_Targets)
            {
                Debug.Log(el.Name + ": " + el.Target.transform.position);
            }
        }
        hsTargetNamesMust.Clear();
        hsTargetNamesHave.Clear();
    }

    public void ARStart()
    {
        sf_Targets[0].Target.GetComponent<Animator>().SetTrigger("Run");

    }
}
