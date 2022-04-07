using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public enum Type { NORMAL,WAYPOINT}
    private const string wayPointFile = "SpawnEnemy";
    public Type type = Type.NORMAL;

    public Color color = Color.yellow;
    public float radius = 0.1f;

    private void OnDrawGizmos()
    {
        if (type ==Type.NORMAL)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawIcon(transform.position + Vector3.up * 1, wayPointFile, true);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
