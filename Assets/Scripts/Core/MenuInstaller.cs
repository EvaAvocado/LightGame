using System.Collections.Generic;
using Controllers;
using UI;
using UnityEngine;

namespace Core
{
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private MenuButtonsView _menuButtonsView;
        [SerializeField] private List<AudioSource> _musicSources;
        
        public void Init(AudioController audioController, SceneSwitchController sceneSwitchController)
        {
            _settingsView.Init(audioController);
            _menuButtonsView.Init(sceneSwitchController);
        }
        
        public List<AudioSource> GetMusicSources() => _musicSources;
    }
}