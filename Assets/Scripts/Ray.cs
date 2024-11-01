using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Ray : MonoBehaviour
{
    public float radius = 50f;
    public Vector2 mousePos = Vector2.zero;
    
    private VisualElement _root;
    private RayDrawer _currentRayDrawer;
    
    public void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
    }
    
    private void Update()
    {
        mousePos = Input.mousePosition;
        
        if (_currentRayDrawer != null)
        {
            _root.Remove(_currentRayDrawer); 
        }
        _currentRayDrawer = new RayDrawer(radius, mousePos);
        _root.Add(_currentRayDrawer);
    }
}