using System;
using UnityEngine;

public class RectOverlapConsequenceVisualizer : MonoBehaviour
{
    [SerializeField] private VisualizerData _visualizerData;
    
    private Vector3 _center;
    private Vector3 _halfExtents;
    private Quaternion _rotation;
    
    public void Initialize(Vector3 center, Vector3 halfExtents, Quaternion rotation)
    {   
        _center = center;
        _halfExtents = halfExtents;
        _rotation = rotation;
    }
    
    private void OnRenderObject()
    {
        if (_visualizerData.LineMaterials == null)
            return;
    
        _visualizerData.LineMaterials.SetPass(0);
        
        GL.PushMatrix();
        
        Matrix4x4 matrix = Matrix4x4.TRS(_center, _rotation, Vector3.one);
        GL.MultMatrix(matrix);
        
        GL.Begin(GL.LINES);
        GL.Color(_visualizerData.Color);
    
        
        Vector3[] corners = Utility.CalculateCorners(Vector3.zero, _halfExtents);
        int[,] edges = Utility.GetBoxEdges();
        
    
        // Draw each edge.
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            GL.Vertex(corners[edges[i, 0]]);
            GL.Vertex(corners[edges[i, 1]]);
        }
    
        GL.End();
        
        GL.PopMatrix();
        
        Destroy(gameObject,0.35f);
    }
}