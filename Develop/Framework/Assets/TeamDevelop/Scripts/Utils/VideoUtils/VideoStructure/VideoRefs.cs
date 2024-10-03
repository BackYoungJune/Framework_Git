/*********************************************************					
* VideoRefs.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;
using System;
using UnityEngine.Video;

namespace Dev_VideoUtils					
{


    //-------------------------------------------------------------					
    //          비디오구분 : 최상위카테고리
    //-------------------------------------------------------------	
    public enum VideoRefType
    {
        None,
        Common,
        LoadingIcon,
    }

    //-------------------------------------------------------------					
    //          비디오카테고리 : Common
    //-------------------------------------------------------------	
    public enum VideoCategory_Common
    {
        None,
        Openning,
    }


    //-------------------------------------------------------------					
    //          VideoRefs 클래스
    //-------------------------------------------------------------	
    [Serializable]
    public abstract class VideoRefBase
	{
        public string       Description;
        public VideoClip    VideoClipSource;

        public abstract string pUniqueID { get; }

    }


    [Serializable]
    public class VideoRef_Common : VideoRefBase
    {
        public VideoCategory_Common UniqueID;

        public override string pUniqueID { get { return UniqueID.ToString(); } }

    }

}//end of namespace					