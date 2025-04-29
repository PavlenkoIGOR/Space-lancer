using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _interpolationLinear;
    [SerializeField] private float _interpolationAngular;
    [SerializeField] private float _camera2Offset;
    [SerializeField] private float _forwardOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target == null || _camera == null) return;
        Vector2 cameraPos = _camera.transform.position;
        Vector2 targetPos = _target.position + _target.transform.up * _forwardOffset;
        Vector2 newCameraPos = Vector2.Lerp(cameraPos, targetPos, _interpolationLinear * Time.deltaTime);
        _camera.transform.position = new Vector3(newCameraPos.x, newCameraPos.y, _camera2Offset);

        if (_interpolationAngular > 0)
        {
            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _target.rotation, _interpolationAngular * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

}
