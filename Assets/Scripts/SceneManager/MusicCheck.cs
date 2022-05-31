using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Audio
{
    public class MusicCheck : MonoBehaviour
    {
        [SerializeField] GameObject musicPlayerPrefab;

        private void Awake()
        {
            if (FindObjectOfType<MusicPlayer>()) return;
            else Instantiate(musicPlayerPrefab, transform.position, transform.rotation);
        }
    }
}

