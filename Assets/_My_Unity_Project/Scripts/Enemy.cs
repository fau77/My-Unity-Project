using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 2f;

    private Transform waypoints;
    private Transform waypoint;

    private int waypointIndex = -1;

    // Use this for initialization
    void Start () {
        waypoints = GameObject.Find("WayPoints").transform;
        NextWaypoint();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = waypoint.transform.position - transform.position;
        dir.y = 0;

        float _speed = Time.deltaTime * speed;
        transform.Translate(dir.normalized * _speed);
        if (dir.magnitude <= _speed)
            NextWaypoint();
    }

    void NextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.childCount)
        {
            Destroy(gameObject);
            return;
        }
        waypoint = waypoints.GetChild(waypointIndex);
    }
}
