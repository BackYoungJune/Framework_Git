/*********************************************************					
* UIModuleInterfaces.cs					
* 작성자 : SeoJin					
* 작성일 : 2024.06.25 오후 4:58					
**********************************************************/					
using System.Collections;					
using System.Collections.Generic;					
using UnityEngine;
using Dev_System;


namespace Dev_UI
{


    public class UIInterface_Fixed : UIInterface<UIModule>
    {
        private UIModule CurUIModule;


        public void Enter(UIModule uiModule)
        {
            CurUIModule = uiModule;
        }

        public void UpdateExcute()
        {

        }

        public void Exit()
        {

        }
      
    }


}//end of namespace					