using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PistonMuscleGroup))]
public class PistonMuscleGroupEditor : Editor
{
    private PistonMuscleGroup pistonMuscleGroup;

    private SerializedProperty nameProperty;
    
    private SerializedProperty startJointProperty;
    
    private SerializedProperty endJointProperty;
    
    void OnEnable()
    {
        pistonMuscleGroup = serializedObject.targetObject as PistonMuscleGroup;

        nameProperty = serializedObject.FindProperty("name");
        
        startJointProperty = serializedObject.FindProperty("startJoint");
        
        endJointProperty = serializedObject.FindProperty("endJoint");
    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = pistonMuscleGroup != null && startJointProperty != null && endJointProperty != null;
        if (GUILayout.Button("Setup Muscles"))
        {
            var startJointObject = startJointProperty.objectReferenceValue as GameObject;
            var endJointObject = endJointProperty.objectReferenceValue as GameObject;

            var startDR = startJointObject.transform.Find("ConDR");
            var startVR = startJointObject.transform.Find("ConVR");
            var startDL = startJointObject.transform.Find("ConDL");
            var startVL = startJointObject.transform.Find("ConVL");
            
            var endDR = endJointObject.transform.Find("ConDR");
            var endVR = endJointObject.transform.Find("ConVR");
            var endDL = endJointObject.transform.Find("ConDL");
            var endVL = endJointObject.transform.Find("ConVL");

            var muscleDR = pistonMuscleGroup.transform.Find("DR").GetComponent<PistonMuscle>();
            var muscleVR = pistonMuscleGroup.transform.Find("VR").GetComponent<PistonMuscle>();
            var muscleDL = pistonMuscleGroup.transform.Find("DL").GetComponent<PistonMuscle>();
            var muscleVL = pistonMuscleGroup.transform.Find("VL").GetComponent<PistonMuscle>();

            muscleDR.Root = startDR;
            muscleDR.Attachment = endDR;
            
            muscleVR.Root = startVR;
            muscleVR.Attachment = endVR;
            
            muscleDL.Root = startDL;
            muscleDL.Attachment = endDL;
            
            muscleVL.Root = startVL;
            muscleVL.Attachment = endVL;

            pistonMuscleGroup.gameObject.name = nameProperty.stringValue + "-" + startJointObject.name + "-" + endJointObject.name;
            pistonMuscleGroup.transform.position = (startJointObject.transform.position + endJointObject.transform.position) * 0.5f;
        }
        GUI.enabled = true;
        
        base.OnInspectorGUI();
    }
}