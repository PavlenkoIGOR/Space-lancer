using Space_lancer;
using UnityEngine;

public class FinishPrefabController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ship = collision.transform.root.GetComponent<SpaceShip>();
        if (ship != null)
        {
            Debug.Log("Won");
        }
    }
}
