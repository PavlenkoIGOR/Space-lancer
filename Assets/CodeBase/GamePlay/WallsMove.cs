using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsMove : MonoBehaviour
{
    [SerializeField] private Transform _target;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
    }
}
