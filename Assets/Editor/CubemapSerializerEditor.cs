using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubemapSerializer))]
public class CubemapSerializerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CubemapSerializer s = (CubemapSerializer)target;
        if(GUILayout.Button("Update Cubemap"))
        {
            s.RenderToCubemap();
        }

        if (GUILayout.Button("Save..."))
        {
            string path = EditorUtility.SaveFolderPanel("Select save destination...", "", "");
            if (path.Length > 0) {
                s.SaveCubemap(path);
            }
        }
    }
}