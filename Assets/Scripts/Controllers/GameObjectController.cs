using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class GameObjectController
    {
        public void MoveObject(GameObject gameObject, Vector3 newPosition, float duration, TweenCallback endAction, Ease ease)
        {
            gameObject.transform.DOMove(newPosition, duration).SetEase(ease).OnComplete(endAction);
        }
        
        public void ScaleObject(GameObject gameObject, Vector3 newScale, float duration, TweenCallback endAction, Ease ease)
        {
            gameObject.transform.DOScale(newScale, duration).SetEase(ease).OnComplete(endAction);
        }
        
        public void MoveObject(GameObject gameObject, Vector3 newPosition, float duration, Ease ease)
        {
            MoveObject(gameObject, newPosition, duration, () => {}, ease);
        }
        
        public void ScaleObject(GameObject gameObject, Vector3 newScale, float duration, Ease ease)
        {
            ScaleObject(gameObject, newScale, duration, () => {}, ease);
        }
    }
}