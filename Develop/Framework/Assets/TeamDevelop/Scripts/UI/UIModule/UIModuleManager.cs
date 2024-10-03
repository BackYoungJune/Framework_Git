/*********************************************************					
* UIModuleManager.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using Dev_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dev_UI
{
    public class UIModuleManager : MonoBehaviour
    {

        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------					


        //**********************************************
        //		단일 UIModule 활성화
        //**********************************************
        public UIModule EnableUIModule(
            UISceneTypes type, UIStates uiState, string uniqueID, UIGroups group, Vector3 genpos = default,
            Transform genTrans = null, float followSpeed = 10f, float followDistanceCam = 3f)
        {

            // 오브젝트 풀링 적용
            GameObject newObj = Managers.ObjectPool.GetUIFromPool(type, uniqueID, genpos, genTrans);

            UIModule newModule = newObj.GetComponent<UIModule>();

            //활성화테이블 추가
            ActivedUIModules.Add(newModule);

            //Enable UIModule 호출
            StartCoroutine(newModule.Callback_EnableUI(
                state: uiState,
                uniqueKey: uniqueID,
                uiGroup: group,
                followspeed: followSpeed,
                followDistanceFromCam: followDistanceCam));



#if UNITY_EDITOR
            Debug.Log(string.Format("<b><color=#1DDB16> <EnableUIModule> UIModule = [{0}]</color></b> ", uniqueID));
#endif
            return newModule;
        }



        //**********************************************
        //		단일 UIModule 비활성화
        //**********************************************
        public void DisableUIModule(string uniqueID, UIGroups group = UIGroups.Group_1)
        {

            for (int i = 0; i < ActivedUIModules.Count; i++)
            {
                //group 비교
                UIModule disableTarget = ActivedUIModules.Find(element => element.MatchUIModule(uniqueID, group) != null);

                if (disableTarget == null)
                {
#if UNITY_EDITOR
                    Debug.Log(string.Format("[ERROR] <DisEnableUI> UIModule =  <b><color=#FF00DD>[{0}]</color></b> 찾을 수 없음", uniqueID));
#endif
                    return;
                }

                //활성화테이블 제거
                ActivedUIModules.Remove(disableTarget);

                //Disable UIModule 호출
                StartCoroutine(disableTarget.Callback_DisableUI());
            }
        }

        IEnumerator DisableUIModule(UIModule targetModule)
        {
            UIModule disableTarget = ActivedUIModules.Find(element => element.MatchUIModule(targetModule) != null);

            if (disableTarget == null)
            {
#if UNITY_EDITOR
                Debug.Log(string.Format("[ERROR] <DisEnableUI> UIModule =  <b><color=#FF00DD>[{0}]</color></b> 찾을 수 없음", targetModule.name));
#endif
                yield break;
            }

            //활성화테이블 제거
            ActivedUIModules.Remove(disableTarget);

            //Disable UIModule 호출
            yield return StartCoroutine(disableTarget.Callback_DisableUI());
        }


        //**********************************************
        //		활성화중인 모든 UIModule 비활성화
        //**********************************************
        public IEnumerator AllDisableUIModule()
        {
            if (ActivedUIModules.Count <= 0)
                yield break;


            ActivedUIModules.RemoveAll(module =>
            {
                StartCoroutine(module.Callback_DisableUI());
                return true;
            });

        }

        public IEnumerator AllDisableUIModuleGroup(UIGroups group)
        {
            if (ActivedUIModules.Count <= 0)
                yield break;

            ActivedUIModules.RemoveAll(module =>
            {
                if (module.pUIGroup == group)
                {
#if UNITY_EDITOR
                    Debug.LogError(string.Format("<b><color=#FF0000>[{0}]그룹 / [{1}] 비활성화 </color></b>", group, module.pUniqueKey));
#endif
                    StartCoroutine(module.Callback_DisableUI());
                    return true;
                }
                return false;
            });
        }

        //**********************************************
        //		생성된 UIModule중 특정 Module 얻기
        //**********************************************
        public UIModule GetUIModule(string uniqueKey)
        {
            UIModule target = ActivedUIModules.Find(element => element.MatchUIModule(uniqueKey) != null);
            return target;
        }


        public UIModuleSO[] pUIModuleSOs { get { return UIModuleSOs; } }
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------		
        [Header("--------------- For Settings ---------------")]
        [SerializeField] private UIModuleSO[] UIModuleSOs;


        [Header("--------------- For Debug ---------------")]
        [SerializeField] private List<UIModule> ActivedUIModules = new();




    }//end of class					
}//end of namespace					