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
        //Массив (точнее HashSet), содержащий набор имен необходимых на поле объектов
        HashSet<string> hsTargetNamesMust = new HashSet<string>();
        //Заполняем его требуемыми именами
        foreach (var el in sf_Targets)
        {
            hsTargetNamesMust.Add(el.Name);
        }
        //Массив определившихся на поле объктов
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
            //Заполняем его из activeTrackables
            hsTargetNamesHave.Add(tb.TrackableName);
        }
        //Сравниваем два набора, если не хватает объектов - выводим предупреждение
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
        StartGame = true;
        //Подготовка снаряда турели
        Bullet.SetActive(true);
        //Направление и дистанция полета снаряда
        //BulletTarget = (BulletStartPosition.transform.localPosition - TurretGun.transform.localPosition) * BulletMaxDistance;
        //BulletTarget = (BulletStartPosition.transform.position - TurretGun.transform.position) * BulletMaxDistance;
        Bullet.transform.position = BulletStartPosition.transform.position;
        BulletTarget = BulletStartPosition.transform.TransformPoint(Vector3.back * BulletMaxDistance);
        Enemy.transform.LookAt(Cave.transform);
    }

    void Update()
    {
        if (StartGame)
        {
            //Проверяем угол оворота Enemy к Cave - если большой сначала поворачиваем Enemy
            //Debug.Log("Angle: " + Quaternion.Angle(Enemy.transform.rotation, Cave.transform.rotation));
            //if (Quaternion.Angle(Enemy.transform.rotation, Cave.transform.rotation) > 0.2f)
            //{
               //Enemy.transform.rotation = Quaternion.RotateTowards(Enemy.transform.rotation, Cave.transform.rotation, Time.deltaTime * EnemyRotateSpeed);  
               //Enemy.transform.LookAt(Cave.transform);
            //}
            //else
            //{
            //Запускаем анимацию бега для Enemy
            if (Enemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "Run") { Enemy.GetComponent<Animator>().SetTrigger("Run"); };
            Enemy.transform.LookAt(Cave.transform);
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Cave.transform.position, Time.deltaTime * EnemyRunSpeed);
            //}

            //Debug.Log("Bullet position/magnitude: " + Bullet.transform.position + "/" + Bullet.transform.position.magnitude);
            //Debug.Log("BuletTarget position/magnitude: " + BulletTarget + "/" + BulletTarget.magnitude);
            //Debug.DrawLine(BulletStartPosition.transform.position, Bullet.transform.position, Color.red, 10f);
            if ((BulletTarget - Bullet.transform.position).magnitude < 0.1f)
            {
                //Bullet.transform.localPosition = BulletStartPosition.transform.localPosition;
                Bullet.transform.position = BulletStartPosition.transform.position;
                BulletTarget = BulletStartPosition.transform.TransformPoint(Vector3.back * BulletMaxDistance);
            }
            //Bullet.transform.localPosition = Vector3.MoveTowards(Bullet.transform.localPosition, BulletTarget, Time.deltaTime * BulletSpeed);
            //Выпускаем снаряд от дула пушки до дистанции, указанной в BulletMaxDistance
            Bullet.transform.position = Vector3.MoveTowards(Bullet.transform.position, BulletTarget, Time.deltaTime * BulletSpeed);

        }
    
    }
}