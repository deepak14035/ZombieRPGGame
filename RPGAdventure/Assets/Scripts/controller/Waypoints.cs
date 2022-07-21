using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, 0.3f);
            if (i == transform.childCount - 1)
                Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(0).transform.position);
            else
                Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }

    public Vector3 getWaypointPosition(int index)
    {
        return transform.GetChild(index).transform.position;
    }
    public int getNextIndex(int i)
    {
        return (i+1)%transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
