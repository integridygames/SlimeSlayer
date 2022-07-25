using Game.Gameplay.Views.Character.Bones;
using Game.Gameplay.Views.Character.Targets;
using Game.Gameplay.Views.Character.Poles;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Controllers.Character.IK 
{
    public class InverseKinematicsLeftHandController : InverseKinematicsController<LeftHandTargetView, LeftHandBoneView, LeftHandPoleView, CharacterView>
    {       
        public InverseKinematicsLeftHandController(CharacterView characterView, LeftHandTargetView leftHandTargetView, 
            LeftHandBoneView leftHandBoneView, LeftHandPoleView leftHandPoleView) : base(characterView, leftHandTargetView, leftHandBoneView, leftHandPoleView)
        {

        }
    }
}