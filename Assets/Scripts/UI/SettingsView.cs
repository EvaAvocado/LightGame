using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        private AudioController _audioController;

        public void Init(AudioController audioController)
        {
            _audioController = audioController;
            _soundSlider.value = _audioController.GetSoundVolume();
            _musicSlider.value = _audioController.GetMusicVolume();
        }
        
        public void SetSoundVolume(float volume)
        {
            _audioController.SetSoundVolume(volume);
            
        }
        
        public void SetMusicVolume(float volume)
        {
            _audioController.SetMusicVolume(volume);
        }
    }
}