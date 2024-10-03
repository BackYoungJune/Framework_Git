/*********************************************************					
* UnitManager.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.11.03 오후 4:52					
**********************************************************/								
using System.Collections.Generic;					
using UnityEngine;
using System;

namespace Dev_Unit
{
	public class UnitManager : MonoBehaviour					
	{
		//--------------------------------------------------------					
		// 외부 참조 함수 & 프로퍼티					
		//--------------------------------------------------------		
		
		/// <summary>
		/// 유닛 생성 함수
		/// </summary>
		/// <param name="uniqueId">생성하려는 유닛 이름</param>
		/// <param name="genpos">생성위치</param>
		/// <param name="isParent">생성위치의 자식으로 넣을까?</param>
		public void CreateUnit(UnitUniqueID uniqueId, UnitState state, Transform genpos, bool isParent, Vector3 scale, bool revertGender = false)
        {
			UnitClass targetClass = UnitDataTable.GetUnitClass(uniqueId);
			UnitScriptableObject targetScriptable = GetUnitScriptable(targetClass);

			//유닛 생성
			targetScriptable.CreateUnitObject(uniqueId, state, genpos, isParent, scale, revertGender);
		}


		//유닛 생성시 등록 함수
		public void AddUnit(UnitBase unit)
        {
			// List Add
		}


		//유닛 사망시 등록해제 함수
		public void RemoveUnit(UnitBase unit)
        {
			// Contain List Remove
		}


		//특정 Table의 유닛 전체 등록 해제 함수
		public void RemoveAllUnit(UnitClass unitClass)
        {
            switch (unitClass)
            {
                case UnitClass.Player:
					UnitPlayerList.Clear();
					break;
			}
        }

        public UnitPlayer GetCurPlayerUnit
        {
            get
            {
                if (UnitPlayerList.Count <= 0)
                {
#if UNITY_EDITOR
                    Debug.Log("[UnitManager] 대기중인 UnitUnitTrainer 없음");
#endif
                    return null;
                }
                return (UnitPlayer)UnitPlayerList[0];
            }
        }

        public UnitScriptableObject GetUnitScriptable(UnitClass unitClass)
        {
			return Array.Find(UnitScriptables, element => element.pUnitClassType == unitClass);
        }

		public int CurTurnUnitIndex { get; set; }
		public int CurTurnExerciseUnitIndex { get; set; }
		//--------------------------------------------------------					
		// 내부 필드 변수					
		//--------------------------------------------------------		

		[Header("---------- Setting Unit ScriptableObjects ----------")]
		[SerializeField] private UnitScriptableObject[] UnitScriptables;


		[Header("----------All Playable Unit List----------")]
		public List<UnitBase> UnitPlayerList = new();





		void Awake()
        {
			if(UnitScriptables.Length <= 0)
            {
                Debug.LogError("[UnitManager] UnitScriptables 세팅하세요");
            }
		}

	


    }//end of class					


}//end of namespace					