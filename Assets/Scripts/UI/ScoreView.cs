using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        public void Init(int maxPoints)
        {
            _text.text = "0/" + maxPoints;
        }
        
        public void SetNewPointsInText(int points, int maxPoints)
        {
            _text.text = points + "/" + maxPoints;
        }
    }
}
