using Game.Gameplay.Views.Character;
using UnityEngine;
using Game.Gameplay.Views.Character.Targets;
using Game.Gameplay.Views.Character.Poles;
using Game.Gameplay.Views.Character.Bones;

namespace Game.Gameplay.Utils.Character.IK 
{
    public class InverseKinematicsResolver
    {
        protected readonly CharacterView CharacterView;
        protected readonly HandTargetView HandTargetView;
        protected readonly HandBoneView HandBone;
        protected readonly HandPoleView HandPole;

        protected Transform[] Bones;
        protected Vector3[] Positions;
        protected float[] BonesLength;
        protected float CompleteLength;

        //start states
        protected Vector3[] StartDirections;
        protected Quaternion[] StartRotationBones;
        protected Quaternion StartRotationTarget;
        protected Quaternion StartRotationRoot;

        //parameters
        protected const int ChainLength = 2;
        protected const int Iterations = 10;
        protected const float Delta = 0.001f;

        protected int ChainLengthPlusOne = ChainLength + 1;

        public InverseKinematicsResolver(CharacterView characterView, HandIKView handIKView)
        {
            this.CharacterView = characterView;
            this.HandTargetView = handIKView.HandTargetView;
            HandBone = handIKView.HandBoneView;
            HandPole = handIKView.HandPoleView;
        }

        public void InitIK() 
        {
            Bones = new Transform[ChainLengthPlusOne];
            Positions = new Vector3[ChainLengthPlusOne];
            BonesLength = new float[ChainLength];
            CompleteLength = 0;

            StartDirections = new Vector3[ChainLengthPlusOne];
            StartRotationBones = new Quaternion[ChainLengthPlusOne];
            StartRotationTarget = this.HandTargetView.transform.rotation;
            StartRotationRoot = HandBone.transform.rotation;

            var current = HandBone.transform;

            for (int i = Bones.Length - 1; i >= 0; i--)
            {
                Bones[i] = current;
                StartRotationBones[i] = current.rotation;

                if (i == Bones.Length - 1)
                {
                    StartDirections[i] = this.HandTargetView.transform.position - current.position;
                }
                else
                {
                    StartDirections[i] = Bones[i + 1].position - current.position;
                    BonesLength[i] = StartDirections[i].magnitude;
                    CompleteLength += BonesLength[i];
                }

                current = current.parent;
            }
        }

        public void ResolveIK() 
        {
            if (this.HandTargetView == null)
                return;

            if (BonesLength.Length != ChainLength)
                InitIK();

            for (int i = 0; i < Bones.Length; i++)
                Positions[i] = Bones[i].position;

            Quaternion handRotation = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
            Quaternion handRotationDiff = handRotation * Quaternion.Inverse(StartRotationRoot);


            if ((this.HandTargetView.transform.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
            {
                var direction = (this.HandTargetView.transform.position - Positions[0]).normalized;
                for (int i = 1; i < Positions.Length; i++)
                    Positions[i] = Positions[i - 1] + direction * BonesLength[i - 1];
            }
            else 
            {
                for(int iteration = 0; iteration < Iterations; iteration++) 
                {
                    for (int i = Positions.Length - 1; i > 0; i--)
                    {
                        if (i == Positions.Length - 1)
                            Positions[i] = this.HandTargetView.transform.position;
                        else
                            Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BonesLength[i];
                    }

                    for (int i = 1; i < Positions.Length; i++)
                        Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i - 1]).normalized * BonesLength[i - 1];

                    if ((Positions[Positions.Length - 1] - this.HandTargetView.transform.position).sqrMagnitude < Delta * Delta)
                        break;
                }
            }

            if (HandPole != null)
            {
                for (int i = 1; i < Positions.Length - 1; i++)
                {
                    var plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                    var projectedPole = plane.ClosestPointOnPlane(HandPole.transform.position);
                    var projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                    var angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                    Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i - 1]) + Positions[i - 1];
                }
            }

            for (int i = 0; i < Positions.Length; i++) 
            {
                if (i == Positions.Length - 1)
                    Bones[i].rotation = this.HandTargetView.transform.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBones[i];
                else
                    Bones[i].rotation = Quaternion.FromToRotation(StartDirections[i], Positions[i + 1] - Positions[i]) * StartRotationBones[i];

                Bones[i].position = Positions[i];
            }               
        }
    }
}