/*********************************************************					
* VideoController.cs					
* �ۼ��� : SeoJin					
* �ۼ��� : 2022.11.03 ���� 1:47					
**********************************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Dev_Utils
{
    public class VideoController : MonoBehaviour
    {
        //--------------------------------------------------------					
        // �ܺ� ���� �Լ� & ������Ƽ					
        //--------------------------------------------------------				

        //�������
        public void OnPlayVideo_Inspector()
        {
            if (videoPlayer != null)
            {
                if (videoPlayer.clip != null)
                {
                    if (PlayVideoCor == null)
                    {
                        PlayVideoCor = StartCoroutine(PlayVideo());
                    }
                    //StartCoroutine("PlayVideo");
                }
            }

        }

        public void OnPlayVideo_Inspector(bool isLoop)
        {
            if (videoPlayer != null)
            {
                useLooping = isLoop;


                if (videoPlayer.clip != null)
                {
                    if (PlayVideoCor == null)
                    {
                        PlayVideoCor = StartCoroutine(PlayVideo());
                    }
                    //StartCoroutine("PlayVideo");
                }
            }
        }

        public void OnPlayVideo(VideoClip videoClip, bool useLoop)
        {
            if (videoPlayer != null)
            {
                useLooping = useLoop;
                videoPlayer.isLooping = useLoop;
                videoPlayer.clip = videoClip;

                if(PlayVideoCor != null)
                {
                    StopCoroutine(PlayVideoCor);
                    PlayVideoCor = null;
                }

                PlayVideoCor = StartCoroutine(PlayVideo());                                              
                //StartCoroutine("PlayVideo");          
            }
        }
        public void OnPlayVideo(VideoClip videoClip, bool useLoop, float targetFrame)
        {
            if (videoPlayer != null)
            {
                useLooping = useLoop;
                videoPlayer.isLooping = useLoop;
                videoPlayer.clip = videoClip;

                if (PlayVideoCor == null)
                {
                    JumpToTargetFrameInspector(targetFrame);
                    PlayVideoCor = StartCoroutine(PlayVideo());
                }
                //StartCoroutine("PlayVideo");
            }
        }

        private Coroutine PlayVideoCor = null;


        public void OnPauseVideo()
        {
            if (videoPlayer != null)
                videoPlayer.Pause();
        }
        public void OnPlayContinue()
        {
            if (videoPlayer != null)
                videoPlayer.Play();
        }
        public void OnStop()
        {
            if (videoPlayer != null)
            {
                videoPlayer.targetTexture.Release();
                videoPlayer.Stop();
                PlayVideoCor = null;
            }
        }
        public void ActiveVideoRenderer(bool isActive)
        {
            VideoRenderer.SetActive(isActive);
        }



        /// <summary>
        /// ���� ������ ����
        /// </summary>
        /// <param name="targetFrame"></param>
        public void JumpToTargetFrameInspector(float targetFrame)
        {
            if (videoPlayer.clip == null)
            {
                Debug.LogError("����Ŭ�� ����");
                return;
            }

            if (videoPlayer == null)
            {
                Debug.LogError("�����÷��̾� ����");
                return;
            }


            targetFrame = Mathf.Clamp(targetFrame, 1, (float)videoPlayer.frameCount);

            if (targetFrame < 1 || targetFrame > (float)videoPlayer.frameCount)
            {
                Debug.LogError("Target Frame Range Error!!");
                return;
            }

            Debug.Log("<color=#0054FF>-----Jump To Target Frame-----</color>");
            videoPlayer.frame = (long)targetFrame;
        }

        public bool IsVideoFinished
        {
            get
            {
                //if (CurFrame >= MaxFrameCount - 2)
                //{
                //	videoPlayer.Stop();
                //}

                if (videoPlayer.clip != null && videoPlayer.isPlaying)
                    return false;
                else
                    return true;

                //return CurFrame >= MaxFrameCount - 2 ? true : false;
            }
        }

        public bool CheckPauseStep(float pauseFrame)
        {
            if (pauseFrame <= 0) return true;

            return pauseFrame > 0 && CurFrame > pauseFrame ? true : false;
        }

        public bool IsTargetFrameCallback(float targetFrame)
        {
            EventTargetFrame = targetFrame;
            return IsTargetFrame;
        }

        public void ResetTargetFrame()
        {
            IsTargetFrame = false;

        }

        public void RenderTextureRelease()
        {

            videoPlayer.targetTexture.Release();
        }

        public float GetMiddleFrame { get { return MaxFrameCount * 0.5f; } }
        public float GetEndFrame { get { return MaxFrameCount - 1.0f; } }
        public float pCurFrame { get { return CurFrame; } }
        public float GetRunTime
        {
            get
            {
                if (videoPlayer == null || videoPlayer.clip == null)
                {
                    return 0;
                }
                return MaxFrameCount / videoPlayer.frameRate;
            }
        }


        public Action EndEventAction { get; set; }

        //--------------------------------------------------------					
        // ���� �ʵ� ����					
        //--------------------------------------------------------					
        [Header("[Settings]")]
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private GameObject VideoRenderer;

        [Header("[Debug]")]
        [SerializeField] private float MaxFrameCount;       //������ �ִ�������
        [SerializeField] private float CurFrame;            //���� ���������
        private float EventTargetFrame;                     //�̺�Ʈ ó���� ������
        private bool IsTargetFrame;                         //�̺�Ʈ ó���� �ο�
        private bool useLooping;
        private bool isEndEvent;



        //     void Start()
        //     {
        //videoPlayer.isLooping = false;
        //videoPlayer.loopPointReached += OnLoopReceived;
        //         videoPlayer.errorReceived += OnErrorReceived;
        //     }

        void Update()
        {
            if (videoPlayer != null && videoPlayer.clip != null)
            {
                GetFrameProgress();
            }

        }

        void OnDisable()
        {
            if (videoPlayer != null)
            {
                if (videoPlayer.clip != null)
                {
                    videoPlayer.Stop();
                }
            }

            MaxFrameCount = 0;
            StopAllCoroutines();
            PlayVideoCor = null;
        }


        IEnumerator PlayVideo()
        {
            //ù������ ���
            //yield return new WaitForEndOfFrame();
            yield return null;

            MaxFrameCount = 0;
            CurFrame = 0;
            EventTargetFrame = 0;
            ResetTargetFrame();


            //���� ���
            if (videoPlayer != null)
            {
                if (videoPlayer.clip != null)
                    videoPlayer.Play();
            }

        }

        IEnumerator PlayVideoTargetFrame(float targetFrame)
        {
            //ù������ ���
            //yield return new WaitForEndOfFrame();
            yield return null;

            MaxFrameCount = 0;
            CurFrame = 0;
            EventTargetFrame = 0;
            ResetTargetFrame();


            //���� ���
            if (videoPlayer != null)
            {
                if (videoPlayer.clip != null)
                    videoPlayer.Play();
            }

        }




        void GetFrameProgress()
        {

            if (videoPlayer.frameCount > 0)
            {
                MaxFrameCount = (float)videoPlayer.frameCount;
                CurFrame = videoPlayer.frame;


                if (EventTargetFrame > 0)
                {
                    if (CurFrame >= MaxFrameCount - EventTargetFrame)
                    {
                        if (IsTargetFrame == false)
                        {
                            IsTargetFrame = true;
                        }

                    }
                }


                if(CurFrame <= 10)
                {
                    if (isEndEvent)
                    {
                        isEndEvent = false;
                    }
                }


                if (CurFrame >= MaxFrameCount - 2)
                {

                    if (useLooping)
                    {
                        if(isEndEvent == false)
                        {
                            isEndEvent = true;
                            EndEventAction?.Invoke();
                        }
                    }
                    else
                    {
#if UNITY_EDITOR
                        Debug.LogError("<b><color=#FF0000> [VideoController] ���� ������ ����! </color></b>");
#endif


                        videoPlayer.Stop();
                        EndEventAction?.Invoke();
                        PlayVideoCor = null;
                    }

                }

            }
        }



        //void OnErrorReceived(VideoPlayer player, string message)
        //{
        //	Debug.LogError(string.Format("[VideoController] Video Error = {0}", message));

        //          player.Stop();
        //      }

        //void OnLoopReceived(VideoPlayer player)
        //{
        //	player.Stop();
        //      }



    }//end of class					


}//end of namespace					