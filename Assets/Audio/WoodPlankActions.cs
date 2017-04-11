using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Shared;
using Assets.Source.Systems.Messaging;
using Assets.Systems.Messaging.Messages;
using UnityEngine;


namespace Assets.Audio
{
    public class WoodPlankActions: BaseComponent
    {
        private MessageDispatcher dispatcher;
        private LevelManager mainManager;
        private bool calledCannon = false;
        private bool IsWasShot =false;
        public bool isWasShot{get { return IsWasShot; } set { IsWasShot = value; } }

        void Awake() {
            mainManager = LevelManager.getInstance();
            dispatcher = GetComponent<MessageDispatcher>();
        }
        // Use this for initialization
        void Start() {
            if (isWasShot) {
                calledCannon = true;
            }
            
        }

        void Update() {
            if (calledCannon) {
                
                dispatcher.Dispatch(new CannonFallMessage());
            }
        }

        void OnCollisionEnter2D(Collision2D other) {
            calledCannon = false;
            if (other.gameObject.layer == mainManager.layerMask["Wood"]) {
                WoodOnWoodAction();
            }
            else if (other.gameObject.tag == "Floor") {
                WoodOnGroundAction();
            }
        }
        public void WoodOnWoodAction()
        {
            dispatcher.Dispatch(new WoodImpactMessage("Wood"));
        }
        public void WoodOnGroundAction()
        {
            dispatcher.Dispatch(new WoodImpactMessage("Floor"));
        }
    }
}
