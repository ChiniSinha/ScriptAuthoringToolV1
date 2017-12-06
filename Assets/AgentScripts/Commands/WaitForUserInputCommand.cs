//using UnityEngine;

//public class WaitForUserInputCommand : BaseCommand
//{
//    private EventBus _globalEvents;
//    private readonly bool _hasTimeout;
//    protected float _startTime;

//    public WaitForUserInputCommand(bool hasTimeout = false, int durationMs = 60000)
//    {
//        _hasTimeout = hasTimeout;
//        Duration = durationMs/1000f;
//    }

//    public float Duration { get; protected set; }

//    public override void Execute()
//    {
//        _startTime = Time.time;
//        InProgress = true;
//        _globalEvents = Globals.EventBus;
//        _globalEvents.Register<MenuSelectedEvent>(OnUiEvent);
//        _globalEvents.Register<CheckboxSubmitEvent>(OnUiEvent);
//        _globalEvents.Register<KeyboardInputEvent>(OnUiEvent);
//        _globalEvents.Register<NumberKeypadSubmitEvent>(OnUiEvent);
//        _globalEvents.Register<SliderInputSubmitEvent>(OnUiEvent);
//        _globalEvents.Register<VideoPlayerInputEvent>(OnUiEvent);
//        _globalEvents.Register<TextInputSubmitEvent>(OnUiEvent);
//        _globalEvents.Register<QuestionnaireSubmitEvent>(OnUiEvent);
//        _globalEvents.Register<TableDisplayInputEvent>(OnUiEvent);
//    }

//    public override void OnUpdate()
//    {
//        if (_hasTimeout && (Time.time > _startTime + Duration))
//        {
//            InProgress = false;
//            UnregisterEvents();
//            //_globalEvents.Dispatch(new ClearUiEvent());
//            UiAnimator.ClearUI();
//            _globalEvents.Dispatch(new UserTimeoutEvent());
//        }
//    }

//    private void UnregisterEvents()
//    {
//        _globalEvents.Unregister<MenuSelectedEvent>(OnUiEvent);
//        _globalEvents.Unregister<CheckboxSubmitEvent>(OnUiEvent);
//        _globalEvents.Unregister<KeyboardInputEvent>(OnUiEvent);
//        _globalEvents.Unregister<NumberKeypadSubmitEvent>(OnUiEvent);
//        _globalEvents.Unregister<SliderInputSubmitEvent>(OnUiEvent);
//        _globalEvents.Unregister<VideoPlayerInputEvent>(OnUiEvent);
//        _globalEvents.Unregister<TextInputSubmitEvent>(OnUiEvent);
//        _globalEvents.Unregister<QuestionnaireSubmitEvent>(OnUiEvent);
//        _globalEvents.Unregister<TableDisplayInputEvent>(OnUiEvent);
//    }

//    protected void OnUiEvent(UIEvent e)
//    {
//        InProgress = false;
//        UnregisterEvents();
//    }
//}