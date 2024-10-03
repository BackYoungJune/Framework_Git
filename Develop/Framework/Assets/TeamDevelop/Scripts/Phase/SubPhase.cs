/*********************************************************					
* SubPhase.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.11.03 오후 2:17					
**********************************************************/
using System.Collections;									
using UnityEngine;
using UnityEngine.Events;

namespace Dev_Phase
{					
	public class SubPhase : BasePhase
	{
		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------				

		//--------------------------------------------------------					
		// 내부 필드 변수					
		//--------------------------------------------------------					
		[SerializeField] private float			EnableWaitTime = 0;
		[SerializeField] private UnityEvent		EnableEvent;
		[SerializeField] private float			StartWaitTime = 0;
		[SerializeField] private UnityEvent		StartEvent;
		[SerializeField] private UnityEvent		DisableEvent;

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