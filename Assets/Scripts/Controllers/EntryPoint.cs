using System.Collections.Generic;
using Objects;
using UI;
using UnityEngine;
using Ray = UI.Ray;

namespace Controllers
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private List<InteractObject> _objects;
        [SerializeField] private Score _score;
        [SerializeField] private Ray _ray;
    
        private AudioController _audioController;

        private void Awake()
        {
            _audioController = new AudioController();
        
            foreach (var obj in _objects)
            {
                obj.Init(_audioController, _score);
            }
        
            _score.Init(_objects);
            _ray.Init(_score);
        }
    }
}