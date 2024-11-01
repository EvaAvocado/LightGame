using System;
using UnityEngine;

public class RayMesh : MonoBehaviour
{
    private void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(-300, -300);
        vertices[1] = new Vector3(-300, 100);
        vertices[2] = new Vector3(100, 100);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;

    }

    
}