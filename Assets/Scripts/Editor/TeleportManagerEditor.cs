using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeleportManager))]
public class TeleportManagerEditor : Editor
{
  
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        TeleportManager myScript = (TeleportManager)target;
       
        if (GUILayout.Button("Teleport"))
        {
            myScript.StartTeleportPoint();
        }
    }
}
