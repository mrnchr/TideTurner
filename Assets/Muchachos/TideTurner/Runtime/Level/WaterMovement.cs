using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class WaterMovement : MonoBehaviour
    {
        [SerializeField] private Transform waterLevel;
        [SerializeField] private Transform upBorder;
        [SerializeField] private Transform downBorder;
    
        [SerializeField] private MeshFilter waterGraphic;

        [Header("Wave settings")]
        [Range(0,10f)][SerializeField] private float amplitude = 3f;
        [Range(0,10f)][SerializeField] private float length = 3f;
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

        public void Init()
        {
            SetWaterLevel(upBorder.transform.position);
        }

        public void SetWaterLevel(Vector3 position)
        {
            waterLevel.transform.position = position;
        }

        private void FixedUpdate()
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
            _offset += Time.fixedDeltaTime * speed;
        }

        private void UpdateWaterLevel()
        {
            var position = waterLevel.transform.position;
            position = Vector3.Lerp(position, new Vector3(0, 
                Mathf.Clamp(position.y + _heightStep * _waterLevelDir, downBorder.position.y , upBorder.position.y), 
                0f), Time.fixedDeltaTime * lerpVelocity);
            waterLevel.transform.position = position;
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

        private float GetWaveHeight(float x)
        {
            return amplitude * Mathf.Sin(x / length + _offset);
        }

        public float GetMeshWaterLevel(float x)
        {
            return waterLevel.position.y + GetWaveHeight(x);
        }
    }
}