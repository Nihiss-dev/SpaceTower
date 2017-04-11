using System.Collections;
using System.Collections.Generic;
using Assets.Source.Audio.Conrtrolleur;
using JetBrains.Annotations;
using UnityEngine;
/*using Assets.Source.GameEntities;
using Assets.Source.Systems.Container;*/


namespace Assets.Source.Audio.Music {

    

    public class MusicEventManager : JsAudioBaseComponent {

        /*private IGameEntityContainer<ICreature> creatureContainer;
        private IAvatar avatar;

        [SerializeField]private AnimationCurve curve;
     
        public float sub;

        [SerializeField]private AudioClip music;
        [SerializeField]private GameObject musicplayer;
        private AudioSource source;
        private List<GameObject> MPlayers = new List<GameObject>();
        private ICreature closestCreature;
        public float distanceTo;

        private IGameEntityContainer<ICreature> CreatureContainer {
            get { return creatureContainer ?? (creatureContainer = GetComponentInScene<IGameEntityContainer<ICreature>>()); }
        }

        private IAvatar Avatar { get { return avatar ?? (avatar = GetComponentInScene<IAvatar>()); } }


        void Awake() {
            MPlayers.Add(Instantiate(musicplayer));
            source = MPlayers[0].GetComponent<AudioSource>();

        }
        void Update() {
            closestCreature = GetClosestIn(GetLargerCreatures());
            if(closestCreature == null) return;
            distanceTo = closestCreature.GetDistanceTo(avatar.Position);
            UpdateMusic(distanceTo);
        }

        private void UpdateMusic(float distance) {

            if (distanceTo < 30 && !source.isPlaying)
            {
                source.clip = music;
                source.loop = true;
                source.Play();

            }
            if (distanceTo < 40) {
                source.volume = curve.Evaluate(distanceTo / 40);
            }
            if (source.volume < 0.01) {
                source.Stop();
            }
        }

        [CanBeNull]
        protected ICreature GetClosestIn(IList<ICreature> creatures)
        {
            ICreature closest = null;

            foreach (var creature in creatures)
            {
                if (closest == null || Avatar.GetDistanceTo(creature) < Avatar.GetDistanceTo(closest))
                    closest = creature;
            }
            return closest;
        }

        private IList<ICreature> GetLargerCreatures()
        {
            var result = new List<ICreature>();

            foreach (var creature in CreatureContainer.GetChildren())
            {
                if (creature.IsLargerThan(Avatar))
                    result.Add(creature);
            }
            return result;
        }*/

       

    }

}