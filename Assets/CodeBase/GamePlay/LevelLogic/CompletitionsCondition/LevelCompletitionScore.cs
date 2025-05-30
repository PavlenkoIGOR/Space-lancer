using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    public class LevelCompletitionScore : LevelCondition
    {
        [SerializeField] private int _score;
        public override bool isCompleted 
        {
            get 
            {
                if (Player.instance.activeShip == null) { return false; }
                if (Player.instance.score >= _score)
                {
                    return true;
                }
                return false;
            }

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
