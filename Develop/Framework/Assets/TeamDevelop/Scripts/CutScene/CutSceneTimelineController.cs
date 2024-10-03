/*********************************************************					
* CutSceneTimelineController.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using UnityEngine;
//using PixelCrushers.DialogueSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Dev_Unit;
using System;
using static DevDefine;
using Dev_System;

namespace Dev_CutScene			
{
    public class CutSceneTimelineController : MonoBehaviour
    {
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------					
        public void PlayCutSceneInspector(string key)
        {
            CutSceneTimeLineEnum enumKey = (CutSceneTimeLineEnum)System.Enum.Parse(typeof(CutSceneTimeLineEnum), key);
            if (System.Enum.IsDefined(typeof(CutSceneTimeLineEnum), enumKey) == false)
            {
                Debug.LogError("key != enum's data");
                return;
            }

            var info = System.Array.Find(CutSceneTable.TimeLineInfos, x => x.enumKey == enumKey);
            if (info != null)
            {
                timelineTrigger.pPlayableDirector.playableAsset = info.playableAsset;
                timelineTrigger.PlayCutScene();

                for (int i = 0; i < info.InfoDetails.Length; i++)
                {
                    PDBind(timelineTrigger.GetComponent<PlayableDirector>(), info.InfoDetails[i].trackNames, ReturnAnimator(info.InfoDetails[i].animator));
                }
            }
        } 

        Animator ReturnAnimator(CutSceneAnimator animator)
        {
            // UnitManager에서 해당 애니메이터 넣기
            switch (animator)
            {
                //case CutSceneAnimator.Player: return Managers.Unit.GetCurTurnUnit.pAnimatorbase;
                //case CutSceneAnimator.FollowNPC: return Managers.Unit.GetCurFollowNPCUnit.nAnimatorbase;
                //case CutSceneAnimator.SubFollowNPC: return Managers.Unit.GetCurSubFollowNPCUnit.nAnimatorbase;
                //case CutSceneAnimator.Enemy: return Managers.Unit.GetCurTargetUnit.pAnimatorbase;
            }
            return null;
        }

        public void PDBind(PlayableDirector director, string trackName, Animator animator)
        {
            var timeline = director.playableAsset as TimelineAsset;
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == trackName)
                {
                    director.SetGenericBinding(track, animator);
                }
            }
        }

        public void CutSceneSkip()
        {
            if (timelineTrigger.pPlayableDirector.state == PlayState.Playing)
            {
                timelineTrigger.StopCutScene();
                // 다이얼로그가 있으면 StopConversation을 넣어준다
                // Dev_Dialogue.DevDialogueManager.Controller.StopConversation();
            }
        }

        //--------------------------------------------------------					
        // 배틀 턴오버 컷씬					
        //--------------------------------------------------------	


        public bool PlayCutScene { get; set; }

        public Action BattleCutSceneEvent;

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					

        [SerializeField] private CutSceneTimelineTable CutSceneTable;
        [SerializeField] private DevTimelineTrigger timelineTrigger;


    }//end of class				
}//end of namespace					