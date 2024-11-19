using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Controllers
{
    public class AudioController
    {
        public void PlayOneShot(AudioSource audioSource, AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    
        public void PlayOneShot(AudioSource audioSource, AudioClip clip, float pitch)
        {
            audioSource.pitch = pitch;
            PlayOneShot(audioSource, clip);
        }

        public void PlayRandomSound(AudioSource audioSource, List<AudioClip> clips)
        {
            ListTools.Shuffle(clips);
            PlayOneShot(audioSource, clips[0]);
        }

        public int PlayNextSound(AudioSource audioSource, List<AudioClip> clips, int currentClip)
        {
            if (currentClip == clips.Count) currentClip = 0;
            PlayOneShot(audioSource, clips[currentClip]);
            currentClip++;
            return currentClip;
        }

        public int PlayNextSound(AudioSource audioSource, List<AudioClip> clips, int currentClip, float pitch)
        {
            audioSource.pitch = pitch;
            return PlayNextSound(audioSource, clips, currentClip);
        }

        public void PlayMusic(AudioSource audioSource, AudioClip clip, bool loop, float volume)
        {
            audioSource.loop = loop;
            audioSource.volume = volume;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}