using UnityEngine;

namespace Objects
{
    public class LensObject : InteractObject
    {
        [SerializeField] private Transform _ray;
        [SerializeField] private AnimationCurve _curve;

        private bool _isMouseIn;

        protected override void OnEnter()
        {
            _isMouseIn = true;
            _ray.gameObject.SetActive(true);
            _ray.GetComponent<BoxCollider2D>().enabled = true;
        }

        protected override void OnExit()
        {
            _isMouseIn = false;
            _ray.gameObject.SetActive(false);
            _ray.GetComponent<BoxCollider2D>().enabled = false;
        }

        private void Update()
        {
            if (_isMouseIn)
            {
                Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var alpha = _curve.Evaluate(mousePos.y);
                _ray.localRotation = Quaternion.Euler(0, 0, alpha);
            }
        }
    }
}
