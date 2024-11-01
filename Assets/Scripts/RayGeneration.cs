using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class RayDrawer : VisualElement
{
    [SerializeField, DontCreateProperty] private float m_Radius;

    [UxmlAttribute, CreateProperty]
    public float Radius
    {
        get => m_Radius;
        set
        {
            m_Radius = value;
            MarkDirtyRepaint();
        }
    }

    [SerializeField, DontCreateProperty] private Vector2 m_MousePos;

    [UxmlAttribute, CreateProperty]
    public Vector2 MousePos
    {
        get => m_MousePos;
        set
        {
            m_MousePos = value;
            MarkDirtyRepaint();
        }
    }

    //public VectorImage VectorImage = new VectorImage();
    public float Width;
    public float Height;
    public float Top;
    public float Left;

    public float TopBack;
    public float LeftBack;

    public RayDrawer()
    {
        generateVisualContent += OnGenerateVisualContent;
    }
    
    public RayDrawer(float radius, Vector2 mousePos)
    {
        m_Radius = radius;
        m_MousePos = mousePos;
        
        generateVisualContent += OnGenerateVisualContent;
    }

    private void OnGenerateVisualContent(MeshGenerationContext ctx)
    {
        var painter = ctx.painter2D;
        painter.lineWidth = 2.0f;
        painter.strokeColor = new Color(156f / 255f, 66f / 255f, 44f / 255f);
        painter.fillColor = new Color(211f / 255f, 113f / 255f, 78f / 255f, 150f / 255f);

        painter.BeginPath();

        int startX = 160;
        int startY = 340;

        var p1 = new Vector2(startX, startY);
        var p2 = new Vector2(startX, startY - 100);
        //var p3 = new Vector2(Input.mousePosition.x, 1080 - Input.mousePosition.y);
        var p3 = new Vector2(m_MousePos.x, 1080 - m_MousePos.y);

        double angle = Math.Atan2(p3.y - p1.y, p3.x - p1.x) - Math.Atan2(p2.y - p1.y, p2.x - p1.x);
        if (angle < 0) angle += 2 * Math.PI;

        angle = 180 * angle / Math.PI;

        painter.MoveTo(p1);

        painter.Arc(p3, m_Radius, (float)angle + 180f, (float)angle);
        painter.ClosePath();

        painter.Fill();
        painter.Stroke();
        
        
        /*var painter2d = new Painter2D();
        painter2d.lineWidth = 2.0f;
        painter2d.strokeColor = new Color(156f / 255f, 66f / 255f, 44f / 255f);
        painter2d.fillColor = new Color(211f / 255f, 113f / 255f, 78f / 255f, 150f / 255f);

        painter2d.BeginPath();

        painter2d.MoveTo(p1);

        painter2d.Arc(p3, m_Radius, (float)angle + 180f, (float)angle);
        painter2d.ClosePath();

        painter2d.Fill();
        painter2d.Stroke();

        painter2d.SaveToVectorImage(VectorImage);*/
        
        Width = Math.Abs(m_MousePos.x - startX);
        if (m_MousePos.x - startX < 0)
        {
            Left = startX - Width;
        }
        else
        {
            Left = startX;
        }
        
        Height = Math.Abs(1080 - m_MousePos.y - startY);
        if (1080 - m_MousePos.y - startY < 0)
        {
            Top = startY - Height;
        }
        else
        {
            Top = startY;
        }

        TopBack = -Top;
        LeftBack = -Left;
    }
}

[RequireComponent(typeof(UIDocument))]
public class RayGeneration : MonoBehaviour
{
    public float radius = 50f;
    public Vector2 mousePos = Vector2.zero;
    
    public VectorImage vectorImage;
    public float width;
    public float height;
    public float top;
    public float left;

    public float widthBack = 1920;
    public float heightBack = 1080;
    public float topBack;
    public float leftBack;

    private VisualElement _root;
    private RayDrawer _currentRayDrawer;

    public void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _root.Q<RayDrawer>().dataSource = this;
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        
        //vectorImage = _root.Q<RayDrawer>().VectorImage;
        width = _root.Q<RayDrawer>().Width;
        height = _root.Q<RayDrawer>().Height;
        top = _root.Q<RayDrawer>().Top;
        left = _root.Q<RayDrawer>().Left;
        topBack = _root.Q<RayDrawer>().TopBack;
        leftBack = _root.Q<RayDrawer>().LeftBack;

        /*if (_currentRayDrawer != null)
        {
            _root.Remove(_currentRayDrawer); 
        }
        _currentRayDrawer = new RayDrawer(radius, mousePos);
        _root.Add(_currentRayDrawer);*/
    }
}