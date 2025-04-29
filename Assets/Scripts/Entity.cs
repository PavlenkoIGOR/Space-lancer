using UnityEngine;

namespace Space_lancer
{
    /// <summary>
    /// Base class of all interactive entities in project
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private string _nickName;
        public string nickName => _nickName;
    }
}