using Assets.Source.Audio.Conrtrolleur;
using UnityEngine;

public class MusAmbAudioTrigger : MonoBehaviour {

    private JsAudioSource aSource;
    private JsAudioSource otherSource;
    private JsAudioSound soundSet;
    private bool playingAmbiance = false;

    void Awake() {
        aSource = GetComponent<JsAudioSource>();
        otherSource = GetComponent<Transform>().gameObject.AddComponent<JsAudioSource>();
        soundSet = GetComponent<JsAudioSound>();
        foreach (string key in soundSet.SoundSetMixers.Keys) {
            Debug.Log(key);
        }
        
    }
    // Use this for initialization
    void Update() {
        if (!playingAmbiance) {
            playingAmbiance = true;
            aSource.Play(soundSet, "Ambiance");
            otherSource.Play(soundSet,"Music");
        }
		
	}
}
