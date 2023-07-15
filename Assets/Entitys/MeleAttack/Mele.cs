using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Mele : Weapon
{
    public bool isOnPlayer = false;

    public bool isAttacking = false;

    public PlayerAttack plAttack;

    public int meleIndex;

    public override void OnTriggerStay(Collider other)
    {
        if(!isOnPlayer)
        {
            if (isAttacking)
            {

                
                base.OnTriggerStay(other);
                //    onlyOnce = false;
            }
        }
      else
        {
            if (isAttacking)
            {
                if (plAttack.hits[meleIndex])
                {
                 //   Debug.Log("attacked with the index" + meleIndex);

                    base.OnTriggerStay(other);
                }
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
       // base.OnTriggerEnter(other);
    }

    public void Update()
    {
        isAttacking = wielder.isAttacking;

        if (!isAttacking) onlyOnce = true;
    }

}

/*[CustomEditor(typeof(Mele))]
public class MyScriptEditor : Editor
{
    private SerializedProperty myBoolProperty;
    private SerializedProperty otherScriptBoolProperty;

    private void OnEnable()
    {
        myBoolProperty = serializedObject.FindProperty("anotherCondition");
        otherScriptBoolProperty = serializedObject.FindProperty("otherScript.boolVariable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(myBoolProperty);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(otherScriptBoolProperty);

        serializedObject.ApplyModifiedProperties();
    }
}
*/
