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
                    if (!PlayerPrefs.HasKey("First Launch"))
                    {
                        FindLevelInstaller(true);
                        PlayerPrefs.SetString("First Launch", "true");
                    }
                    FindLevelInstaller();
                    break;
                case "Tutorial":
                    FindTutorialInstaller();
                    break;
            }
        }

        private void FindLevelInstaller(bool isTutorial = false)
        {
            LevelInstaller levelInstaller = FindFirstObjectByType<LevelInstaller>();
            levelInstaller.Init(_audioController, _sceneSwitchController, isTutorial);

            _audioController.Init(levelInstaller.GetMusicSources());
        }
        
        private void FindTutorialInstaller()
        {
            TutorialInstaller tutorInstaller = FindFirstObjectByType<TutorialInstaller>();
            tutorInstaller.Init(_audioController, _sceneSwitchController);

            _audioController.Init(tutorInstaller.GetMusicSources());
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