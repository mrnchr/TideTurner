using UnityEngine;

public class FloatingConstant : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    private FloatingObject _floating;
    private Vector3 _position;

    private void Awake()
    {
        _floating = GetComponent<FloatingObject>();
    }

    private void Start()
    {
        _position = _floating.transform.localPosition;
    }

    private void FixedUpdate()
    {
        _floating.transform.position = _parent.position + _position;
    }
}