using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARButtons : MonoBehaviour
{

    public GameObject MenuText;
    public GameObject Enemy;
    public GameObject Cave;
    public float EnemyRunSpeed;
    public float EnemyRotateSpeed;
    private bool EnemyMooving = false;
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
                Debug.Log(el.Name + " position: " + el.Target.transform.position);
                Debug.Log(el.Name + " rotation: " + el.Target.transform.rotation);
            }
        }
        hsTargetNamesMust.Clear();
        hsTargetNamesHave.Clear();
    }

    public void ARStart()
    {
        //GameObject.Find("Enemy").GetComponent<Animator>().SetTrigger("Run");
        //Enemy.GetComponent<Animator>().SetTrigger("Run");
        EnemyMooving = true;
        //GameObject.Find("Enemy").transform.LookAt(Cave.transform);
        //GameObject.Find("Enemy").transform.position.Set(10, 0, 10);
    }

    void Update()
    {
        if (EnemyMooving)
        {
            if //(Mathf.Abs(Quaternion.Dot(Enemy.transform.rotation, Quaternion.LookRotation(Cave.transform.position))) < 0.9f) 
            (Enemy.transform.rotation != Cave.transform.rotation)
            {
               Enemy.transform.rotation = Quaternion.RotateTowards(Enemy.transform.rotation, Cave.transform.rotation, Time.deltaTime * EnemyRotateSpeed);
               
            }
            else
            {
            Enemy.transform.LookAt(Cave.transform);
            Enemy.GetComponent<Animator>().SetTrigger("Run");
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Cave.transform.position, Time.deltaTime*EnemyRunSpeed);
            }
        }
    }
}