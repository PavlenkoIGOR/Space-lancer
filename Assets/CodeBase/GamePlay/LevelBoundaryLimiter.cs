using UnityEngine;

namespace Space_lancer
{
    /// <summary>
    /// Применяется на объект, который надо ограничить. Работает в связке со скриптом LevelBoundary.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.instance == null) 
            {
                return;
            }

            var levelBoundary = LevelBoundary.instance;
            var radius = levelBoundary.Radius;

            if (transform.position.magnitude > radius)
            {
                if (levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * radius;
                }
                if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * radius;
                }
            }
        }
    }
}
