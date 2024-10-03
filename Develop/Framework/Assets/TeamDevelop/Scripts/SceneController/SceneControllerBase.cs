/*********************************************************					
* SceneControllerBase.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using System.Collections;					
using UnityEngine;					
					
namespace Dev_SceneControl
{					
	public abstract class SceneControllerBase : MonoBehaviour					
	{
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------					


        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        protected virtual void Awake()
        {
            Init();
        }

        protected abstract IEnumerator Init();

        protected abstract void ResetScene();

    }//end of class								
}//end of namespace					