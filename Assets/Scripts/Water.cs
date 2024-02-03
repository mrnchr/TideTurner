using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform waterLevel;

    [SerializeField] private float amplitute = 3f;
    [SerializeField] private float length = 3f;
    [SerializeField] private float speed = 3f;
    
    [SerializeField] private float offset = 0f;
    
    private MeshFilter _meshFilter;

    private float[] _initialVertexPosZ;
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();

        int length = _meshFilter.mesh.vertices.Length;

        _initialVertexPosZ = new float[length];
        
        Vector3[] vertices = _meshFilter.mesh.vertices;
        
        for (int i = 0; i < length; i++)
        {
            _initialVertexPosZ[i] = vertices[i].z;
        }
    }

    private void Update()
    {
        UpdateWaveHeight();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].z = _initialVertexPosZ[i] + GetWaveHeight( transform.position.x + vertices[i].x); //transform.position.x
        }

        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.RecalculateNormals();
    }

    private void UpdateWaveHeight()
    {
        offset += Time.deltaTime * speed;
    }

    public Transform GetWaterLevel()
    {
        return waterLevel;
    }

    public void ChangeWaterLevel(float changeValue)
    {
        waterLevel.transform.position = new Vector3(0, waterLevel.transform.position.y + changeValue, 0f);
    }

    public float GetWaveHeight(float x)
    {
        return amplitute * Mathf.Sin(x / length + offset);
    }
}
