using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private Transform waterLevel;
    [SerializeField] private Transform upBorder;
    [SerializeField] private Transform downBorder;
    
    [SerializeField] private MeshFilter waterGraphic;


    [Header("Wave settings")]
    [Range(0,10f)][SerializeField] private float amplitude = 3f;
    [Range(1f,10f)][SerializeField] private float length = 3f;
    [Range(0,10f)][SerializeField] private float speed = 3f;
    [Header("Water level settings")]
    [Range(0,5f)][SerializeField] private float minWaterLevelSpeedChange = 1f;
    [Range(0,5f)][SerializeField] private float maxWaterLevelSpeedChange = 1f;
    [Range(0,1f)][SerializeField] private float lerpVelocity = 0.1f;
    
    private float[] _initialVertexPosZ;
    private float _heightStep = 1f;
    private float _offset = 0f;
    private float _waterLevelDir = 0;
    private void Awake()
    {
        int length = waterGraphic.mesh.vertices.Length;

        _initialVertexPosZ = new float[length];
        
        Vector3[] vertices = waterGraphic.mesh.vertices;
        
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
        UpdateWaterLevel();
    }

    private void UpdateMesh()
    {
        Vector3[] vertices = waterGraphic.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].z = _initialVertexPosZ[i] + GetWaveHeight( transform.position.x + vertices[i].x);
        }

        waterGraphic.mesh.vertices = vertices;
        waterGraphic.mesh.RecalculateNormals();
    }

    private void UpdateWaveHeight()
    {
        _offset += Time.deltaTime * speed;
    }

    private void UpdateWaterLevel()
    {
        waterLevel.transform.position = Vector3.Lerp(waterLevel.transform.position, new Vector3(0, 
            Mathf.Clamp(waterLevel.transform.position.y + _heightStep * _waterLevelDir, downBorder.position.y , upBorder.position.y), 
            0f), Time.deltaTime * lerpVelocity); 
    }

    public Transform GetWaterLevel()
    {
        return waterLevel;
    }
    
    public void ChangeWaterLevel(float changeValue)
    {
        changeValue = Mathf.Clamp(changeValue, -1f, 1f);

        if (Mathf.Approximately(changeValue, 0))
        {
            _waterLevelDir = 0;
            return;
        }
        
        _waterLevelDir = Mathf.Clamp(_waterLevelDir + changeValue, -minWaterLevelSpeedChange, maxWaterLevelSpeedChange);
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + _offset);
    }
}
/*
  waterLevel.transform.position = Vector3.Lerp(waterLevel.transform.position, new Vector3(0, 
            Mathf.Clamp(upBorder.position.y + _heightStep * _waterLevelDir, downBorder.position.y , upBorder.position.y), 
            0f), Time.deltaTime * lerpVelocity); 
 */