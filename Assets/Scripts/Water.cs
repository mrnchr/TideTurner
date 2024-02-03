using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform waterLevel;
    [SerializeField] private Transform upBorder;
    [SerializeField] private Transform downBorder;

    [SerializeField] private float amplitude = 3f;
    [SerializeField] private float length = 3f;
    [SerializeField] private float speed = 3f;
    
    [SerializeField] private float offset = 0f;
    
    private MeshFilter _meshFilter;
    private float[] _initialVertexPosZ;
    private float _heightStep;
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

        _heightStep = (upBorder.transform.position.y - downBorder.transform.position.y) / 10;
    }

    //private float g = 1;
    private void Update()
    {
        UpdateWaveHeight();
        UpdateMesh();

        //g = Mathf.Clamp(g + Input.mouseScrollDelta.y / 10, 0, 1f);
        //ChangeWaterLevel(g);
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
        Debug.Log(changeValue);

        changeValue = Mathf.Clamp(changeValue, 0, 1f);
        
        waterLevel.transform.position = Vector3.Lerp(waterLevel.transform.position, new Vector3(0, 
            Mathf.Clamp(downBorder.position.y + _heightStep * changeValue * 10, downBorder.position.y , upBorder.position.y), 
            0f), Time.deltaTime); 
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
