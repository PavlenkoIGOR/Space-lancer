using System;
using UnityEngine;
using Common;

namespace Space_lancer
{
    [RequireComponent(typeof(SpaceShip))]
    public class AI_Controller : MonoBehaviour
    {
        #region ��� ������ ����������
        private enum MovementShape
        {
            Simple,
            Circle,
            Square
        }
        [SerializeField] private MovementShape _movementShape;

        [SerializeField] private float _shapeSize = 10f; // ������ ��� ������� ��������
        #endregion

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

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE; //������������� ������ ���� �.�. ���� ���� ������ 45 ����

            return -angle;
        }

        private bool isHasCenter = false;
        private Vector3 _centerOfPatrolShape;
        private float _currentAngle = 0f; // �������� � �����
        private float orbitSpeed = 0.1f; // �������� �������� (���/�), ��������� �� �������������
        private void Action_FindNewPosition()
        {
            if (_behaviour == AIBehaviour.Patrol)
            {
                if (_selectedTarget != null)
                {
                    //_movePosition = _selectedTarget.transform.position;

                    Vector3 targetPos = _selectedTarget.transform.position;

                    // �������� Rigidbody2D ����
                    Rigidbody2D targetRb = _selectedTarget.GetComponent<Rigidbody2D>();
                    Vector3 targetVelocity = Vector3.zero;
                    if (targetRb != null)
                    {
                        targetVelocity = targetRb.velocity; // Vector2 -> Vector3
                    }

                    float distance = Vector3.Distance(transform.position, targetPos);


                    float timeToReach = distance / (targetVelocity.z + 2.0f);

                    Vector3 predictedPos = targetPos + (Vector3)targetVelocity * timeToReach;

                    _movePosition = predictedPos;
                }
                else
                {
                    if (_patrolPoint != null)
                    {
                        Vector3 newPoint;
                        if (_movementShape == MovementShape.Simple)
                        {
                            bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.radius * _patrolPoint.radius;

                            if (isInsidePatrolZone)
                            {
                                if (_randomizeDirectionTimerSL.isFinished)
                                {
                                    newPoint = UnityEngine.Random.onUnitSphere * _patrolPoint.radius + _patrolPoint.transform.position;

                                    _movePosition = newPoint;

                                    _randomizeDirectionTimerSL.Start(_randSelectMovePointTime);
                                }
                            }
                            else
                            {
                                _movePosition = _patrolPoint.transform.position;
                            }
                        }
                        if (_movementShape == MovementShape.Square)
                        {
                            float halfSide = _shapeSize / 2f;
                            float randX = UnityEngine.Random.Range(-halfSide, halfSide);
                            float randY = UnityEngine.Random.Range(-halfSide, halfSide);
                            newPoint = _patrolPoint.transform.position + new Vector3(randX, randY, 0);
                            _movePosition = newPoint;
                        }
                        if (_movementShape == MovementShape.Circle)
                        {
                            if (isHasCenter == true)
                            {
                                // ��������� ���� ��� �������� �� �������
                                _currentAngle -= orbitSpeed * Time.deltaTime;

                                // ��������� ����� ������� �� ����������
                                float x = _centerOfPatrolShape.x + Mathf.Cos(_currentAngle) * _shapeSize;
                                float y = _centerOfPatrolShape.y + Mathf.Sin(_currentAngle) * _shapeSize;

                                transform.position = new Vector3(x, y, transform.position.z);

                                // ������� ������� ���, ����� +X �������� �� �����
                                Vector3 directionToCenter = _centerOfPatrolShape - transform.position;
                                float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0, 0, angle);
                            }
                            else
                            {
                                isHasCenter = true;
                                // ���������� ������������� ����� ���, ����� ������ ��� �� ���������� _shapeSize
                                _centerOfPatrolShape = transform.position + transform.right * _shapeSize;


                                // �������������� ���� ���, ����� ������ ��� �� ����������
                                Vector3 dirFromCenter = transform.position - _centerOfPatrolShape;
                                _currentAngle = Mathf.Atan2(dirFromCenter.y, dirFromCenter.x);
                            }
                        }

                        //Debug.Log($"_patrolPoint {_patrolPoint}");
                    }
                    else
                    {
                        //Debug.Log("_patrolPoint null");
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
