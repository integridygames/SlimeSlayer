using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects.Substructure;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "EssenceDataBase", menuName = "ScriptableObjects/EssenceDataBase")]
    public class EssenceDataBase : PrefabsDataBase<EssenceRecord, EssenceType, EssenceView>
    {      
       
    }
}