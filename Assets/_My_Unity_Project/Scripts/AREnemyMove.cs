using UnityEngine;
using Vuforia;

public class AREnemyMove : MonoBehaviour, ITrackableEventHandler
                                                
    {
    public GameObject Enemy;
    public GameObject ImageTargetPath1;
    public float speed = 0.2f;

    private TrackableBehaviour mTrackableBehaviour;
    private Transform tr_Enemy;
    private Transform tr_Path1;

    void Start()
        {
            mTrackableBehaviour = ImageTargetPath1.GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
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
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
        // Play when target is found
        Enemy.GetComponent<Animator>().SetTrigger("Run");

        }
        else
        {
       //Stop play when target is lost
        Enemy.GetComponent<Animator>().SetTrigger("Idle");
        
        }
    }

}
