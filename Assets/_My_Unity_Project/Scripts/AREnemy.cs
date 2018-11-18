using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для персонажа управления свойствами Enemy
public class AREnemy : MonoBehaviour {
    //Здоровье Enemy
    [SerializeField] private float EnemyHP = 100;
    //Текущее здоровье
    private float EnemyCurrentHP;
    //Скорость передвижения
    [SerializeField] private float EnemySpeed = 0.4f;

    //Событие смерти Enemy
    public Action<AREnemy> OnEnemyDie;

    //Активация Enemy
    public void EnemyActivate(bool activate)
    {
        if (activate)
        {
            //Начальная позиция в 0,0,0
            transform.localPosition = new Vector3(0,0,0);
            transform.localRotation = Quaternion.identity;
            SetEnemyAnimationClip("Idle");
            EnemyCurrentHP = EnemyHP;
        }
        else
        {
        }
    }

    //Состояние Enemy - живой?
    public bool isEnemyAlive
    {
        get { return (EnemyCurrentHP > 0); }
    }

    //Установить анимацию
    public void SetEnemyAnimationClip(string AnimationClipName)
    {
        if (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != AnimationClipName) { GetComponent<Animator>().SetTrigger(AnimationClipName); };
    }

    //Метод передвижения Enemy до цели toPosition
    public void EnemyMove(Vector3 toPosition)
    {
        //Повернуть в сторону цели
        transform.LookAt(toPosition);
        //Изменить анимацию на бег
        SetEnemyAnimationClip("Run");
        //Изменить позицию методом MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, toPosition, Time.deltaTime * EnemySpeed);
    }

    //Получение урона
    public void EnemyGetDamage (float Damage)
    {
        EnemyCurrentHP -= Damage;
        Debug.Log("Осталось здоровья: " + EnemyCurrentHP + " из " + EnemyHP);
        Debug.Log("Живой?: " + isEnemyAlive);
        if (!isEnemyAlive)
        {
            //Положить Enemy
            transform.LookAt(Vector3.down);
            //Установить анимацию Idle
            SetEnemyAnimationClip("Idle");
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        Debug.Log("Столкновение!");
        if (collider.gameObject.name == "Bullet")
        {
            EnemyGetDamage(20f);
        }
    }

    public void EnemyDie()
    {
        //OnEnemyDie?Invoke();
    }
}
