using System;
using Controllers;
using UnityEngine;

namespace Playable
{
    public class Ray : MonoBehaviour
    {
        [SerializeField] private float _xDelta = 3.93f;
        [SerializeField] private AnimationCurve _yCurve;
        [SerializeField] private bool _isCanScale;
        [SerializeField] private float _deltaScale = 0.08f;

        private readonly float _k1 = 0; // the first angular coefficient
        private float _k2; // the second angular coefficient
        private float _alpha;
        private float _delta;

        public void Init(ScoreController scoreController)
        {
            scoreController.OnAddScore += AddDelta;
        }
    
        private void Update()
        {
            Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);

            Rotate(mousePos);
            if (_isCanScale) Scale(mousePos);
        }

        private void Rotate(Vector3 mousePos)
        {
            _k2 = (mousePos.y - transform.position.y) / (mousePos.x - transform.position.x); // calculating the second angular coefficient

            _alpha = (float)Math.Atan((_k1 - _k2) / (1 + _k1 * _k2)); // the angle between two straight lines in radians


            if (mousePos.y - transform.position.y < 0)
            {
                _alpha -= (float)Math.PI; // second and third quarters
            }

            if (mousePos.y - transform.position.y < 0 && mousePos.x - transform.position.x > 0 ||
                mousePos.y - transform.position.y > 0 &&
                mousePos.x - transform.position.x < 0) // swap the second and fourth quarters
            {
                _alpha += (float)Math.PI;
            }

            _alpha = -180 * _alpha / (float)Math.PI; // conversion from radians to degrees

            transform.rotation = Quaternion.Euler(0, 0, _alpha);
        }

        private void Scale(Vector3 mousePos)
        {
            var r1 = Math.Abs(_xDelta - transform.position.x);
            var r2 = Math.Abs(Math.Sqrt(Math.Pow(transform.position.x - mousePos.x, 2) +
                                        Math.Pow(transform.position.y - mousePos.y, 2)));

            var newScaleX = (float)r2 / r1;
        
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

            Resize((float)r2);
        }

        private void Resize(float dist)
        {
            var newScaleY = _yCurve.Evaluate(dist);
        
            transform.localScale = new Vector3(transform.localScale.x, newScaleY + _delta, transform.localScale.z);
        }

        private void AddDelta()
        {
            _delta += _deltaScale;
        }
    }
}