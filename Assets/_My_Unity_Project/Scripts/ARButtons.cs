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
    public GameObject Bullet;
    public GameObject BulletStartPosition;
    public GameObject TurretGun;
    public float EnemyRunSpeed;
    public float EnemyRotateSpeed;
    public float BulletSpeed;
    public float BulletMaxDistance;
    private bool StartGame = false;
    public float BulletTime = 1f;
    private float BulletTimer = 0;
    private Vector3 BulletTarget;
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
        StartGame = true;
        //GameObject.Find("Enemy").transform.LookAt(Cave.transform);
        //GameObject.Find("Enemy").transform.position.Set(10, 0, 10);
    }

    void Update()
    {
        if (StartGame)
        {
            if (Quaternion.Angle(Enemy.transform.rotation, Cave.transform.rotation) > 0.2f)
            //(Mathf.Abs(Quaternion.Dot(Enemy.transform.rotation, Quaternion.LookRotation(Cave.transform.position))) < 0.9f) 
            //(Enemy.transform.rotation != Cave.transform.rotation)
            {
               Enemy.transform.rotation = Quaternion.RotateTowards(Enemy.transform.rotation, Cave.transform.rotation, Time.deltaTime * EnemyRotateSpeed);
               
            }
            else
            {
            //Enemy.transform.LookAt(Cave.transform);
            if (Enemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "Run") { Enemy.GetComponent<Animator>().SetTrigger("Run"); };
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Cave.transform.position, Time.deltaTime * EnemyRunSpeed);
            }

            //Instantiate(Bullet, BulletStartPosition.transform.position, BulletStartPosition.transform.rotation);
            Bullet.SetActive(true);
            //BulletTarget.Set(BulletStartPosition.transform.position.x - BulletMaxDistance, BulletStartPosition.transform.position.y - BulletMaxDistance, BulletStartPosition.transform.position.z - BulletMaxDistance);
            BulletTarget = (BulletStartPosition.transform.localPosition - TurretGun.transform.localPosition) * BulletMaxDistance;
            //BulletTarget = BulletStartPosition.transform.localPosition * BulletMaxDistance;
            Debug.Log("BulletStartPosition position: " + BulletStartPosition.transform.position);
            Debug.Log("BuletTarget position: " + BulletTarget);
            Bullet.transform.localPosition = Vector3.MoveTowards(Bullet.transform.localPosition, BulletTarget, Time.deltaTime * BulletSpeed); ;
        }
    
    }
}