using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PacMan
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        AudioSource start;
        [SerializeField]
        private AudioClip startClip;

        AudioSource loop;
        [SerializeField]
        private AudioClip loopClip;

        private AudioSource end;
        [SerializeField]
        private AudioClip endClip;

        private AudioSource death;
        [SerializeField]
        private AudioClip deathClip;

        AudioSource siren;
        [SerializeField]
        private AudioClip sirenClip;

        AudioSource waka;
        [SerializeField]
        private AudioClip wakaClip;

        private AudioSource ghostDeath;
        [SerializeField]
        private AudioClip ghostDeathClip;

        private float songVolume = 0.5f;
        private float songWhileSirenVolume = 0.4f;

        public float DeathClipLength { get; private set; }
        public float EndClipLength { get; private set; }

        void Start()
        {
            start = AddWithSettings(startClip, 0.7f, false);
            loop = AddWithSettings(loopClip, 0.5f, true);
            end = AddWithSettings(endClip, 0.6f, false);
            death = AddWithSettings(deathClip, 0.8f, false);
            siren = AddWithSettings(sirenClip, 0.8f, false);
            waka = AddWithSettings(wakaClip, 1f, true);
            ghostDeath = AddWithSettings(ghostDeathClip, 1f, false);

            DeathClipLength = deathClip != null ? deathClip.length : 1f;
            EndClipLength = endClip != null ? endClip.length : 1f;

            //play beginning song and loop
            PlayLoop();
        }

        private AudioSource AddWithSettings(AudioClip clip, float volume, bool loop)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.clip = clip;
            return audioSource;
        }

        private IEnumerator StartLoop()
        {
            start.Play();
            waka.Play();
            waka.Pause();
            loop.Play();
            loop.Pause();
            while (start.isPlaying)
                yield return null;
            loop.UnPause();

        }

        private void PlayLoop()
        {
            StartCoroutine(StartLoop());
        }

        private void PlayEnd()
        {
            start.Stop();
            loop.Stop();
            siren.Stop();
            waka.Stop();
            end.Play();
        }

        private void PlayDeath()
        {
            start.Stop();
            loop.Stop();
            siren.Stop();
            waka.Stop();
            death.Play();
        }

        private IEnumerator StartSiren()
        {
            start.volume = songWhileSirenVolume;
            loop.volume = songWhileSirenVolume;
            siren.Play();
            while (siren.isPlaying)
                yield return null;
            start.volume = songVolume;
            loop.volume = songVolume;
        }

        private void PlaySiren()
        {
            StartCoroutine(StartSiren());
        }

        private void PlayWaka()
        {
            if (siren.isPlaying)
            {
                waka.Pause();
                return;
            }
            if (!waka.isPlaying)
                waka.UnPause();
        }

        private void PauseWaka()
        {
            waka.Pause();
        }
        public void SubscribePellet(PelletController controller)
        {
            controller.OnPowerPellet += PlaySiren;
            controller.OnWaka += waka =>
            {
                if (waka)
                    PlayWaka();
                else
                    PauseWaka();
            };
        }

        internal void SubcribeFailState(FailState failState)
        {
            failState.OnDeath += PlayDeath;
            failState.OnReset += PlayLoop;
        }

        internal void SubscribeWin(WinState winState)
        {
            winState.OnWin += PlayEnd;
        }

        internal void SubscribeGhostDeath(GhostLogic ghostLogic)
        {
            ghostLogic.OnGhostDeath += () => ghostDeath.Play();
        }
    }
}
