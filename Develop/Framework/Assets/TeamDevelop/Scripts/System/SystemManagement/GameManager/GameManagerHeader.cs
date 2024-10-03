/*********************************************************					
* GameManagerHeader.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using System;
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
					
namespace Dev_System
{					
	public partial class GameManager : MonoBehaviour					
	{
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------					

        public Language pCurLanguage { get { return curLanguage; } set { curLanguage = value; } }
        public NewtorkStates pNetworkState { get { return NetworkState; } }
        public LoginMode pcurLoginMode { get { return curLoginMode; } set { curLoginMode = value; } }
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------				
        [Header("*************** FPS Display ***************")]
        [SerializeField] private bool            UseFPS = false;
        [SerializeField] private int             FPS_fontSize;
        [SerializeField] private Color           FPS_FontColor;

        [Header("*************** Settings ***************")]
        [SerializeField] private Language        curLanguage;
        [SerializeField] private GamePlayMode    gamePlayMode = GamePlayMode.AutoSelect;
        [SerializeField] private NewtorkStates   NetworkState;
        [SerializeField] private LoginMode       curLoginMode;
       
        void Start()					
		{
            if (Application.platform == RuntimePlatform.Android)
            {
                curLoginMode = LoginMode.LoginPlay;
            }
        }					
							
							
		void Update()					
		{
            InputManager.OnUpdate();
        }



        bool CheckNetworkState()
        {
            NetworkReachability reachability = Application.internetReachability;

            switch (reachability)
            {
                //네트워크 연결안됨
                case NetworkReachability.NotReachable:
                    Debug.LogError("<b><color=cyan>네트워크 연결 안됨</color></b>");

                    NetworkState = NewtorkStates.OFFLINE;
                    return false;

                //네트워크 연결됨
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                case NetworkReachability.ReachableViaLocalAreaNetwork:

#if UNITY_EDITOR
                    Debug.Log("<b><color=cyan>네트워크 연결 됨</color></b>");
#endif

                    NetworkState = NewtorkStates.ONLINE;
                    return true;
                default:
                    return false;
            }
        }


    }//end of class								
}//end of namespace					