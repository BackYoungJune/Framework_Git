using System.Collections;
using UnityEngine;
using Dev_Unit;
using Dev_Data;
//using Dev_Gesture;
using UnityEngine.SceneManagement;
using Dev_UI;
using Dev_VideoUtils;


namespace Dev_System
{
    public enum Manager_UniqueID
    {
        None,
        GameManager,
        UnitManager,
        ObjectPoolManager,
        SoundManager,
        DataManager,
        GestureManager,
        UIModuleManager,
        UserStatusManager,
        VideoStructureManager
    }

    public class Managers : MonoBehaviour
    {
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------		

        // ------------------ 생성형 Manager들------------------------- 
        public static Managers Instance { get {  return instance; } }
        public static GameManager Game { get { return Instance?.game; } }
        public static UnitManager Unit { get { return Instance?.unit; } }
        public static ObjectPoolManager ObjectPool { get { return Instance?.objectPool; } }
        public static SoundManager Sound { get { return Instance?.sound; } }
        public static DataManager Data { get { return Instance?.data; } }
       // public static GestureManager Gesture { get { return Instance?.gesture; } }
        public static UIModuleManager UIManager { get { return Instance?.uiModuleManager; } }
        public static VideoStructureManager VideoManager { get { return Instance?.videoStructureManager; } }

        // ------------------ 생성하지않는 Manager들-------------------- 


        // ------------------ Managers의 변수들 ------------------------
        public static bool Initialized { get; set; } = false;
        public static bool AllCreateManagers = false;

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------	
        [SerializeField] private ManagerDataTable managerDataTable;

        // ------------------ 생성형 Manager들------------------------- 
        private static Managers instance;
        private GameManager game;
        private UnitManager unit;
        private ObjectPoolManager objectPool;
        private SoundManager sound;
        private DataManager data;
        //private GestureManager gesture;
        private UIModuleManager uiModuleManager;
        private VideoStructureManager videoStructureManager;

        void Awake()
        {
            if(instance == null && Initialized == false)
            {
                Initialized = true;
                StartCoroutine(Init());
            }
        }

        void Start()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Managers.Gesture.ResetGestureActions();
        }

        IEnumerator Init()
        {
            AllCreateManagers = false;
            yield return  StartCoroutine(CrateManager());
            AllCreateManagers = true;
        }

        IEnumerator CrateManager()
        {
            if (instance == null)
            {
                var manager = FindObjectOfType<Managers>();
                if (manager == null)
                {
                    GameObject obj = new GameObject("#Managers");
                    manager = obj.AddComponent<Managers>();
                }
                instance = manager;
                DontDestroyOnLoad(instance);
            }
            yield return new WaitUntil(() => instance != null);

            foreach (var info in managerDataTable.pManagerInfos)
            {
                GameObject obj = Instantiate(info.Prefab);
                obj.transform.SetParent(this.transform);

                switch (info.ID)
                {
                    case Manager_UniqueID.GameManager:
                        {
                            game = obj.GetComponent<GameManager>();
                            break;
                        }
                    case Manager_UniqueID.UnitManager:
                        {
                            unit = obj.GetComponent<UnitManager>();
                            break;
                        }
                    case Manager_UniqueID.ObjectPoolManager:
                        {
                            objectPool = obj.GetComponent<ObjectPoolManager>();
                            break;
                        }
                    case Manager_UniqueID.SoundManager:
                        {
                            sound = obj.GetComponent<SoundManager>();
                            break;
                        }
                    case Manager_UniqueID.DataManager:
                        {
                            data = obj.GetComponent<DataManager>();
                            break;
                        }
                    //case Manager_UniqueID.GestureManager:
                    //    {
                    //        gesture = obj.GetComponent<GestureManager>();
                    //        break;
                    //    }
                    case Manager_UniqueID.UIModuleManager:
                        {
                            uiModuleManager = obj.GetComponent<UIModuleManager>();
                            break;
                        }
                    case Manager_UniqueID.VideoStructureManager:
                        {
                            videoStructureManager = obj.GetComponent<VideoStructureManager>();
                            break;
                        }
                }
            }
            yield return null;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}