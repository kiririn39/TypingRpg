using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Temp : MonoBehaviour
{

    public Animation lol = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lol = GetComponent<Animation>();
    }
}

[CustomEditor(typeof(Temp))]
public class CustomEditorTemp : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Debug.Log((target as Temp).lol.clip.name);
        if (GUILayout.Button("play"))
        {
            (target as Temp).lol.Stop();
            (target as Temp).lol.Play("Attack");
            
        }
        
    }
}
