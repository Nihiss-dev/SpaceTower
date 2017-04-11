using System.Collections;
using System.Collections.Generic;
using Assets.Audio;
using Assets.Shared;
using Assets.Source.Systems.Messaging;
using Assets.Systems.Messaging.Messages;
using UnityEngine;

public class RocksAction : BaseComponent {

    private MessageDispatcher dispatcher;
    private bool calledCannon;
    private LevelManager mainManager;

    void Awake() {
        dispatcher = GetComponent<MessageDispatcher>();
        mainManager =LevelManager.getInstance();
    }

    void Start() {
        if (isWasShot) {
            calledCannon = true;
        }

    }

    public bool isWasShot { get { return calledCannon; } set { calledCannon = value; } }

    void Update() {
        if (calledCannon) {

            dispatcher.Dispatch(new CannonFallMessage());
        }

    }

    void OnCollisionEnter2D(Collision2D other) {
        calledCannon = false;
        RockImpactAction();
        if (other.gameObject.layer == mainManager.layerMask["Wood"])
        {
            other.gameObject.GetComponent<WoodPlankActions>().WoodOnWoodAction();
        }
    }



    public void RockImpactAction() {
            dispatcher.Dispatch(new RockImpactMessage());
        }
    }


