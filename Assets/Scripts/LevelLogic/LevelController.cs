using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Space_lancer
{
    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] private LevelCondition[] _conditions;
        private bool _isLevelCompleted;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_isLevelCompleted) return;

            int numCompleted = 0;

            for (int i = 0; i < _conditions.Length; i++)
            {
                if (_conditions[i].isCompleted)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == _conditions.Length)
            {
                _isLevelCompleted = true;
            }
        }
    }
}