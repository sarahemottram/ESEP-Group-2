using System;
using System.Collections;
using UnityEngine;

namespace PacMan
{
    public class SoundManager : MonoBehaviour
    {
        AudioSource start;
        AudioSource loop;
        internal AudioSource end;
        internal AudioSource death;
        AudioSource siren;
        AudioSource waka;
        internal AudioSource ghostDeath;

        private float songVolume = 0.5f;
        private float songWhileSirenVolume = 0.4f;

        void Start()
        {
            //grab all audio sources
            start = GameObject.Find("Start Music").GetComponent<AudioSource>();
            loop = GameObject.Find("Loop Music").GetComponent<AudioSource>();
            end = GameObject.Find("End Music").GetComponent <AudioSource>();
            death = GameObject.Find("Death Sound").GetComponent<AudioSource>();
            siren = GameObject.Find("Siren").GetComponent<AudioSource>();
            waka = GameObject.Find("Waka").GetComponent<AudioSource>();
            ghostDeath = GameObject.Find("Ghost Death").GetComponent<AudioSource>();

            //play beginning song and loop
            PlayLoop();
        }

        private IEnumerator StartLoop()
        {
            start.Play();
            waka.Play();
            waka.Pause();
            loop.Play();
            loop.Pause();
            while(start.isPlaying)
                yield return null;
            loop.UnPause();
            
        }

        internal void PlayLoop()
        {
            StartCoroutine(StartLoop());
        }

        internal void PlayEnd()
        {
            start.Stop();
            loop.Stop();
            siren.Stop();
            waka.Stop();
            end.Play();
        }

        internal void PlayDeath()
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
            while(siren.isPlaying)
                yield return null;
            start.volume = songVolume;
            loop.volume = songVolume;
        }

        internal void PlaySiren()
        {
            StartCoroutine(StartSiren());
        }

        internal void PlayWaka()
        {
            if (siren.isPlaying)
            {
                waka.Pause();
                return;
            }
            if(!waka.isPlaying)
                waka.UnPause();
        }

        internal void PauseWaka()
        {
            waka.Pause();
        }
    }
}
