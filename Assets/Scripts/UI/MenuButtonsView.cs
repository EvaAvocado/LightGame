using Controllers;
using UnityEngine;

namespace UI
{
    public class MenuButtonsView : MonoBehaviour
    {
        private SceneSwitchController _sceneSwitchController;

        public void Init(SceneSwitchController sceneSwitchController)
        {
            _sceneSwitchController = sceneSwitchController;
        }
        
        public void LoadGame()
        {
            _sceneSwitchController.SwitchScene("Game");
        }

        public void LoadTutorial()
        {
            _sceneSwitchController.SwitchScene("Tutorial");
        }
    }
}