using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AREnemyMove : MonoBehaviour, ITrackableEventHandler

{
    public GameObject Enemy;
    public GameObject ImageTargetPath1;
    public GameObject ImageTargetTower1;
    public float speed = 0.2f;

    private TrackableBehaviour tb_Path1;
    private TrackableBehaviour tb_Tower1;

    private Transform tr_Enemy;
    private Transform tr_Path1;

    void Start()
    {
        tb_Path1 = ImageTargetPath1.GetComponent<TrackableBehaviour>();
        if (tb_Path1)
        {
            tb_Path1.RegisterTrackableEventHandler(this);
        }

        tb_Tower1 = ImageTargetTower1.GetComponent<TrackableBehaviour>();
        if (tb_Tower1)
        {
            tb_Tower1.RegisterTrackableEventHandler(this);
        }

        tr_Enemy = GameObject.Find("Enemy").transform;
        tr_Path1 = GameObject.Find("ImageTargetPath1").transform;
    }

    void Update()
    {
        //Vector3 dir = tr_Path1.transform.position - tr_Enemy.transform.position;

        //float _speed = Time.deltaTime * speed;
        //tr_Enemy.transform.Translate(dir.normalized * _speed);
        //if (dir.magnitude <= _speed) { tr_Enemy.transform.Translate(dir.normalized); };
        //tr_Enemy.transform.position = tr_Path1.transform.position;
        //Debug.Log(tr_Enemy.transform.position);

        // Get the Vuforia StateManager
        //StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        //IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        //Debug.Log("List of trackables currently active (tracked): ");
        //foreach (TrackableBehaviour tb in activeTrackables)
        //{
        //    Debug.Log("Trackable: " + tb.TrackableName);
        //}
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        
        Debug.Log("Before foreach: " + newStatus);
        

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            Debug.Log("Before check: " + tb.TrackableName);
            Debug.Log("Status: " + newStatus);
            //if (tb.TrackableName == "Tower3")
            //{
            //    if (newStatus == TrackableBehaviour.Status.DETECTED ||
            //   newStatus == TrackableBehaviour.Status.TRACKED ||
            //    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            //    {
            //        // Play when target is found
            //        Enemy.GetComponent<Animator>().SetTrigger("Run");
            //    }
            //    else
            //    {
            //        //Stop play when target is lost
            //        Enemy.GetComponent<Animator>().SetTrigger("Idle");
            //    }
            //}
        }
    }
}

