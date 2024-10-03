/*********************************************************					
* UIModuleSOEditor.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Dev_UI
{
    [CustomEditor(typeof(UIModuleSO))]
    public class UIModuleSOEditor : Editor
    {

        SerializedProperty SceneTypeUISceneType;
        SerializedProperty UIModuleInfo_Common;
        SerializedProperty UIModuleInfos_Lobby;
        SerializedProperty UIModuleInfos_Adventure;
        SerializedProperty UIModuleInfos_Fitness;
        SerializedProperty UIModuleInfos_Meditation;



        void OnEnable()
        {
            SceneTypeUISceneType = serializedObject.FindProperty("SceneTypeUISceneType");
            UIModuleInfo_Common = serializedObject.FindProperty("UIModuleInfo_Common");
            UIModuleInfos_Lobby = serializedObject.FindProperty("UIModuleInfos_Lobby");
            UIModuleInfos_Adventure = serializedObject.FindProperty("UIModuleInfos_Adventure");
            UIModuleInfos_Fitness = serializedObject.FindProperty("UIModuleInfos_Fitness");
            UIModuleInfos_Meditation = serializedObject.FindProperty("UIModuleInfos_Meditation");
        }



        public override void OnInspectorGUI()
        {

            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }



        void DrawProperties()
        {

            switch ((UISceneTypes)SceneTypeUISceneType.enumValueIndex)
            {
                case UISceneTypes.None:

                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.HelpBox("UI SceneType을 설정하세요", MessageType.Error, true);      
                    break;
                case UISceneTypes.Common:

                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.PropertyField(UIModuleInfo_Common);
                    break;
                case UISceneTypes.Lobby:


                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.PropertyField(UIModuleInfos_Lobby);
                    break;
                case UISceneTypes.Adventure:

                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.PropertyField(UIModuleInfos_Adventure);
                    break;
                case UISceneTypes.Fitness:

                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.PropertyField(UIModuleInfos_Fitness);
                    break;

                case UISceneTypes.Meditation:

                    EditorGUILayout.PropertyField(SceneTypeUISceneType);
                    EditorGUILayout.PropertyField(UIModuleInfos_Meditation);
                    break;
            }




        }






    }//end of class					


}//end of namespace					