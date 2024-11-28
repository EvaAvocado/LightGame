using System;
using Controllers;
using UnityEngine;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        private AudioController _audioController;
        private SceneSwitchController _sceneSwitchController;

        private void Awake()
        {
            _audioController = new AudioController();
            _sceneSwitchController = new SceneSwitchController();

            _sceneSwitchController.OnLoadScene += CheckLoadedScene;

            _audioController.SetVolumeFromPlayerPrefs();
            OpenMenu();
            DontDestroyOnLoad(gameObject);
        }

        private void CheckLoadedScene(string sceneName)
        {
            switch (sceneName)
            {
                case "Menu":
                    OpenMenu();
                    break;
                case "Game":
                    FindLevelInstaller();
                    break;
                case "Tutorial":
                    print("Tutorial");
                    break;
            }
        }

        private void FindLevelInstaller()
        {
            LevelInstaller levelInstaller = FindFirstObjectByType<LevelInstaller>();
            levelInstaller.Init(_audioController);

            _audioController.Init(levelInstaller.GetMusicSources());
        }

        private void OpenMenu()
        {
            MenuInstaller menuInstaller = FindFirstObjectByType<MenuInstaller>();
            menuInstaller.Init(_audioController, _sceneSwitchController);
            
            _audioController.Init(menuInstaller.GetMusicSources());
        }

        private void OnDisable()
        {
            _audioController.SaveVolumeToPlayerPrefs();
        }
    }
}