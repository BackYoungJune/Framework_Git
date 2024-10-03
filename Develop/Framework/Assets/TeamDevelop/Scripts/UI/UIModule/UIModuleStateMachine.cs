/*********************************************************					
* UIModuleStateMachine.cs					
* 작성자 : SeoJin					
* 작성일 : 2024.06.25 오후 4:59					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
					

namespace Dev_UI
{				
	public interface UIInterface<T>
	{
		void Enter(T uiModule);
		void UpdateExcute();
		void Exit();
	}

	public enum UIStates
	{
		None,
		Fixed,
		FollowerVR
	}



	public class UIModuleStateMachine<T>		
	{
        public UIInterface<T> CurState { get { return curState; } set { curState = value; } }


        private UIInterface<T> curState;
        private T Sender;


        public UIModuleStateMachine(T sender, UIInterface<T> state)
		{
			Sender = sender;
			InitState(state);
		}


        public void InitState(UIInterface<T> newState)
		{
			if (Sender == null)
			{
				Debug.LogError("[UIModule State Machine] Sender is null");
				return;
			}

			if (curState == newState)
			{
				return;
			}

			if(curState != null)
			{
				curState.Exit();
			}

			curState = newState;
			curState.Enter(Sender);
		}


    }//end of class					
					
					
}//end of namespace					