using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCompletitionPosition : LevelCondition
{
    [SerializeField] private float _radiusLCP;

    public override bool isCompleted
    {
        get
        {
            if (Player.instance.activeShip == null)
            {
                return false;
            }
            if (Vector3.Distance(Player.instance.activeShip.transform.position, transform.position) <= _radiusLCP) 
            {
                return true;
            }
            return false;
        }
    }

#if UNITY_EDITOR
    private static Color GizmoColor = new Color(0,1,0,0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, _radiusLCP);
    }
#endif
}
