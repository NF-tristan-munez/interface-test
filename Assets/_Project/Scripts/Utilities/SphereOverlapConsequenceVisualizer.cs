using UnityEngine;

public class SphereOverlapConsequenceVisualizer : MonoBehaviour
{
    [SerializeField] private VisualizerData _visualizerData;
    private const int SEGMENTS = 36;
    
    private Vector3 _center;
    private float _radius;
    private Quaternion _rotation;
    
    public void Initialize(Vector3 center, float radius, Quaternion rotation)
    {
        _center = center;
        _radius = radius;
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

        // Draw three circles approximating the sphere.
        DrawCircle(Vector3.zero, Vector3.up, _radius, SEGMENTS);     // Circle in the XZ plane.
        DrawCircle(Vector3.zero, Vector3.right, _radius, SEGMENTS);    // Circle in the YZ plane.
        DrawCircle(Vector3.zero, Vector3.forward, _radius, SEGMENTS);  // Circle in the XY plane.

        GL.End();
        GL.PopMatrix();

        // Destroy the visualizer after a short delay.
        Destroy(gameObject, 0.35f);
    }

  
    private void DrawCircle(Vector3 center, Vector3 normal, float radius, int segments)
    {
        // Create a rotation that aligns Vector3.up with the desired normal.
        Quaternion alignRotation = Quaternion.FromToRotation(Vector3.up, normal);
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + alignRotation * (Quaternion.Euler(0, 0, 0) * new Vector3(0, radius, 0));

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleStep * i;
            Vector3 newPoint = center + alignRotation * (Quaternion.Euler(0, 0, angle) * new Vector3(0, radius, 0));
            GL.Vertex(prevPoint);
            GL.Vertex(newPoint);
            prevPoint = newPoint;
        }
    }
}