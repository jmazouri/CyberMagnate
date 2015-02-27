using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(TextBinding))]
public class TextBindingInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TextBinding cur = (TextBinding) target;

        GUI.enabled = !Application.isPlaying;

        cur.Reference = EditorGUILayout.ObjectField("Target", cur.Reference, typeof(Object), true);

        if (cur.Reference != null)
        {
            string[] Options = cur.Reference.GetType().GetFields().Select(d => d.Name).ToArray();
            
            cur.Format = EditorGUILayout.TextField("Format", cur.Format);
            cur.ShowChanges = EditorGUILayout.Toggle("Value Change Animation?", cur.ShowChanges);

            cur.PropIndex = EditorGUILayout.Popup("Property", cur.PropIndex, Options);

            EditorUtility.SetDirty(target);
        }
        else
        {
            EditorGUILayout.HelpBox("Set a reference", MessageType.Error);
        }
    }
}
