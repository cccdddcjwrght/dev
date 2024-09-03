using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
namespace SGame.VS
{
    /// <summary>
    /// 角色移动并等待解锁
    /// </summary>
    [UnitTitle("CharacterMove3D")] 
    [UnitCategory("Game/Character")]
    public class CharacterMove3D : WaitUnit
    {
        private static ILog log = LogManager.GetLogger("vs.character");
        
        /// <summary>
        /// 角色ID
        /// </summary>
        [DoNotSerialize]
        public ValueInput characterID { get; private set; }

        [DoNotSerialize]
        public ValueInput targetPosition { get; private set; }

        [DoNotSerialize]
        public ValueInput lastRotation { get; private set; }
        
        bool IsMoving(Character c)
        {
            if (c == null)
                return false;

            return c.isMoving;
        }
        
        protected override void Definition()
        {
            base.Definition();

            characterID     = ValueInput<int>(nameof(characterID));
            targetPosition  = ValueInput<Vector3>(nameof(targetPosition));
            lastRotation    = ValueInput<float>(nameof(lastRotation), -1);
            
            Requirement(characterID, enter);
        }
        
        protected override IEnumerator Await(Flow flow)
        {
            var id          = flow.GetValue<int>(characterID);
            var TargetPos   = flow.GetValue<Vector3>(targetPosition);
            var rot         = flow.GetValue<float>(lastRotation);
            var c     = CharacterModule.Instance.FindCharacter(id);
            
            if (c != null)
            {
                c.Move3dPosition(TargetPos);
                c.LastRotation(rot);
                yield return new WaitWhile(()=>IsMoving(c));
            }
            else
            {
                log.Error("character not found=" + id);   
            }

            yield return exit;
        }
    }
}