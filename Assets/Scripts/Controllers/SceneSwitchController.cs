using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneSwitchController
    {
        private string _sceneName; 
        
        public Action<string> OnLoadScene;
        
        public void SwitchScene(string sceneName)
        {
            _sceneName = sceneName;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
            asyncOperation.completed += OnSceneLoaded;
        }
        
        private void OnSceneLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                OnLoadScene?.Invoke(_sceneName);
            }
        }
    }
}