using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARTowers.AR
{
    public class ARGameController : MonoBehaviour
    {

        public AREnemy Enemy;
        public ARBullet Bullet;
        public GameObject Tower;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (ARButtons.StartGame)
            {
               Enemy.EnemyMove(Tower.transform.position);
               if (Bullet) { Bullet.BulletMove(); };
            }

        }
    }
}