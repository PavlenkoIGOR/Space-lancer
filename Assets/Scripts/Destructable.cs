using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Space_lancer
{
    public class Destructable : Entity
    {
        #region Properties
        /// <summary>
        /// is destructable object or not
        /// </summary>
        [SerializeField] private bool _isDestructable;
        public bool isDestructable => _isDestructable;

        /// <summary>
        /// hitpoints on start
        /// </summary>
        [SerializeField] public int _hitPoints;

        /// <summary>
        /// current hitpoints
        /// </summary>
        private int _currentHitPoints;
        public int currentHitPoints => _currentHitPoints;

        [SerializeField] private UnityEvent _eventOnDeath;
        public UnityEvent eventOnDeath => _eventOnDeath;

        [SerializeField] private SpriteRenderer _shipSpriteRenderer;

        private Animator _shipExplosionAnim;
        [SerializeField] private GameObject _shipExplosionNode;
        #endregion

        #region Unity events
        protected virtual void Start()
        {
            _shipExplosionAnim = _shipExplosionNode.GetComponent<Animator>();
            _shipExplosionNode.gameObject.SetActive(false);
            _currentHitPoints = _hitPoints;
        }
        #endregion

        #region Public API
        public void ApplyDmg(int dmg)
        {
            if (_isDestructable == true)
            {
                _currentHitPoints -= dmg;
            }

            if (_currentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            if (_shipExplosionAnim != null)
            {
                _shipExplosionNode.gameObject.SetActive(true);
                // Запускаем анимацию и начинаем корутину для уничтожения объекта
                StartCoroutine(PlayExplosionAndDestroy());
            }
        }

        private IEnumerator PlayExplosionAndDestroy()
        {
            // Запускаем анимацию взрыва
            _shipExplosionAnim.Play("ShipExplosion", 0, 0f);

            if (_shipExplosionAnim.GetCurrentAnimatorStateInfo(0).length >= 0.2f)
            {
                _shipSpriteRenderer.enabled = false;
            }
            // Ждем, пока анимация завершится
            yield return new WaitForSeconds(_shipExplosionAnim.GetCurrentAnimatorStateInfo(0).length);

            // Уничтожаем объект после завершения анимации
            Destroy(gameObject);

            // Вызываем событие смерти
            _eventOnDeath?.Invoke();
        }
        #endregion


        static HashSet<Destructable> _allDestructables;
        public static IReadOnlyCollection<Destructable> allDestructables => _allDestructables;

        protected virtual void OnEnable()
        {
            if (_allDestructables == null)
            {
                _allDestructables = new HashSet<Destructable>();
            }
            _allDestructables.Add(this);
        }

        protected virtual void OnDestroy()
        {
            _allDestructables?.Remove(this);
            _eventOnDeath.RemoveAllListeners();
        }

        public const int teamIdNeutral = 0;

        [SerializeField] private int _teamId;
        public int teamId => _teamId;


    }
}