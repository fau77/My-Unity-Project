using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCollisions : MonoBehaviour {

    public float EnemyHP = 20f;
    public float BulletDamage = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {

        Debug.Log("Столкновение!");
        if (collider.gameObject.name == "Bullet")
        {
            EnemyHP -= BulletDamage;
            Debug.Log("Остаток здоровья: " + EnemyHP);
            gameObject.GetComponent<Animator>().SetTrigger("RoundKick");
            if (EnemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    } 
}
