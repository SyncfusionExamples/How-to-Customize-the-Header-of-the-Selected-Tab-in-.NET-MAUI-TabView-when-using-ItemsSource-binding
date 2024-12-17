This article explains how to customize the selected tab header in the [.NET MAUI TabView](https://www.syncfusion.com/maui-controls/maui-tab-view) when using the [ItemsSource](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.SfTabView.html#Syncfusion_Maui_TabView_SfTabView_ItemsSource) property. The `VisualStateManager` is designed to work directly with [TabItem](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.SfTabItem.html#Syncfusion_Maui_TabView_SfTabItem__ctor) but does not integrate seamlessly with the ItemsSource-based tab binding. To achieve the desired customization of the selected tab header, the [SelectionChanged](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.SfTabView.html?tabs=tabid-1#Syncfusion_Maui_TabView_SfTabView_SelectionChanged) event can be leveraged, by utilizing the [EventToCommandBehavior](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/behaviors/event-to-command-behavior).

To dynamically change the text color of the selected tab header, follow these steps:

**Model**

```csharp
public class Model: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string? propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

    private string? name;
    public string? Name
    {
        get { return name; }
        set { name = value; OnPropertyChanged("Name"); }
    }

    private Color? textColor;
    public Color? TextColor
    {
        get { return textColor; }
        set { textColor = value; OnPropertyChanged("TextColor"); }
    }
}
```

**ViewModel**

In your ViewModel, you will need to define the properties and commands to handle the selection change:

```csharp
public class MainPageViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private ObservableCollection<Model> tabItems;
    public ObservableCollection<Model> TabItems
    {
        get { return tabItems; }
        set { tabItems = value; OnPropertyChanged("TabItems"); }
    }

    private Command<object> selectionChangedCommand;
    public Command<object> SelectionChangedCommand
    {
        get { return selectionChangedCommand; }
        set { selectionChangedCommand = value; OnPropertyChanged("SelectionChangedCommand"); }
    }

    public MainPageViewModel()
    {
        TabItems = new ObservableCollection<Model>
        {
            new Model() { Name = "Alexandar", TextColor = Colors.Red },
            new Model() { Name = "Gabriella", TextColor = Colors.Black }
            // Add more items as needed
        };

        SelectionChangedCommand = new Command<object>(ExecuteSelectionChangedCommand);
    }

    public void ExecuteSelectionChangedCommand(object obj)
    {
        if (obj is SfTabView tabItem)
        {
            if (TabItems != null)
            {
                // Ensure the selected index is within the valid range
                if (tabItem.SelectedIndex >= 0 && tabItem.SelectedIndex < TabItems.Count)
                {
                    for (int i = 0; i < TabItems.Count; i++)
                    {
                        // Set TextColor for selected tab to red, others to black
                        TabItems[i].TextColor = (i == (int)tabItem.SelectedIndex) ? Colors.Red : Colors.Black;
                    }
                }
            }
        }
    }
}
```

**Explanation**

1. **TextColor Property**: A `TextColor` property is defined within the `Model` class to allow dynamic updates to the text color of the tab headers.
2. **SelectionChangedCommand**: This command is bound to the `SelectionChanged` event of the `.NET MAUI TabView`. When the selection changes, the command is executed with the `.NET MAUI TabView` instance as the command parameter.
3. **ExecuteSelectionChangedCommand Method**: This method resets the text color for all tab items to the default color (black) and updates the text color of the currently selected tab to red.

**XAML**

```xml
<tabView:SfTabView x:Name="tabView" ItemsSource="{Binding TabItems}" TabWidthMode="SizeToContent" TabHeaderPadding="0">
    <tabView:SfTabView.Behaviors>
        <toolkit:EventToCommandBehavior Command="{Binding SelectionChangedCommand}" CommandParameter="{x:Reference tabView}" EventName="SelectionChanged"/>
    </tabView:SfTabView.Behaviors>
    <tabView:SfTabView.HeaderItemTemplate>
        <DataTemplate >
            <Label x:Name="headerlabel" VerticalOptions="Center" Text="{Binding Name}" 
            TextColor="{Binding TextColor}"/>
        </DataTemplate>
    </tabView:SfTabView.HeaderItemTemplate>
    <tabView:SfTabView.ContentItemTemplate>
        <DataTemplate>
            <Grid HorizontalOptions="Center" VerticalOptions="Center">
                <Label x:Name="contentLabel" Text="{Binding Name}" HorizontalOptions="Center" VerticalOptions="Center"/>
            </Grid>
        </DataTemplate>
    </tabView:SfTabView.ContentItemTemplate>
</tabView:SfTabView>
```

By following these steps, you can effectively customize the selected tab header in a .NET MAUI TabView when using ItemsSource binding.

**Output**

![TabViewHeaderCustomization.gif](https://support.syncfusion.com/kb/agent/attachment/article/18235/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MDUxIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.LFJsmNPHH9vWsR3CpL62ZtwF5lLr3sWpSrgckb-4pCM)

**Requirements to run the demo**
 
To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)
 
**Troubleshooting:**

**Path too long exception** 

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.