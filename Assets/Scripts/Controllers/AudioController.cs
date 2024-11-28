using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Controllers
{
    public class AudioController
    {
        private List<AudioSource> _musicSources = new List<AudioSource>();
        
        private float _musicVolume = 1f;
        private float _soundVolume = 1f;

        public void Init(List<AudioSource> musicSources)
        {
            _musicSources.Clear();
            _musicSources = musicSources;
            SetMusicVolume(_musicVolume);
        }
        
        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            foreach (var musicSource in _musicSources)
            {
                musicSource.volume = _musicVolume;
            }
        }

        public void SetSoundVolume(float volume)
        {
            _soundVolume = volume;
        }
        
        public float GetSoundVolume()
        {
            return _soundVolume;
        }
        
        public float GetMusicVolume()
        {
            return _musicVolume; 
        }
        
        public void PlayOneShot(AudioSource audioSource, AudioClip clip)
        {
            audioSource.volume = _soundVolume;
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
            audioSource.volume = _musicVolume;
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void SetVolumeFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                SetMusicVolume(PlayerPrefs.GetFloat("Music"));
            }
            
            if (PlayerPrefs.HasKey("Sound"))
            {
                SetSoundVolume(PlayerPrefs.GetFloat("Sound"));
            }
        }

        public void SaveVolumeToPlayerPrefs()
        {
            PlayerPrefs.SetFloat("Music", _musicVolume);
            PlayerPrefs.SetFloat("Sound", _soundVolume);
        }
    }
}