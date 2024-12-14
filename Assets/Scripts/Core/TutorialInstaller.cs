using System.Collections.Generic;
using Controllers;
using Data;
using Objects;
using UnityEngine;
using Ray = Playable.Ray;

namespace Core
{
    public class TutorialInstaller : MonoBehaviour
    {
        [SerializeField] private List<InteractObject> _objects;
        [SerializeField] TutorialController _tutorialController;
        [SerializeField] private GameConfig _config;
        [SerializeField] private Ray _ray;
        [SerializeField] private List<AudioSource> _musicSources;
        
        private SceneSwitchController _sceneSwitchController;
        private ScoreController _scoreController;
        
        public void Init(AudioController audioController, SceneSwitchController sceneSwitchController)
        {
            _scoreController = new ScoreController();
            _sceneSwitchController = sceneSwitchController;
        
            foreach (var obj in _objects)
            {
                obj.Init(audioController, _scoreController, _sceneSwitchController);
            }
            
            _scoreController.Init(_objects, _config.MaxScore);
            _tutorialController.Init(_sceneSwitchController, audioController);
            _ray.Init(_scoreController);
        }
        
        public List<AudioSource> GetMusicSources() => _musicSources;
    }
}