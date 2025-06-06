using UnityEngine;

namespace Common
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