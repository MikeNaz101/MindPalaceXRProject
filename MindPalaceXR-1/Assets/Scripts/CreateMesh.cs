using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    void Start()
    {
        // Create a new mesh
        Mesh mesh = new Mesh();

        // Define the vertices (corners of the quad)
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0), // Bottom left
            new Vector3(1, 0, 0), // Bottom right
            new Vector3(0, 1, 0), // Top left
            new Vector3(1, 1, 0)  // Top right
        };

        // Define the triangles (connect the vertices to form faces)
        int[] triangles = new int[6]
        {
            0, 2, 1, // First triangle
            2, 3, 1  // Second triangle
        };

        // Set the mesh data
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals (important for lighting)
        mesh.RecalculateNormals();

        // Assign the mesh to a MeshFilter component
        GetComponent<MeshFilter>().mesh = mesh;

        // Optionally add a MeshRenderer and Material to make it visible
    }
}