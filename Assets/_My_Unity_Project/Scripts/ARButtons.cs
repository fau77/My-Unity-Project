using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace ARTowers.AR
{
    public class ARButtons : MonoBehaviour
    {
        //Объект класса AREnemy
        public AREnemy Enemy;
        //Объект класса ARbullet
        public ARBullet Bullet;
        //Объект надписи с ошибкой
        public GameObject MenuText;
        //Объект текста кнопки
        public GameObject ButtonStartStop;
        //Признак состояния игры
        [HideInInspector]
        public static bool StartGame = false;
        [SerializeField] private GameObject Win;
        [SerializeField] private GameObject Lose;

        [SerializeField] public GameTargets[] sf_Targets;
        [Serializable]
        public struct GameTargets
        {
            public string Name;
            public GameObject Target;
        }

        public void ARStart()
        {
            if (StartGame)
            {
                ///Устанавливаем признак - игры остановлена
                StartGame = false;
                //Деактивируем пулю
                Bullet.BulletActivate(false);
                //Активация Enemy
                Enemy.EnemyActivate(true);
                //Изменяем надпись кнопки на "старт".
                ButtonStartStop.GetComponentInChildren<Text>().text = "СТАРТ";
                Win.SetActive(false);
                Lose.SetActive(false);

            }
            else
            {
                //Массив (точнее HashSet), содержащий набор имен необходимых на поле объектов
                HashSet<string> hsTargetNamesMust = new HashSet<string>();
                //Заполняем его требуемыми именами
                foreach (var el in sf_Targets)
                {
                    hsTargetNamesMust.Add(el.Name);
                }
                //Массив определившихся на поле объектов
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
                    ///Устанавливаем признак старта игры
                    StartGame = true;
                    //Активация Enemy
                    Enemy.EnemyActivate(true);
                    //Активируем пулю
                    Bullet.BulletActivate(true);
                    //Изменяем надпись кнопки старт на "стоп".
                    ButtonStartStop.GetComponentInChildren<Text>().text = "СТОП";
                    Win.SetActive(false);
                    Lose.SetActive(false);

                }
                hsTargetNamesMust.Clear();
                hsTargetNamesHave.Clear();
            }
        }

        public void ARExit()
        {
            Application.Quit();
        }

    }
}