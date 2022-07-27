using System.Collections.Generic;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Controllers.Character.IK;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character 
{
    public class InverseKinematicsSystem : IUpdateSystem
    {
        private List<HandIKView> _handIKViews;
        private List<InverseKinematicsController> _controllerIK;

        public InverseKinematicsSystem(List<HandIKView> handIKViews, CharacterView characterView) 
        {
            _handIKViews = handIKViews;
            _controllerIK = new List<InverseKinematicsController>();
            foreach(var handIKView in _handIKViews)            
                _controllerIK.Add(new InverseKinematicsController(characterView, handIKView));

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