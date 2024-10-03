/*********************************************************					
* PhaseManager.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.11.03 오후 2:09					
**********************************************************/

//using Dev_SceneManagement; // todo : SceneHandler 나오면 수정하기
using Dev_System;
using System;
using System.Collections;
using System.Collections.Generic;					
using UnityEngine;
using UnityEngine.Events;

namespace Dev_Phase
{					

	public class PhaseManager : MonoBehaviour					
	{
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------		
        public void InitRootPhase(RootPhase target)
        {
            //RootPhseIndex 업데이트 - 이전RootPhase와 targetRootPhase의 순서 비교

            //targetIndex
            int targetIndex = Array.IndexOf(DirectLinkRootPhaseList.ToArray(), target);
            //int resultIndex = targetIndex - RootPhaseIndex;


            //Debug.LogError(string.Format("목표인덱스 = {0} / 현재인덱스 =  {1}", targetIndex, RootPhaseIndex));

            if (target == CurRootPhase)
            {
                return;
            }
       

            if (target == null)
            {
                Debug.LogError("[PhaseManager] 타겟루트페이즈 is null");
                return;
            }

            if (CurRootPhase != null)
            {
                PrevRootPhase = CurRootPhase;
                if (CurRootPhase == target)
                {
                    Debug.LogError("[PhaseManager] 활성화 RootPhase 중복");
                    return;
                }
                else
                {
                    PrevRootPhase.gameObject.SetActive(false);
                }

            }

            //RootPhseIndex 업데이트
            RootPhaseIndex = targetIndex;


            CurRootPhase = target;
            CurRootPhase.gameObject.SetActive(true);
            SubPhaseIndex = 0;
            InitSubPhase();
        }

        public void InitRootPhase()
        {
            StartCoroutine(InitRootPhaseCor());
        }

        IEnumerator InitRootPhaseCor()
        {
            yield return new WaitUntil(() => Managers.AllCreateManagers);
            //yield return new WaitUntil(() => Managers.LoadingHandler.IsLoading == false);

            if (CurRootPhase == DirectLinkRootPhaseList[LastRootPhaseIndex])
            {
                Debug.LogError("[PhaseManager] 다이렉트링크 페이즈 init 오류");
                yield break;
            }

            if (CurRootPhase != null)
            {
                PrevRootPhase = CurRootPhase;
                CurRootPhase.gameObject.SetActive(false);
            }

            CurRootPhase = DirectLinkRootPhaseList[RootPhaseIndex];
            CurRootPhase.gameObject.SetActive(true);
            SubPhaseIndex = 0;
            InitSubPhase();
        }

        public void CompletePhaseBack()
        {
            if(CurRootPhase == null)
            {
                Debug.LogError("[PhaseManager] [ERROR]---------- 현재 설정된 Phase가 없습니다! ----------");
                return;
            }


            //서브페이즈가 있는경우 - 서브페이즈 뒤로...
            if (CurRootPhase.SubPhaseList.Contains(PrevSubPhase))
            {
                //처음 서브페이즈가 아니라면...
                if (CurSubPhase != CurRootPhase.SubPhaseList[StartPhaseIndex])
                {
                    SubPhaseIndex--;
                    PrevInitSubPhase();
                    return;
                }
            }

            //첫번째 루트페이즈일경우...
            if (CurRootPhase == DirectLinkRootPhaseList[StartPhaseIndex])
            {
                Debug.LogError("<color=#FF0000> 첫번쨰 Root Phase 입니다 </color>");
                return;
            }

            //메인페이즈를 뒤로 갱신
            SubPhaseIndex = 0;
            RootPhaseIndex--;
            PrevInitRootPhase();
        }


        public void CompletePhase()
        {
            if (CurRootPhase == null)
            {
                Debug.LogError("[PhaseManager] [ERROR]---------- 현재 설정된 Phase가 없습니다! ----------");
                return;
            }


            /*
            //subPhase가 있는경우 - subPhase 넘김
            if (CurRootPhase.SubPhaseList.Contains(CurSubPhase))
            {
                if (CurSubPhase != CurRootPhase.SubPhaseList[CurRootPhase.SubPhaseList.Count - 1])
                {
                    SubPhaseIndex++;
                    InitSubPhase();
                    Debug.Log("SubPhase 넘김");
                    return;
                }
            }

            //미리 설정된 루트페이즈가 있는 경우
            if (DirectLinkRootPhaseList.Count > 0)
            {
                //마지막 루트페이즈일때
                if (CurRootPhase == DirectLinkRootPhaseList[LastRootPhaseIndex])
                {
                    ResetPhase();
                    return;
                }

                SubPhaseIndex = 0;
                RootPhaseIndex++;
                InitRootPhase();
                Debug.Log("[PhaseManager] RootPhase 넘김");

            }
            //미리 설정된 루트페이즈가 없는 경우 -> 외부에서 InitRootPhase(RootPhase target)호출시 
            else
            {
                ResetPhase();
            }
            */

            //-------------------------------------------------------------------------------
            //서브페이즈가 있는경우 - 서브페이즈 넘김
            if (CurRootPhase.SubPhaseList.Contains(CurSubPhase))
            {
                //마지막 서브페이즈가 아닐때
                if (CurSubPhase != CurRootPhase.SubPhaseList[CurRootPhase.SubPhaseList.Count - 1])
                {
                    SubPhaseIndex++;
                    InitSubPhase();
                    ComplePhaseEvent?.Invoke();
                    return;
                }
            }

            //서브페이즈 없는경우 - 마지막 루트페이즈인경우
            if (CurRootPhase == DirectLinkRootPhaseList[LastRootPhaseIndex])
            {
                Debug.LogError("[PhaseManager] ----- 마지막 루트페이즈입니다 -----");
                CompleteAllPhaseEvent.Invoke();

                if (UseLoop)
                {
                    SubPhaseIndex = 0;
                    RootPhaseIndex = 0;
                    CurRootPhase.gameObject.SetActive(false);
                    CurRootPhase = DirectLinkRootPhaseList[RootPhaseIndex];
                    InitRootPhase();
                }

                //Debug.LogError("----- 루트페이즈 넘김 -----");
                ComplePhaseEvent?.Invoke();
                return;
            }


            //서브페이즈 없는경우 - 루트페이즈넘김
            SubPhaseIndex = 0;
            RootPhaseIndex++;
            InitRootPhase();
            ComplePhaseEvent?.Invoke();
            //Debug.Log("[PhaseManager] ----- PhaesManager Complete -----");
        }



