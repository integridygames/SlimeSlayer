using System.Linq;
using UnityEngine;

namespace Game.DataBase.FX
{
    [CreateAssetMenu(fileName = "RecyclableParticlesDataBase", menuName = "ScriptableObjects/RecyclableParticlesDataBase")]
    public class RecyclableParticlesDataBase : AbstractDataBase<RecyclableParticleRecord, RecyclableParticleType>
    {
        public override RecyclableParticleRecord GetRecordByType(RecyclableParticleType recordType)
        {
            return Records.First(x => x._recyclableParticleType == recordType);
        }
    }
}