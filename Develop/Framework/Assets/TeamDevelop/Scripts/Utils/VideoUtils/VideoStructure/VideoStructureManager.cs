/*********************************************************					
* VideoStructureManager.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;
using System;
			
namespace Dev_VideoUtils
{					
	public class VideoStructureManager : MonoBehaviour					
	{

		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------					
		public VideoDataStructure GetVideoDataStructure(VideoRefType refType)
		{
			return Array.Find(VideoDataStructures, element => element.pVideoRef == refType);
		}






        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------				
        [SerializeField] private VideoDataStructure[] VideoDataStructures;



        void Awake()
        {
			for (int i = 0; i < VideoDataStructures.Length; i++)
			{
				VideoDataStructures[i].Initializing();
            }

        }


        void OnDestroy()
        {
			for (int i = 0; i < VideoDataStructures.Length; i++)
			{
				VideoDataStructures[i].DeleteVideoRefTable();
            }
         
        }







    }//end of class					
					
					
}//end of namespace					