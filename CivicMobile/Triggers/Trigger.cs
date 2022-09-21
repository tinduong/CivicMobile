﻿namespace CivicMobile.Triggers;

public class DeselectListViewItemAction : TriggerAction<CollectionView>
{
    protected override void Invoke(CollectionView sender)
    {
        sender.SelectedItem = null;
    }
}