using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        MusicPlayer instance;
        [SerializeField] AudioSource audioSource;
        [SerializeField] GameObject musicPlayerPrefab;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(musicPlayerPrefab);
            }
            else
            {
                Destroy(musicPlayerPrefab);
            }
            audioSource = GetComponent<AudioSource>();
            //add in the future for Master Volume
            //audiouSource.volume = PlayerPrefsController.GetMasterVolume();
        }
        
        //public void SetVolume(float volume)
        //{
        //    audioSource.volume = volume;
        //}

        public void ChangeAudio(AudioClip music)
        {
            if (audioSource.clip.name == music.name) return;
            audioSource.Stop();
            audioSource.clip = music;
            audioSource.Play();
        }

        public void StopMusic()
        {
            audioSource.Stop();
            Destroy(gameObject);
        }
    }
}

