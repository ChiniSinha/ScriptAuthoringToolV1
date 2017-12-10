using UnityEngine;

public class ScriptCommandMediator : BaseCommandMediator
{
    protected EventBus _eventBus;
    protected DefaultMessageParser _parser;
    protected ScriptCommandProtocol _protocol;

    public ScriptCommandMediator(ScriptCommandProtocol protocol) : base(protocol)
    {
        _protocol = protocol;
    }

    public override void Setup()
    {
        base.Setup();
        _eventBus = Globals.EventBus;
        _parser = new DefaultMessageParser();

        _eventBus.Register<CheckboxSubmitEvent>(OnCheckboxSubmit);
        _eventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _eventBus.Register<MenuSelectedEvent>(OnMenuSelect);
        _eventBus.Register<TableDisplayInputEvent>(OnTableInput);
        _eventBus.Register<NumberKeypadSubmitEvent>(OnNumberSubmit);
        _eventBus.Register<TextInputSubmitEvent>(OnUserTextSubmit);
        _eventBus.Register<LayoutChangeEvent>(OnLayoutChange);
        _eventBus.Register<XmlMessageReceived>(OnXmlMessageReceived);
        _eventBus.Register<NoChangeUiEvent>(OnUiNoChange);
        _eventBus.Register<ProcessExecuteEvent>(OnProcessExecute);
        _eventBus.Register<ProcessUILogicEvent>(OnProcessUILogic);
        _eventBus.Register<UserTimeoutEvent>(OnTimeoutEvent);
    }

    public override void Cleanup()
    {
        base.Cleanup();
        _eventBus.Unregister<CheckboxSubmitEvent>(OnCheckboxSubmit);
        _eventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
        _eventBus.Unregister<MenuSelectedEvent>(OnMenuSelect);
        _eventBus.Unregister<NumberKeypadSubmitEvent>(OnNumberSubmit);
        _eventBus.Unregister<TextInputSubmitEvent>(OnUserTextSubmit);
        _eventBus.Unregister<LayoutChangeEvent>(OnLayoutChange);
        _eventBus.Unregister<XmlMessageReceived>(OnXmlMessageReceived);
        _eventBus.Unregister<NoChangeUiEvent>(OnUiNoChange);
        _eventBus.Unregister<ProcessExecuteEvent>(OnProcessExecute);
        _eventBus.Unregister<ProcessUILogicEvent>(OnProcessUILogic);
        _eventBus.Unregister<UserTimeoutEvent>(OnTimeoutEvent);
    }

    private void OnTimeoutEvent(UserTimeoutEvent e)
    {
        _protocol.SendTimeout();
    }

    private void OnProcessUILogic(ProcessUILogicEvent e)
    {
        _protocol.SendUILogic(e.Ui);
    }

    private void OnProcessExecute(ProcessExecuteEvent e)
    {
        _protocol.SendExecute(e.Command);
    }

    private void OnXmlMessageReceived(XmlMessageReceived e)
    {
        Debug.Log("Received message: " + e.Message.OuterXml);
        _parser.DeserializeMessage(e.Message);
    }

    private void OnUserTextSubmit(TextInputSubmitEvent textInputSubmitEvent)
    {
        _protocol.SendTextInput(textInputSubmitEvent.UserString, textInputSubmitEvent.ButtonPressed);
    }

    private void OnNumberSubmit(NumberKeypadSubmitEvent numberKeypadSubmitEvent)
    {
        _protocol.SendNumberInput(numberKeypadSubmitEvent.UserInput, numberKeypadSubmitEvent.ButtonPressed);
    }

    private void OnTableInput(TableDisplayInputEvent tableDisplayInputEvent)
    {
        _protocol.SendTableInput(tableDisplayInputEvent.ButtonPressed);
    }

    private void OnMenuSelect(MenuSelectedEvent menuSelectedEvent)
    {
        _protocol.SendMenuSelection(menuSelectedEvent.SelectedOptionIdx);
    }

    private void OnInteractableChange(InteractableChangeEvent interactableChangeEvent)
    {
        _protocol.SendDummyInputResponse();
    }

    private void OnCheckboxSubmit(CheckboxSubmitEvent checkboxSubmitEvent)
    {
        _protocol.SendCheckboxInput(checkboxSubmitEvent.SelectedIdxs, checkboxSubmitEvent.ButtonPressed);
    }

    private void OnLayoutChange(LayoutChangeEvent e)
    {
        _protocol.SendDummyInputResponse();
    }

    private void OnUiNoChange(NoChangeUiEvent e)
    {
        _protocol.SendDummyInputResponse();
    }

}