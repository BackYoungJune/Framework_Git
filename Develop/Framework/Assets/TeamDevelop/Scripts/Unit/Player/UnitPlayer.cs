/*********************************************************					
* UnitPlayer.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/
using Dev_System;
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
					
namespace Dev_Unit
{
    public struct PlayerAnimatorID
	{
        public int AnimID_MotionSpeed;
        public int AnimID_MoveSpeed;
        public int AnimID_Dead;
    }


    public class UnitPlayer : UnitBase					
	{
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------		

        protected override void InitUnit(UnitState state)
        {
            // UnitManager 캐싱
            Managers.Unit.AddUnit(this);

            //Player 상태 캐싱
            PlayerStateCache.Add(UnitState.None, null);
            PlayerStateCache.Add(UnitState.CutScene, new PlayerCutScene());
            PlayerStateCache.Add(UnitState.Idle, new PlayerIdle());
            PlayerStateCache.Add(UnitState.Move, new PlayerMove());
            StateMachine = new UnitStateMachine<UnitPlayer>(this, PlayerStateCache[state]);
            ChangeStateMachine(state);

        }

        public void ChangeStateMachine(UnitState state)
        {
            if (StateMachine == PlayerStateCache[state])
            {
                Debug.LogError("[UnitPlayer] 동일한 State로 변경 불가!");
                return;
            }

            StateMachine.InitState(PlayerStateCache[state]);
            curState = state;
        }

        public PlayerAnimatorID pAnimatorIDTable { get { return AnimatorIDTable; } }
        public Transform pPlayerCameraTarget { get { return playerCameraTarget; } }
        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        protected UnitStateMachine<UnitPlayer> StateMachine;
        protected Dictionary<UnitState, UnitInterface<UnitPlayer>> PlayerStateCache = new();
        protected AnimatorStateInfo AnimatorStateInfo;
        protected PlayerAnimatorID AnimatorIDTable;

        [Header("Setting")]
        [SerializeField] private Transform playerCameraTarget;

        [Header("---------- State Debug ----------")]
        [SerializeField] UnitState curState;

        protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {

        }


        protected virtual void Start()
        {

        }

        protected virtual void AssignAnimatorIDs()
        {
            AnimatorIDTable.AnimID_MoveSpeed = Animator.StringToHash("MovementSpeed");
            AnimatorIDTable.AnimID_MotionSpeed = Animator.StringToHash("MotionSpeed");
            AnimatorIDTable.AnimID_Dead = Animator.StringToHash("Dead");
        }

        protected virtual void Update()
        {
            if (StateMachine != null)
            {
                if (StateMachine.CurState != null)
                {
                    StateMachine.CurState.UpdateExcute();
                }
            }
        }


        protected virtual void OnDisable()
        {
            StopAllCoroutines();
            Managers.Unit.RemoveUnit(this);
        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            if (StateMachine.CurState != null)
            {
                StateMachine.CurState.OnTriggerEnter(col);
            }
        }

    }//end of class					
}//end of namespace					