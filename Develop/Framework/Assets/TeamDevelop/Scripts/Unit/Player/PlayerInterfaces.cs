/*********************************************************					
* PlayerInterfaces.cs					
* 작성자 : #AUTHOR#					
* 작성일 : #DATE#					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;					
					
namespace Dev_Unit
{
    public class PlayerCutScene : UnitInterface<UnitPlayer>
    {
        private UnitPlayer CurPlayer;

        public void Enter(UnitPlayer unit)
        {
            CurPlayer = unit;
        }

        public void UpdateExcute()
        {

        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider col)
        {


        }
    }//end of class

    public class PlayerIdle : UnitInterface<UnitPlayer>
    {
        private UnitPlayer CurPlayer;

        public void Enter(UnitPlayer unit)
        {
            CurPlayer = unit;
        }

        public void UpdateExcute()
        {

        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider col)
        {


        }
    }//end of class

    public class PlayerMove : UnitInterface<UnitPlayer>
    {
        private UnitPlayer CurPlayer;

        public void Enter(UnitPlayer unit)
        {
            CurPlayer = unit;
        }

        public void UpdateExcute()
        {

        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider col)
        {


        }
    }//end of class



}//end of namespace					