using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGravityWells : MonoBehaviour
{
    [SerializeField] private GameObject _leftWell;
    [SerializeField] private GameObject _rightWell;

    [SerializeField] private float _timerInterval = 2.0f;
    private float _timeRemain = 0;
    private BoxCollider2D _boxCollider2D_leftWell;
    private BoxCollider2D _boxCollider2D_rightWell;
    private float _boxCollider2DStartPosition_leftWell;
    private float _boxCollider2DStartPosition_rightWell;
    private int directionColliderOffset = -1;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D_leftWell = _leftWell?.GetComponent<BoxCollider2D>();
        _boxCollider2D_rightWell = _rightWell?.GetComponent<BoxCollider2D>();

        _boxCollider2DStartPosition_rightWell = _boxCollider2D_rightWell.offset.x;
        _boxCollider2DStartPosition_leftWell = _boxCollider2D_leftWell.offset.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCollider();
    }
    private void MoveCollider()
    {
        if (_timeRemain >= _timerInterval)
        {
            _timeRemain = 0;

            _timerInterval = UnityEngine.Random.Range(1, 5);

            _boxCollider2D_leftWell.offset = new Vector2(_boxCollider2DStartPosition_leftWell + directionColliderOffset * _boxCollider2D_leftWell.size.x / 2, 0);
            _boxCollider2D_rightWell.offset = new Vector2(_boxCollider2DStartPosition_rightWell + directionColliderOffset * _boxCollider2D_rightWell.size.x / 2, 0);

            directionColliderOffset *= -1;
        }
        _timeRemain += Time.fixedDeltaTime;
    }
}
