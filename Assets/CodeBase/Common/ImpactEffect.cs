using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private float _timer;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_timer < _lifeTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
