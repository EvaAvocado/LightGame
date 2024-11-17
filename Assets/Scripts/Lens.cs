using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField] private Transform _ray;
    [SerializeField] private AnimationCurve _curve;

    private bool _isMouseIn;
    
    private void OnMouseEnter()
    {
        _isMouseIn = true;
        _ray.gameObject.SetActive(true);
        _ray.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnMouseExit()
    {
        _isMouseIn = false;
        _ray.gameObject.SetActive(false);
        _ray.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (_isMouseIn)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var alpha = _curve.Evaluate(mousePos.y);
            _ray.localRotation = Quaternion.Euler(0, 0, alpha);
        }
    }
}
