/*********************************************************					
* DevTimelineTrigger.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dev_CutScene
{					
	public class DevTimelineTrigger : MonoBehaviour					
	{					
		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------					
		public void PlayCutScene()
		{
			SetBindings();
            PlayableDirector.Play();
			CutSceneStartEvent?.Invoke();
			startCutScene = true;
        }

		public void StopCutScene()
		{
			PlayableDirector.Stop();
			CutSceneEndEvent?.Invoke();
        }

		public PlayableDirector pPlayableDirector { get { return PlayableDirector; } }
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        [Header("------------ Director ------------------")]
		[SerializeField] private PlayableDirector PlayableDirector;
		[SerializeField] private TimelineAsset TimelineAsset;

        [Header("------------ Event ------------------")]
        [SerializeField] private UnityEvent CutSceneStartEvent;
		[SerializeField] private UnityEvent CutSceneEndEvent;

        [Header("------------ Bind ------------------")]
        [Tooltip("(Optional) Bind these GameObjects to the Timeline's tracks.")]
        [SerializeField] List<GameObject> bindings = new List<GameObject>();

        private bool startCutScene = false;

        void Awake()					
		{
			if (PlayableDirector == null) PlayableDirector = GetComponent<PlayableDirector>();
            if (PlayableDirector == null && TimelineAsset != null) PlayableDirector = gameObject.AddComponent<PlayableDirector>();
            if (PlayableDirector != null && PlayableDirector.playableAsset == null) PlayableDirector.playableAsset = TimelineAsset;
        }					
							
		void SetBindings()					
		{				
			var timelineAsset = PlayableDirector.playableAsset as TimelineAsset;
			if (timelineAsset == null) return;
			for(var i = 0; i < bindings.Count; i++)
			{
				if (bindings[i] != null)
				{
					var track = timelineAsset.GetOutputTrack(i);
					if(track != null)
					{
						PlayableDirector.SetGenericBinding(track, bindings[i]);
					}
				}
			}
		}

		void Update()
		{
			if (PlayableDirector.state == PlayState.Paused && startCutScene == true)
			{
				CompleteCutScene();
				startCutScene = false;
            }
		}

		void CompleteCutScene()
		{
			CutSceneEndEvent?.Invoke();
		}

	}//end of class					
}//end of namespace					