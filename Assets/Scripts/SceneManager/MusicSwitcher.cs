using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Audio
{
    public class MusicSwitcher : MonoBehaviour
    {
        [SerializeField] AudioClip newTrack;
        private MusicPlayer musicPlayer;

        private void Awake()
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();
            if(newTrack != null)
            {
                musicPlayer.ChangeAudio(newTrack);
            }
        }
        
    }
}

