/*********************************************************					
* UIModule.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using Dev_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Dev_Unit;


namespace Dev_UI
{
    public abstract class UIModule : MonoBehaviour
    {

        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------				
        public IEnumerator Callback_EnableUI(UIStates state, string uniqueKey, UIGroups uiGroup, float followspeed, float followDistanceFromCam)
        {

#if UNITY_EDITOR
            Debug.Log("UIModule 활성화 성공");
#endif

            UniqueKey = uniqueKey;
            UIGroup = uiGroup;
            FollowUITarget = this.transform;
            FollowSpeed = followspeed;
            FollowDistanceFromCam = followDistanceFromCam;


            if (UIStateCache.Count <= 0)
            {
                //UIStateCache 캐싱
                UIStateCache.Add(UIStates.None, null);
                UIStateCache.Add(UIStates.Fixed, new UIInterface_Fixed());
                //UIStateCache.Add(UIStates.FollowerVR, new UIInterface_FollowerVR());
            }

            if (UIStateMachine == null)
            {
                //UI 스테이트머신
                UIStateMachine = new UIModuleStateMachine<UIModule>(this, UIStateCache[UIStates.None]);
            }


            ChangeStateMachine(state);

            yield return StartCoroutine(EnableUI());
        }


        public IEnumerator Callback_DisableUI()
        {

#if UNITY_EDITOR
            Debug.Log("UIModule 비활성화 성공");
#endif
            yield return StartCoroutine(DisableUI());
            Action_End?.Invoke();
            Action_End = null;
            Managers.ObjectPool.ReturnObjectPool(PoolObjectType.UI, this.gameObject);
            //Destroy(this.gameObject);
        }


        public void ChangeStateMachine(UIStates state)
        {
            if (UIStateMachine == UIStateCache[state])
            {
                Debug.LogError("[UIModule] 동일한 UI State로 변경 불가!");
                return;
            }

            UIStateMachine.InitState(UIStateCache[state]);
            CurUIState = state;
        }


        public UIModule MatchUIModule(string uniqueId, UIGroups group)
        {
            return UIGroup == group && UniqueKey == uniqueId ? this : null;
        }


        public UIModule MatchUIModule(string uniqueId)
        {
            return UniqueKey == uniqueId ? this : null;
        }


        public UIModule MatchUIModule(UIModule module)
        {
            return this == module ? this : null;
        }


        public void MovePos(Vector3 offset)
        {
            PosOffset = offset;
        }





        public Action Action_End;
        public Canvas pTargetCanvas { get { return TargetCanvas; } }
        public string pUniqueKey { get { return UniqueKey; } }
        public UIGroups pUIGroup { get { return UIGroup; } }
        public Vector3 pPosOffset { get { return PosOffset; } }
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------			
        [Header("For Settings")]
        [SerializeField] protected Canvas TargetCanvas;

        [Header("For Debug")]
        [SerializeField] private string UniqueKey;
        [SerializeField] private Vector3 PosOffset;
        [SerializeField] private UIGroups UIGroup;
        [SerializeField] private UIStates CurUIState;


        //UI Interfaces
        protected UIModuleStateMachine<UIModule> UIStateMachine;
        protected Dictionary<UIStates, UIInterface<UIModule>> UIStateCache = new();
        public Transform FollowUITarget;
        public float FollowSpeed = 10f;
        public float FollowDistanceFromCam = 3f;


        protected virtual void Start()
        {
            InitUIModule();
        }


        protected virtual void Update()
        {
            if (UIStateMachine != null)
            {
                if (UIStateMachine.CurState != null)
                {
                    UIStateMachine.CurState.UpdateExcute();
                }
            }
        }


        protected virtual void OnDisable()
        {
            PosOffset = Vector3.zero;
            ResetUIModule();
        }


        //--------------------------------------------------------					
        // 추상함수
        //--------------------------------------------------------		
        //UIModule 초기화
        protected abstract void ResetUIModule();

        //UIModule 활성화시 연출
        protected abstract IEnumerator EnableUI();

        //UIModule 비활성화시 연출
        protected abstract IEnumerator DisableUI();

        // 버튼 Enter일 때 Scale 커지는거 말고 이 함수로 구현한다
        protected virtual void EachEnterFunction(Button hoverBtn) { }
        // 버튼 Exit일 때 Scale 커지는거 말고 이 함수로 구현한다
        protected virtual void EachExitFunction(Button hoverBtn) { }



        //UIModyule 생성초기화
        protected virtual void InitUIModule()
        {
            TargetCanvas.sortingOrder = DevDefine.SortingLayers.Small_Depth;
        }


    }//end of class					
}//end of namespace					