using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingPlatformController))]
public class MovingPlatformControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovingPlatformController controller = (MovingPlatformController)target;

        controller.waypointObj = (GameObject)EditorGUILayout.ObjectField("Waypoint Object", controller.waypointObj, typeof(GameObject), false);
        controller.moveSpeed = EditorGUILayout.FloatField("Speed: ", controller.moveSpeed);

        EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);

        if(controller.waypoints != null && controller.waypoints.Count != 0)
        {
            for (int i = 0; i < controller.waypoints.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                controller.waypoints[i].gameObject.name = EditorGUILayout.TextField(controller.waypoints[i].gameObject.name);
                controller.waypoints[i].position = EditorGUILayout.Vector2Field("", controller.waypoints[i].position);
                if(GUILayout.Button("Delete?"))
                {
                    controller.RemoveWaypoint(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        if(GUILayout.Button("Add Waypoint"))
        {
            controller.AddNewWaypoint();
        }

        if(GUILayout.Button("Clear Waypoints"))
        {
            controller.ClearWaypoints();
        }
        // base.OnInspectorGUI();
    }
}
