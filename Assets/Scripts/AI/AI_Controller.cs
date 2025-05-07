using UnityEngine;

namespace Space_lancer
{
    public class AI_Controller : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        [SerializeField] private AIBehaviour _behaviour;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _navigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _navigationAngular;

        [SerializeField] private float _randSelectMovePointTime;

        [SerializeField] private float _findNewTargetTime;

        [SerializeField] private float _shootDelay;

        [SerializeField] private float _evadeRayLength;

        private SpaceShip _ship;
        private Vector3 _movePosition;
        private Destructable _selectedTarget;

        private TimerSL _timerSL;

        void Start()
        {
            _timerSL = new TimerSL(3);
        }

        void Update()
        {
            _timerSL.RemoveTime(Time.deltaTime);
            if (_timerSL.isFinished)
            {
                Debug.Log("Test");
                _timerSL.Start(3);
            }
        }
    }
}
