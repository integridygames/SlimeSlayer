using Game.Gameplay.Views.Character.Bones;
using Game.Gameplay.Views.Character.Targets;
using Game.Gameplay.Views.Character.Poles;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Controllers.Character.IK 
{
    public class InverseKinematicsRightHandController : InverseKinematicsController<RightHandTargetView, RightHandBoneView, RightHandPoleView, CharacterView>
    {
        public InverseKinematicsRightHandController(CharacterView characterView, RightHandTargetView rightHandTargetView, 
            RightHandBoneView rightHandBoneView, RightHandPoleView rightHandPoleView) : base(characterView, rightHandTargetView, rightHandBoneView, rightHandPoleView) 
        {
            
        }
    }
}