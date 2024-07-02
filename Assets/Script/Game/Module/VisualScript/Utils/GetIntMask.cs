using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System;
using System.Linq;
/// 获得对象的位数据
namespace SGame.VS
{
    [UnitCategory("Game/Utils")]
    public class GetIntMask : Unit
    {
        [SerializeAs(nameof(argumentCount))]
        private int _argumentCount;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Arguments")]
        public int argumentCount
        {
            get => _argumentCount;
            set => _argumentCount = Mathf.Clamp(value, 1, 5);
        }
        
        [DoNotSerialize]
        public List<ValueInput> arguments { get; } = new List<ValueInput>();

        [DoNotSerialize]
        public ValueOutput      m_MaskValue;
        
        protected override void Definition()
        {
            arguments.Clear();
            for (int i = 0; i < _argumentCount; i++)
            {
                arguments.Add(ValueInput<int>("param_" + i, i));
            }

            m_MaskValue = ValueOutput<int>("MaskValue", (flow) =>
            {
                var mask = 0;
                for (int i = 0; i < arguments.Count; i++)
                {
                    int value = flow.GetValue<int>(arguments[i]);
                    mask = BitOperator.Set(mask, value, true);
                }
                
                return mask;
            });
        }
    }
}