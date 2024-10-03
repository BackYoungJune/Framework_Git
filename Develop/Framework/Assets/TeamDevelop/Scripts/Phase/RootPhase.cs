/*********************************************************					
* RootPhase.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.11.03 오후 2:09					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;
using UnityEngine.Events;

namespace Dev_Phase
{					
	public class RootPhase : BasePhase
	{
		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------				
		[HideInInspector] public List<SubPhase> SubPhaseList = new List<SubPhase>();


		//--------------------------------------------------------					
		// 내부 필드 변수					
		//--------------------------------------------------------					

		[SerializeField] private float			EnableWaitTime = 0;
		[SerializeField] private UnityEvent		EnableEvent;
		[SerializeField] private float			StartWaitTime = 0;
		[SerializeField] private UnityEvent		StartEvent;
		[SerializeField] private UnityEvent		DisableEvent;


		void Awake()
		{
			if (SubPhaseList.Count <= 0)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					if (transform.GetChild(i).GetComponent<SubPhase>() != null)
						SubPhaseList.Add(transform.GetChild(i).GetComponent<SubPhase>());
				}
			}

		}


        protected override void OnEnable()
        {
			StartCoroutine(IOnEnable());
		}


		IEnumerator IOnEnable()
		{
			yield return new WaitForSeconds(EnableWaitTime);
			EnableEvent.Invoke();
		}


        protected override IEnumerator Start()
        {
			yield return new WaitForSeconds(StartWaitTime);
			StartEvent.Invoke();
		}


		void OnDisable()
		{
			StopAllCoroutines();
			DisableEvent.Invoke();
		}



	}//end of class					


}//end of namespace					