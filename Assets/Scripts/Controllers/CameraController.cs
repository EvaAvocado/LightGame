using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class CameraController
    {
        public void MoveCamera(Camera camera, Vector3 newPosition, float duration, TweenCallback endAction)
        {
            camera.transform.DOMove(newPosition, duration).OnComplete(endAction);
        }
        
        public void ScaleCamera(Camera camera, float newScale, float duration, TweenCallback endAction)
        {
            camera.DOOrthoSize(newScale, duration).OnComplete(endAction);
        }
        
        public void MoveCamera(Camera camera, Vector3 newPosition, float duration)
        {
            MoveCamera(camera, newPosition, duration, (() => {}));
        }
        
        public void ScaleCamera(Camera camera, float newScale, float duration)
        {
            ScaleCamera(camera, newScale, duration, (() => {}));
        }
    }
}
