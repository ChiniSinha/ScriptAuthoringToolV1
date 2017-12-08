public class SimpleTitleMediator : ITitleMediator
{
    private readonly TitleDisplay _title;

    public SimpleTitleMediator(TitleDisplay title)
    {
        _title = title;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<SetTitleEvent>(OnSetTitle);
        _title.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<SetTitleEvent>(OnSetTitle);
    }

    private void OnSetTitle(SetTitleEvent e)
    {
        if (string.IsNullOrEmpty(e.Title))
        {
            _title.PrimaryAnimator.Hide();
        }
        else
        {
            _title.Title.text = e.Title;
            _title.PrimaryAnimator.Show();
        }
    }
}