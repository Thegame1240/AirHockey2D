using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip PuckAudio, GoalAudio;


        public void PlayPuck()
        {
            AudioSource.PlayClipAtPoint(PuckAudio, Camera.main.transform.position, 0.3f);
        }

        public void PlayGoal()
        {
            AudioSource.PlayClipAtPoint(GoalAudio, Camera.main.transform.position, 0.3f);
        }
    }
}
