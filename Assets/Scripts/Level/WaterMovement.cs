using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private Transform waterLevel;
    [SerializeField] private Transform upBorder;
    [SerializeField] private Transform downBorder;
    
    [SerializeField] private MeshFilter meshFilter;

    [Range(0,10f)][SerializeField] private float amplitude = 3f;
    [Range(1f,10f)][SerializeField] private float length = 3f;
    [Range(0,10f)][SerializeField] private float speed = 3f;
    
    private float[] _initialVertexPosZ;
    private float _heightStep;
    private float _offset = 0f;
    private void Awake()
    {
        int length = meshFilter.mesh.vertices.Length;

        _initialVertexPosZ = new float[length];
        
        Vector3[] vertices = meshFilter.mesh.vertices;
        
        for (int i = 0; i < length; i++)
        {
            _initialVertexPosZ[i] = vertices[i].z;
        }

        _heightStep = (upBorder.transform.position.y - downBorder.transform.position.y) / 10;
    }

    private void Update()
    {
        UpdateWaveHeight();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].z = _initialVertexPosZ[i] + GetWaveHeight( transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }

    private void UpdateWaveHeight()
    {
        _offset += Time.deltaTime * speed;
    }

    public Transform GetWaterLevel()
    {
        return waterLevel;
    }

    public void ChangeWaterLevel(float changeValue)
    {
        changeValue = Mathf.Clamp(changeValue, 0, 1f);
        
        waterLevel.transform.position = Vector3.Lerp(waterLevel.transform.position, new Vector3(0, 
            Mathf.Clamp(downBorder.position.y + _heightStep * changeValue * 10, downBorder.position.y , upBorder.position.y), 
            0f), Time.deltaTime); 
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + _offset);
    }
}
