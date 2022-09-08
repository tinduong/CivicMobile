namespace CivicMobile.Triggers
{
    public class Trigger
    {
    }

    public class DeselectListViewItemAction : TriggerAction<ListView>
    {
        protected override void Invoke(ListView sender)
        {
            sender.SelectedItem = null;
        }
    }
}
