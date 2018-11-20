using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для управления свойствами снаряда/пули Bullet
public class ARBullet : MonoBehaviour
{

    // Скорость передвижения пули
    [SerializeField] private float BulletSpeed = 2f;
    // Дальность передвижения пули
    [SerializeField] private float BulletDistance = 12f;
    // Урон, наносимый пулей
    [SerializeField] private float _BulletDamage = 20f;
    // Стартовая позиция пули (привязана к позиции туррели Turret)
    public Transform BulletStartPosition;
    //Конечная позиция пули
    private Vector3 BulletTarget;
    //Анимация попадания через Paticle System объекта
    [SerializeField] private GameObject BulletExp;

    //Активация пули
    public void BulletActivate(bool activate)
    {
        if (activate)
        {
            gameObject.SetActive(true);
            //Начальная позиция пули
            transform.position = BulletStartPosition.position;
            //Конечная позиция пули (перевод из локальных координат в мировые)
            BulletTarget = BulletStartPosition.TransformPoint(Vector3.forward * BulletDistance);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float BulletDamage
    {
        get { return _BulletDamage; }
    }

    //Метод, задающий алгоритм передвижения пули
    public void BulletMove()
    {
        //Проверяем позицию поли относительно конечной с точностью до 0.1f
        if ((BulletTarget - transform.position).magnitude < 0.1f)
        {
            //Начальная позиция пули
            transform.position = BulletStartPosition.position;
            //Конечная позиция пули (перевод из локальных координат в мировые)
            BulletTarget = BulletStartPosition.TransformPoint(Vector3.forward * BulletDistance);
            GetComponent<MeshRenderer>().enabled = true;
        }
        //Изменить позицию пули методом MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, BulletTarget, Time.deltaTime * BulletSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Enemy")
        {
            //Скрываем пулю при попадании
            if ((!BulletExp.GetComponent<ParticleSystem>().isPlaying) & (GetComponent<MeshRenderer>().enabled))
            {
                BulletExp.transform.localPosition = transform.localPosition;
                BulletExp.GetComponent<ParticleSystem>().Play();
            }
                GetComponent<MeshRenderer>().enabled = false;
        }
    }
}