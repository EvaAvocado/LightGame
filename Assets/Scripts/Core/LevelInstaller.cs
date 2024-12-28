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
        [SerializeField] private List<AchievementView> _achievements;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Ray _ray;
        [SerializeField] private GameConfig _config;
        [SerializeField] private List<AudioSource> _musicSources;
        [SerializeField] private GameObject _tutorialPanel;
        
        private ScoreController _scoreController;
        private SceneSwitchController _sceneSwitchController;

        public void Init(AudioController audioController, SceneSwitchController sceneSwitchController, bool isTutorial)
        {
            _scoreController = new ScoreController();
            _sceneSwitchController = sceneSwitchController;
        
            foreach (var obj in _objects)
            {
                obj.Init(audioController, _scoreController, _sceneSwitchController, _config);
            }

            foreach (var achievement in _achievements)
            {
                achievement.Init(audioController);
            }
        
            _scoreController.Init(_objects, _scoreView, _config.MaxScore);
            _ray.Init(_scoreController);
            
            if (isTutorial)
            {
                _tutorialPanel.SetActive(true);
                _ray.gameObject.SetActive(false);
                foreach (var obj in _objects)
                {
                    obj.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        
        public void NotOpenTutorialScene()
        {
            _sceneSwitchController.SwitchScene("Game");
        }
        
        public void OpenTutorialScene()
        {
            _sceneSwitchController.SwitchScene("Tutorial");
        }
        
        public List<AudioSource> GetMusicSources() => _musicSources;
    }
}