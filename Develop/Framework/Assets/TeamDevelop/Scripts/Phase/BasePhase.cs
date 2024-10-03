/*********************************************************					
* BasePhase.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.11.03 오후 2:09					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
							
namespace Dev_Phase
{					
	public abstract class BasePhase : MonoBehaviour					
	{
		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------					
		public string pPhaseDiscription { get { return PhaseDiscription; } }

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------		

        [TextArea(3, 15), SerializeField]
		protected string PhaseDiscription;

      



		protected abstract void OnEnable();

		protected abstract IEnumerator Start();




    }//end of class					
					
					
}//end of namespace					