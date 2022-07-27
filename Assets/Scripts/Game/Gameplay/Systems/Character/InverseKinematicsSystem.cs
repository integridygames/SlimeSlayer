using System.Collections.Generic;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Utils.Character.IK;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character 
{
    public class InverseKinematicsSystem : IInitializeSystem, IUpdateSystem
    {
        private readonly List<InverseKinematicsResolver> _controllerIK;

        public InverseKinematicsSystem(List<HandIKView> handIKViews, CharacterView characterView) 
        {
            _controllerIK = new List<InverseKinematicsResolver>();
            foreach (var handIKView in handIKViews)
                _controllerIK.Add(new InverseKinematicsResolver(characterView, handIKView));
        }

        public void Initialize()
        {
            foreach (var controllerIK in _controllerIK)
                controllerIK.InitIK();
        }

        public void Update()
        {
            foreach (var controllerIK in _controllerIK)
                controllerIK.ResolveIK();
        }
    }
}