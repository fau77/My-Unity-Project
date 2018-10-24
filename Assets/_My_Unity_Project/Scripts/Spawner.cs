using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject spawnObject;
	public float spawnTime = 1f;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Instantiate (spawnObject, transform.position, transform.rotation);
			timer = spawnTime;
		}
	}
}
