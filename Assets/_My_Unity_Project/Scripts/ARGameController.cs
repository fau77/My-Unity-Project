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
        public GameObject EnemyDieExp;
        [SerializeField]  private GameObject Win;
        [SerializeField] private GameObject Lose;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (ARButtons.StartGame)
            {
                if (Enemy.isEnemyAlive)
                {
                    if (Enemy.isEnemyAtacking)
                    {
                        Enemy.EnemyAttack();
                        Lose.SetActive(true);
                    }
                    else
                    {
                        Enemy.EnemyMove(Tower.transform.position);
                    }
                    if (Bullet) { Bullet.BulletMove(); }
                }
                else
                {
                    if (!EnemyDieExp.GetComponent<ParticleSystem>().isPlaying)
                    {
                        if (Enemy.isActiveAndEnabled)
                        {
                            EnemyDieExp.transform.localPosition = Enemy.transform.localPosition;
                            EnemyDieExp.GetComponent<ParticleSystem>().Play();
                            EnemyDieExp.GetComponent<AudioSource>().Play();
                            if (!Lose.gameObject.activeSelf) Win.SetActive(true);
                        }
                    }
                    Enemy.gameObject.SetActive(false);
                    Bullet.BulletActivate(false);
                }
               
            }

        }
    }
}