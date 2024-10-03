/*********************************************************					
* UnitDataTable.cs					
* 작성자 : SeoJin					
* 작성일 : 2022.12.18 오후 6:12					
**********************************************************/					
namespace Dev_Unit
{
	public enum UnitClass
	{
		None, 
		Player, 
		NPC, 
		Enemy,
		Exercise,
		Trainer,
	}

	public enum UnitUniqueID
	{
		None,

		//----- Player -----
		Player_Male,
		Player_Female,
		Player,
		Player_VR,

		//----- NPC -----
		NPC_Shong,
        NPC_Aengdu,

        //----- Enemy -----
        Enemy_Mini,
		Enemy_MinJi,
		

		//----- Exercise ----
		Exercise_Carrot,
		
		//----- Trainer -----
		Trainer_Fitness,

    }


    public static class UnitDataTable
    {

		public static UnitClass GetUnitClass(UnitUniqueID uniqueId)
		{
			string uniqueStr = uniqueId.ToString();

            // uniqueID의 첫번째 _까지 짤라서 사용, 뒤에서부터 찾으려면 LastIndexOf사용
            int index = uniqueStr.IndexOf("_");
            uniqueStr = uniqueStr.Substring(0, index);

            return DevUtils.StringToEnum<UnitClass>(string.Format("{0}", uniqueStr));
        }
    }
}//end of namespace					