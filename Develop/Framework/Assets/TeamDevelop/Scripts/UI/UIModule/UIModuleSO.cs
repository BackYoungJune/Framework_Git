/*********************************************************					
* UIModuleSO.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dev_UI
{
    public enum UISceneTypes
    {
        None,
        Common,
        Lobby,
        Adventure,
        Fitness,
        Meditation
    }
    public enum UIGroups
    {
        None,
        Group_1, Group_2, Group_3, Group_4, Group_5, Group_6, Group_7, Group_8, Group_9, Group_10,
        Group_11, Group_12, Group_13, Group_14, Group_15, Group_16, Group_17, Group_18, Group_19, Group_20,
        Group_21, Group_22, Group_23, Group_24, Group_25, Group_26, Group_27, Group_28, Group_29, Group_30,
        Group_31, Group_32, Group_33, Group_34, Group_35, Group_36, Group_37, Group_38, Group_39, Group_40,
        Group_41, Group_42, Group_43, Group_44, Group_45, Group_46, Group_47, Group_48, Group_49, Group_50,
        Group_51, Group_52, Group_53, Group_54, Group_55, Group_56, Group_57, Group_58, Group_59, Group_60,
        Group_61, Group_62, Group_63, Group_64, Group_65, Group_66, Group_67, Group_68, Group_69, Group_70,
        Group_71, Group_72, Group_73, Group_74, Group_75, Group_76, Group_77, Group_78, Group_79, Group_80,
        Group_81, Group_82, Group_83, Group_84, Group_85, Group_86, Group_87, Group_88, Group_89, Group_90,
        Group_91, Group_92, Group_93, Group_94, Group_95, Group_96, Group_97, Group_98, Group_99, Group_100,
    }

    [Serializable]
    public class UIModuleInfo
    {
        [SerializeField] private string UniqueID;       //구분할수있는 고유 키값
        [SerializeField] private UIModule UIModulePrefab;   //프리팹


        public string pUniqueID { get { return UniqueID; } }
        public UIModule pUIModulePrefab { get { return UIModulePrefab; } }

        public UIModuleInfo MatchUIModule(string id)
        {
            return UniqueID == id ? this : null;
        }

        public UIModuleInfo MatchUIModule(UIModule module)
        {
            return UIModulePrefab == module ? this : null;
        }
    }



    [CreateAssetMenu(fileName = "UIModule_", menuName = "#ScriptableObject/#UIModule")]
    public class UIModuleSO : ScriptableObject
    {
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------		
        public UIModuleInfo GetUIModule(UISceneTypes type, string idKey)
        {

            UIModuleInfo outModule;

            switch (type)
            {
                case UISceneTypes.Common:

                    outModule = UIModuleInfo_Common.Find(element => element.MatchUIModule(idKey) != null);
                    return outModule;

                case UISceneTypes.Lobby:

                    outModule = UIModuleInfos_Lobby.Find(element => element.MatchUIModule(idKey) != null);
                    return outModule;

                case UISceneTypes.Adventure:

                    outModule = UIModuleInfos_Adventure.Find(element => element.MatchUIModule(idKey) != null);
                    return outModule;

                case UISceneTypes.Fitness:

                    outModule = UIModuleInfos_Fitness.Find(element => element.MatchUIModule(idKey) != null);
                    return outModule;

                case UISceneTypes.Meditation:

                    outModule = UIModuleInfos_Meditation.Find(element => element.MatchUIModule(idKey) != null);
                    return outModule;

                default:
                    return null;
            }

        }



        public List<UIModuleInfo> pUIModuleInfo_Common { get { return UIModuleInfo_Common; } set { UIModuleInfo_Common = value; } }
        public List<UIModuleInfo> pUIModuleInfos_Lobby { get { return UIModuleInfos_Lobby; } set { UIModuleInfos_Lobby = value; } }
        public List<UIModuleInfo> pUIModuleInfos_Adventure { get { return UIModuleInfos_Adventure; } set { UIModuleInfos_Adventure = value; } }
        public List<UIModuleInfo> pUIModuleInfos_Fitness { get { return UIModuleInfos_Fitness; } set { UIModuleInfos_Fitness = value; } }
        public List<UIModuleInfo> pUIModuleInfos_Meditation { get { return UIModuleInfos_Meditation; } set { UIModuleInfos_Meditation = value; } }

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------	
        [SerializeField] private UISceneTypes SceneTypeUISceneType;
        [SerializeField] private List<UIModuleInfo> UIModuleInfo_Common;
        [SerializeField] private List<UIModuleInfo> UIModuleInfos_Lobby;
        [SerializeField] private List<UIModuleInfo> UIModuleInfos_Adventure;
        [SerializeField] private List<UIModuleInfo> UIModuleInfos_Fitness;
        [SerializeField] private List<UIModuleInfo> UIModuleInfos_Meditation;




    }//end of class					


}//end of namespace					