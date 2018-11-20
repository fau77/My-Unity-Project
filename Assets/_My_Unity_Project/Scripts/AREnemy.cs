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
    private bool EnemyAtacking = false;

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
            gameObject.SetActive(true);
            SetEnemyAnimationClip("Idle");
            EnemyCurrentHP = EnemyHP;
            EnemyAtacking = false;
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

    //Состояние Enemy - атакует?
    public bool isEnemyAtacking
    {
        get { return (EnemyAtacking); }
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

    //Атака
    public void EnemyAttack()
    {
        SetEnemyAnimationClip("RoundKick");
    }

    //Получение урона
    public void EnemyGetDamage (float Damage)
    {
        EnemyCurrentHP -= Damage;
        Debug.Log("Осталось здоровья: " + EnemyCurrentHP + " из " + EnemyHP);
        Debug.Log("Живой?: " + isEnemyAlive);
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Bullet")
        {
            EnemyGetDamage(20f);
            Debug.Log("Попадание!");
        }
        if (collider.gameObject.name == "Tower")
        {
            Debug.Log("Враг дошел до башни!");
            EnemyAtacking = true;
        }
    }

    public void EnemyDie()
    {
        //OnEnemyDie?Invoke();
    }
}
