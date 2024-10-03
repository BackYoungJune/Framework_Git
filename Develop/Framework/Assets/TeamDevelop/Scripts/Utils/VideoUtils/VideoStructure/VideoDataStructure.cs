/*********************************************************					
* VideoDataStructure.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using System;


namespace Dev_VideoUtils
{

    [CreateAssetMenu(fileName = "VideoDataStructure_", menuName = "#ScriptableObject/#VideoStructure")]
    public class VideoDataStructure : ScriptableObject
    {

		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------					
		public void Initializing()
		{
			if(VideoDataTable.Count > 0)
			{
				VideoDataTable.Clear();
            }

            pCurVideoRef = GetVideoRefList();

            for (int i = 0; i < pCurVideoRef.Count; i++)
            {
                if (VideoDataTable.ContainsKey(pCurVideoRef[i].pUniqueID))
                {

                    Debug.LogError(string.Format("VideoStructure Resource 중복 오류 = {0}", pCurVideoRef[i].pUniqueID));
                }
                else
                {

                    VideoDataTable.Add(pCurVideoRef[i].pUniqueID, pCurVideoRef[i].VideoClipSource);

#if UNITY_EDITOR
                    Debug.Log(string.Format("<color=#1DDB16> Add VideoDatabase Success = {0} </color>", pCurVideoRef[i].pUniqueID));
#endif

                }
            }
        }

        public VideoRefBase GetVideoRefTable(string keyId)
        {
            return Array.Find(pCurVideoRef.ToArray(), element => element.pUniqueID == keyId);
        }


        public void DeleteVideoRefTable()
        {
            VideoDataTable.Clear();
        }




        public VideoRefType                         pVideoRef { get { return VideoRef; } }
		public int                                  pRefCount { get { return RefCount; } set { RefCount = value; } }
        public List<VideoRef_Common>                pRef_Common { get { return Ref_Common; } set { Ref_Common = value; } }
       



        public Dictionary<string, VideoClip>        VideoDataTable = new();
        public List<VideoRefBase>                   pCurVideoRef = new();
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        [SerializeField] private VideoRefType				    VideoRef;
        [SerializeField] private int						    RefCount;
        [SerializeField] private List<VideoRef_Common>          Ref_Common;
		



		List<VideoRefBase> GetVideoRefList()
		{
            switch (VideoRef)
            {
                case VideoRefType.Common:
                    return Ref_Common.Cast<VideoRefBase>().ToList();

                default:
                    return null;
            }



        }






    }//end of class					


}//end of namespace