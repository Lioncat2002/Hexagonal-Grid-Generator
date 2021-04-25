using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexmesh;
    List<Vector3> vertices;
    List<int> triangles;
    MeshCollider meshCollider;
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexmesh = new Mesh();
        hexmesh.name = "Hex Mesh";
        meshCollider = gameObject.AddComponent<MeshCollider>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Triangulate(HexCell[] cells)
    {
        hexmesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for(int i=0;i<cells.Length;i++)
        {
            Triangulate(cells[i]);
        }
        hexmesh.vertices = vertices.ToArray();
        hexmesh.triangles = triangles.ToArray();
        hexmesh.RecalculateNormals();
        meshCollider.sharedMesh = hexmesh;
    }
    void AddTriangles(Vector3 v1,Vector3 v2,Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
    private void Triangulate(HexCell hexCell)
    {
        Vector3 center = hexCell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangles(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i+1]);
        }
    }
}
