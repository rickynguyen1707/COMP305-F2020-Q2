using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject waypointObj;
    public float moveSpeed = 5.0f;
    public List<Transform> waypoints;

    private int currentTargetIndex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        waypoints = new List<Transform>();
        foreach (Transform t in transform.parent.GetChild(1))
        {
            waypoints.Add(t);
        }

        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentTargetIndex].position, Time.deltaTime * moveSpeed);

            if (Vector2.Distance(transform.position, waypoints[currentTargetIndex].position) < 0.01f)
            {
                // Close enough! Change my target
                currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
            }
        }
    }

    public void AddNewWaypoint()
    {
        GameObject gObj = Instantiate(waypointObj, Vector2.zero, Quaternion.identity);
        gObj.transform.SetParent(transform.parent.GetChild(1));
        gObj.name = "Waypoint " + waypoints.Count;
        waypoints.Add(gObj.transform);
    }

    public void RemoveWaypoint(int index)
    {
        waypoints.RemoveAt(index);
        //  waypoints.TrimExcess();
        DestroyImmediate(transform.parent.GetChild(1).GetChild(index).gameObject);
    }

    public void ClearWaypoints()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            DestroyImmediate(waypoints[i].gameObject);
        }
        
        waypoints.Clear();
    }
}
