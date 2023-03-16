using System.Collections.Generic;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Utils.Character.IK;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character.IK
{
    public class InverseKinematicsSystem : IUpdateSystem
    {
        private readonly List<InverseKinematicsResolver> _controllerIK;

        public InverseKinematicsSystem(List<HandIKView> handIKViews, HeapInfo heapInfo)
        {
            _controllerIK = new List<InverseKinematicsResolver>();
            foreach (var handIKView in handIKViews)
                _controllerIK.Add(new InverseKinematicsResolver(handIKView, heapInfo));
        }

        public void Update()
        {
            foreach (var controllerIK in _controllerIK)
                controllerIK.ResolveIK();
        }
    }
}