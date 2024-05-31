using SGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICameraDisbale : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput state;

    protected override void Definition()
    {
        inputTrigger = ControlInput("Input", (flow) =>
        {
            bool disbale = flow.GetValue<bool>(state);
            SceneCameraSystem.Instance.disableDrag = disbale;
            return outputTrigger;
        });

        state = ValueInput<bool>("state");
        outputTrigger = ControlOutput("Output");
    }
}
