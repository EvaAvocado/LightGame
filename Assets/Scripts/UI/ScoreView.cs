using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetNewPointsInText(int points)
        {
            _text.text = points + "/10";
        }
    }
}
