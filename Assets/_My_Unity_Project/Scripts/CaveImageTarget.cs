using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CaveImageTarget : MonoBehaviour, ITrackableEventHandler
                                                
    {
        private TrackableBehaviour mTrackableBehaviour;
        public GameObject Enemy;

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
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
            // Stop audio when target is lost
            Enemy.GetComponent<Animator>().SetTrigger("Idle");
        }
        }
    }
