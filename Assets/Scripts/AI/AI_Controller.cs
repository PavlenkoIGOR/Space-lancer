using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space_lancer
{
    [RequireComponent(typeof(SpaceShip))]
    public class AI_Controller : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        [SerializeField] private AIBehaviour _behaviour;

        [SerializeField] private AI_PointPatrol _patrolPoint;

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

        private TimerSL _randomizeDirectionTimerSL;
        private TimerSL _fireTimerSL;
        private TimerSL _findNewTargetTimerSL;

        void Start()
        {
            _ship = GetComponent<SpaceShip>();

            InitTimers();
        }

        void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (_behaviour == AIBehaviour.Null)
            {

            }

            if (_behaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        void UpdateBehaviourPatrol()
        {
            Action_FindNewPosition();
            Action_ControlShip();
            Action_FindNewAttackTarget();
            Action_Fire();
            ActionEvadeCollision();
        }

        private void Action_Fire()
        {
            if (_selectedTarget != null)
            {
                if (_fireTimerSL.isFinished)
                {
                    _ship.ShipFire(TurretMode.Primary);

                    _fireTimerSL.Start(_shootDelay);
                }
            }
        }

        private void Action_FindNewAttackTarget()
        {
            if (_findNewTargetTimerSL.isFinished)
            {
                _selectedTarget = FindNearestDestructableTarget();

                _findNewTargetTimerSL.Start(_shootDelay);
            }
        }

        private Destructable FindNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;

            Destructable potentialTarget = null;
            foreach (var item in Destructable.allDestructables)
            {
                if (item.GetComponent<SpaceShip>() == _ship) { continue; }
                if (item.teamId == Destructable.teamIdNeutral)
                {
                    continue;
                }
                if (item.teamId == _ship.teamId)
                {
                    continue;
                }

                float dist = Vector2.Distance(_ship.transform.position, item.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = item;
                }
            }


            return potentialTarget;
        }

        private void Action_ControlShip()
        {
            _ship.thrustControl = _navigationLinear;

            _ship.torqueControl = ComputeAlignTorqueNormalized(_movePosition, _ship.transform) * _navigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        static float ComputeAlignTorqueNormalized(Vector3 targetPos, Transform ship)
        {
            Vector2 localPosition = ship.InverseTransformPoint(targetPos);

            float angle = Vector3.SignedAngle(localPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; //ограничивание нашего угла т.е. если угол больше 45 град

            return -angle;
        }

        private void Action_FindNewPosition()
        {
            if (_behaviour == AIBehaviour.Patrol)
            {
                if (_selectedTarget != null)
                {
                    _movePosition = _selectedTarget.transform.position;
                }
                else
                {
                    if (_patrolPoint != null)
                    {
                        bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.radius * _patrolPoint.radius;

                        if (isInsidePatrolZone)
                        {
                            if (_randomizeDirectionTimerSL.isFinished)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * _patrolPoint.radius + _patrolPoint.transform.position;

                                _movePosition = newPoint;

                                _randomizeDirectionTimerSL.Start(_randSelectMovePointTime);
                            }
                        }
                        else
                        {
                            _movePosition = _patrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        #region Timers

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, _evadeRayLength))
            {
                _movePosition = transform.position + transform.right * 100.0f;
            }
        }

        void InitTimers()
        {
            _randomizeDirectionTimerSL = new TimerSL(_randSelectMovePointTime);

            _fireTimerSL = new TimerSL(_shootDelay);

            _findNewTargetTimerSL = new TimerSL(_findNewTargetTime);
        }

        void UpdateTimers()
        {
            _randomizeDirectionTimerSL.RemoveTime(Time.deltaTime);

            _fireTimerSL.RemoveTime(Time.deltaTime);
            _findNewTargetTimerSL.RemoveTime(Time.deltaTime);
        }


        public void SetPatrolBehaviour(AI_PointPatrol aI_PointPatrol)
        {
            _behaviour = AIBehaviour.Patrol;
            _patrolPoint = aI_PointPatrol;
        }
        #endregion
    }
}
