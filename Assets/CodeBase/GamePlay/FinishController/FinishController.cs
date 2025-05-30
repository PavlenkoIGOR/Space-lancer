using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField] private GameObject _wallsNode;
    [SerializeField] private GameObject _finishLinePrefab;

    [SerializeField] private float _timerInterval = 5.0f;
    private float _timeRemain = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MyTimer();
    }

    private void MyTimer()
    {
        if (_timeRemain >= _timerInterval)
        {
            _timeRemain = 0;
            var finishline = Instantiate(_finishLinePrefab, new Vector3(_wallsNode.transform.position.x, _wallsNode.transform.position.y + 7.0f, transform.position.z-1.0f), Quaternion.identity);
        }
        _timeRemain += Time.fixedDeltaTime;
    }
}
