using System.Collections;
using System.Collections.Generic;
using Assets.Audio;
using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Triggers;
using Assets.Source.Systems.Messaging;
using Assets.Systems.Messaging.Messages;
using UnityEngine;

public class PlancheAudioTrigger : AbstractAudioTrigger {

    private JsAudioSource audioSource;
    private JsAudioSound aSound;
    private JsAudioSound soundSet;
    private bool isCannonShot = false;

    protected override JsAudioSource source { get { return audioSource; } }

    protected override JsAudioSound sound { get { return aSound; } }

    protected override MessageDispatcher MessageDispatcher { get { return GetComponentInParent<MessageDispatcher>(); } }

    void Awake() {
        MessageDispatcher.AddHandler<WoodImpactMessage>(OnWoodImpact);
        MessageDispatcher.AddHandler<CannonFallMessage>(OnCannonBallExist);
        soundSet = GetComponent<JsAudioSound>();
        audioSource = GetComponent<JsAudioSource>();
    }

    

    // Use this for initialization
    void Update() {
        if (isCannonShot) {
            if (audioSource.Pitch > 0) {
                audioSource.Pitch -= 0.01f;
            }
            
        }
		
    }

    private void OnWoodImpact(WoodImpactMessage message) {
        isCannonShot = false;
        if (message.tag == "Wood") {
            source.Play(soundSet, "WoodOnWood");
        }
        else if(message.tag == "Floor") {
            source.Play(soundSet, "WoodOnGround");
        }
    }
    private void OnCannonBallExist(CannonFallMessage message)
    {
        
        audioSource.Play(soundSet, "Projectile");
        isCannonShot = true;
    }

}
