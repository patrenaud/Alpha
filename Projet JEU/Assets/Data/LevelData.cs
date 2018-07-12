using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{

	public List<EnemyAI> m_EnemyListData;

}
