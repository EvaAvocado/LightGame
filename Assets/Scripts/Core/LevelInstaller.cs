using System.Collections.Generic;
using Controllers;
using Data;
using Objects;
using UI;
using UnityEngine;
using Ray = Playable.Ray;

namespace Core
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private List<InteractObject> _objects;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Ray _ray;
        [SerializeField] private GameConfig _config;
        [SerializeField] private List<AudioSource> _musicSources;
        
        private ScoreController _scoreController;

        public void Init(AudioController audioController)
        {
            _scoreController = new ScoreController();
        
            foreach (var obj in _objects)
            {
                obj.Init(audioController, _scoreController);
            }
        
            _scoreController.Init(_objects, _scoreView, _config.MaxScore);
            _ray.Init(_scoreController);
        }
        
        public List<AudioSource> GetMusicSources() => _musicSources;
    }
}