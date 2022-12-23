using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;

//C:\Users\stauber\AppData\Local\Packages\64673058-d486-410a-9ec4-643fc8187170_9zz4h110yvjzm\LocalState
namespace TodoSQLite.Views;


public partial class TodoListPage : ContentPage
{
    TodoItemDatabase database;
    public Int64 currentID;
    TodoItem currentitem;

    public ObservableCollection<TodoItem> Items { get; set; } = new();
    public TodoListPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);

        });
    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new TodoItem()
        });
    }

    private  void  CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //liste der Ausgewählten Einträge
        // var objects = (List<TodoItem>)e.CurrentSelection;
         

        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
            return;

        currentID = (long)(e.CurrentSelection.FirstOrDefault() as TodoItem)?.ID;
        currentitem = item;

        //await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        //{
        //    ["Item"] = item
        //});
    }
    void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
                string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        
    }
    void OnEntryCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
    }

    async void OnItemsSave(object sender, EventArgs e)
    {
        
        foreach (TodoItem item in Items)
        {
            await database.SaveItemAsync(item);
        }
        //     if (string.IsNullOrWhiteSpace(Item.Name))
        //        {
        //            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
        //            return;
        //        }
        //await database.SaveItemAsync(Item);
        //await Shell.Current.GoToAsync("..");

       
    }
    async void OnItemsAdd(object sender, EventArgs e)
    {

        TodoItem item = new TodoItem();
        await database.SaveItemAsync(item);
        Items.Add(item);

    }

    async void OnItemDelete(object sender, EventArgs e)
    {
        //foreach (TodoItem item in Items.Where(x => x.ID == currentID))
        //{
        //    await database.DeleteItemAsync(item);
        //}

        await database.DeleteItemAsync(currentitem);

        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);

        });
    }

}