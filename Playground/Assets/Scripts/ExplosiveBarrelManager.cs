using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// excludes any code within this if on build
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> allBarrels = new List<ExplosiveBarrel>();

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (ExplosiveBarrel barrel in allBarrels)
        {
            // draws a line with anti-aliasing 
            // Handles.DrawAAPolyLine(transform.position, barrel.transform.position);
            // Gizmos.DrawLine(transform.position, barrel.transform.position);

            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeight;
            Handles.DrawBezier(managerPos, barrelPos, managerPos - offset, barrelPos + offset, Color.white, EditorGUIUtility.whiteTexture, 1f);
        }
    }
    #endif
}
