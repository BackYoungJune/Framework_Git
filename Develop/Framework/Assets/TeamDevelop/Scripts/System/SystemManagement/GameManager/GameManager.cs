/*********************************************************					
* GameManager.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using System;
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
					
namespace Dev_System					
{
    public enum NewtorkStates
    {
        ONLINE, OFFLINE
    }

    public enum Language
    {
        Korea, English
    }

    public enum GamePlayMode
    {
        AutoSelect, FreeSelect
    }

    public enum LoginMode
    {
        None,
        LoginPlay,
        DebugPlay,
    }


    public partial class GameManager : MonoBehaviour					
	{
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------		

        public void CharGenderTrigger(Action male, Action female)
        {
            //todo : 현재버전에서는 여자캐릭터만 사용함
            female?.Invoke();
        }

        public void GestureRangeTrigger(Action punchAction, Action kickAction)
        {
            //todo : 운동모듈 개발전까지 임시로 상체운동으로만 사용
            punchAction?.Invoke();
        }

        public void GamePlayModeTrigger(Action autoSelect, Action freeSelect)
        {
            switch (gamePlayMode)
            {
                case GamePlayMode.AutoSelect:
                    autoSelect?.Invoke();
                    break;

                case GamePlayMode.FreeSelect:
                    freeSelect?.Invoke();
                    break;     
            }
        }

        public void LanguageModeTrigger(Action korea, Action english)
        {
            //todo : 현재버전에서는 영어만 사용
            english?.Invoke();
        }

        public void NetworkStateTrigger(Action onlineAction, Action OfflineAction)
        {
            //todo : 현재버전에서는 로컬만 사용
            OfflineAction?.Invoke();


            //CheckNetworkState();

            //switch (NetworkState)
            //{
            //    case NewtorkStates.ONLINE:
            //        onlineAction?.Invoke();
            //        break;
            //    case NewtorkStates.OFFLINE:
            //        OfflineAction?.Invoke();
            //        break;
            //}
        }

        public void LoginModeTrigger(Action loginPlayMode = null, Action debugPlayMode = null)
        {
            switch (pcurLoginMode)
            {
                case LoginMode.LoginPlay:
                    {
                        loginPlayMode?.Invoke();
                        break;
                    }
                case LoginMode.DebugPlay:
                    {
                        debugPlayMode?.Invoke();
                        break;
                    }
            }
        }


        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					





    }//end of class								
}//end of namespace					