        public BasePhase pCurRootPhase { get { return CurRootPhase; } }
        public UnityAction ComplePhaseEvent;

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        [SerializeField] private bool UseLoop = false;

        [Header("----------Phase Settings for Direct Link----------")]
        [SerializeField] private List<RootPhase> DirectLinkRootPhaseList = new List<RootPhase>();
        [Header("----------Unity Events----------")]
        [SerializeField] private UnityEvent CompleteAllPhaseEvent;


        private int LastRootPhaseIndex { get { return DirectLinkRootPhaseList.Count - 1; } }
        private int StartPhaseIndex { get { return 0; } }

        [Header("----------DEBUG----------")]
        [SerializeField] private int RootPhaseIndex = 0;
        [SerializeField] private RootPhase CurRootPhase = null;
        [SerializeField] private RootPhase PrevRootPhase = null;
        [SerializeField] private int SubPhaseIndex = 0;
        [SerializeField] private SubPhase CurSubPhase = null;
        [SerializeField] private SubPhase PrevSubPhase = null;
   
     

        void Update()
        {
            if (InputManager.Return_KeyDown)
            {
                CompletePhase();
                Debug.Log("[PhaseManager] 강제 퀘스트완료 디버그");

            }

            if (InputManager.Space_KeyDown)
            {
                CompletePhaseBack();
                Debug.Log("[PhaseManager] 강제 퀘스트 뒤로가기 디버그");

            }
        }


        void PrevInitRootPhase()
        {
            if (CurRootPhase != null)
            {
                int index = RootPhaseIndex - 1;

                //이전페이즈가 맨처음 페이즈일때 or index out of range..
                if (index < 0)
                {
                    index = 0;
                    RootPhaseIndex = index;
                }

                PrevRootPhase = DirectLinkRootPhaseList[index];

            }

            //현재 켜져있는 curRootPhase 비활성화
            CurRootPhase.gameObject.SetActive(false);

            //curRootPhase 갱신
            CurRootPhase = DirectLinkRootPhaseList[RootPhaseIndex];
            CurRootPhase.gameObject.SetActive(true);
            InitSubPhase();

            Debug.Log("[PhaseManager] 메인퀘스트 뒤로 갱신");
        }




        void InitSubPhase()
        {
            //subPhase 갱신
            if (CurSubPhase != null)
            {
                PrevSubPhase = CurSubPhase;
                PrevSubPhase.gameObject.SetActive(false);
            }

            //subPhase 갱신
            if (CurRootPhase.SubPhaseList.Count > 0)
            {
                CurSubPhase = CurRootPhase.SubPhaseList[SubPhaseIndex];
                CurSubPhase.gameObject.SetActive(true);
            }

        }

        void PrevInitSubPhase()
        {
            //prevSubPhase, curSubPhase 뒤로 갱신
            if (CurSubPhase != null)
            {
                int index = SubPhaseIndex - 1;

                if (index < 0)
                {
                    index = 0;
                    SubPhaseIndex = index;
                }
                PrevSubPhase = CurRootPhase.SubPhaseList[index];
            }

            //현재 활성화중인 subPhase 비활성화
            CurSubPhase.gameObject.SetActive(false);

            CurSubPhase = CurRootPhase.SubPhaseList[SubPhaseIndex];
            CurSubPhase.gameObject.SetActive(true);
            Debug.Log("[PhaseManager] 서브퀘스트 뒤로 갱신");
        }


        void ResetPhase()
        {
            SubPhaseIndex = 0;

            if (CurSubPhase != null)
                CurSubPhase.gameObject.SetActive(false);
            if (CurRootPhase != null)
                CurRootPhase.gameObject.SetActive(false);


            CurRootPhase = null;
            PrevRootPhase = null;
            CurSubPhase = null;
            PrevSubPhase = null;

            RootPhaseIndex = 0;
            SubPhaseIndex = 0;
            //TargetRootPhase = null;
            //TempTargetRootPhase = null;

            CompleteAllPhaseEvent.Invoke();
            Debug.LogError("[PhaseManager] RootPhase 끝남");
        }


    }//end of class					


}//end of namespace					