using FairyGUI;
using SGame;
using Unity.Entities;
using Unity.VisualScripting;

public class UIGetBtnClick : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput uiName;

    [DoNotSerialize]
    public ValueInput uiClickName;

    [DoNotSerialize]
    public ValueOutput isClick;

    private bool clickValue;

    protected override void Definition()
    {
        inputTrigger = ControlInput("Input", (flow) =>
        {
            clickValue = false;
            string name = flow.GetValue<string>(uiName);
            string path = flow.GetValue<string>(uiClickName);
            Entity e = UIUtils.GetUIEntity(name);
            var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<SGame.UI.UIWindow>(e);
            var gObject = ui.Value.contentPane.GetChildByPath(path);
            if (gObject != null)
            {
                gObject.onClick.Add(Click);
                gObject.onClick.Add(() => gObject.onClick.Remove(Click));
            }
            return outputTrigger;
        });

        uiName = ValueInput<string>("uiname");
        uiClickName = ValueInput<string>("uiClickName");
        isClick = ValueOutput<bool>("IsClick", (flow)=> clickValue);
        outputTrigger = ControlOutput("Output");
    }

    public void Click() 
    {
        clickValue = true;
    }
}
