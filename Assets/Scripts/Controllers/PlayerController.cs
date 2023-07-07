using System;
using System.Net.Http.Headers;
using Assets.Scripts.Model;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Controllers
{
    public class EventTriggerHelper
    {
        public static void AddListenerToEventTrigger(EventTrigger eventTrigger, EventTriggerType eventTriggerType, Action action)
        {
            var entry = new EventTrigger.Entry
                {callback = new EventTrigger.TriggerEvent(), eventID = eventTriggerType};
            entry.callback.AddListener(eventData => action());
            eventTrigger.triggers.Add(entry);
        }
    }

    public class PlayerController : MonoBehaviour {

        private World _world;

        public bool UseMouse;
        public EventTrigger LeftTrigger;
        public EventTrigger RightTrigger;

        /*
   public GameObject UIJumps;


   public GameObject[] UIdisableOnEnd;

   public Text UIStarsScore;
   public Text UIDistanceScore;

   public GameObject GameOverScreen;
   public GameObject FinishLevelScreen;

   public Text UIStarsGameOver;
   public Text UIDistanceGameOver;

   public Slider UIDistanceTargetSlider;
   */

        public PlayerComponent PlayerComponent { get; set; }

        void Awake(){

            _world = World.GetInstance();

        }

        // Use this for initialization
        public void Initialize(PlayerComponent playerComponent, Action<int, int> updateJumpsAction,
            Action onPlayerDeathAction)
        {

            PlayerComponent = playerComponent;

            PlayerComponent.Initialize(updateJumpsAction, onPlayerDeathAction);
           

            if (UseMouse)
                return;

            EventTriggerHelper.AddListenerToEventTrigger(LeftTrigger, EventTriggerType.PointerDown, LeftButtonTouch);
            EventTriggerHelper.AddListenerToEventTrigger(RightTrigger, EventTriggerType.PointerDown, RightButtonTouch);


        }


        private void LeftButtonTouch()
        {
            PlayerComponent.PlayerJump();
        }

        private void RightButtonTouch()
        {
            PlayerComponent.PlayerSlam();

        }

        // Update is called once per frame
        void Update () {
		    
            if (UseMouse)
                HandleMouseButtons();
        }

        public void HandleMouseButtons()
        {

            if (PlayerComponent == null)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                LeftButtonTouch();
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                RightButtonTouch();
                return;
            }
            
        }


        
    }
}
