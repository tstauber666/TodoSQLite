using Microsoft.Maui.Animations;
using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;
using static System.Net.Mime.MediaTypeNames;

//C:\Users\stauber\AppData\Local\Packages\64673058-d486-410a-9ec4-643fc8187170_9zz4h110yvjzm\LocalState
namespace TodoSQLite.Views;


public partial class DataGridPage : ContentPage
{
    TodoItemDatabase database;
    public Int64 currentID;
    AaaTestBaeume currentitem;

    public ObservableCollection<AaaTestBaeume> Items { get; set; } = new();
    public ObservableCollection<BaumartenListe> ItemsBaumarten { get; set; } = new();
    public ObservableCollection<X3Ba> BaumArtList { get; set; } = new();
    public DataGridPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var baumarten = await database.GetBaAsync();  // Baumartenliste
        var items = await database.GetBaumeAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            BaumArtList.Clear();
            foreach (var item in baumarten)
                BaumArtList.Add(item);


            Items.Clear(); ItemsBaumarten.Clear();
            foreach (var item in items)
            {
              
                Items.Add(item);
            }

            baumartpicker.SelectedItem = baumartpicker.Items[3];
            baumartpicker.SelectedIndex = 2;
        });

        

    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DataGridPage), true, new Dictionary<string, object>
        {
            ["Item"] = new AaaTestBaeume()
        });
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //liste der Ausgewählten Einträge
        // var objects = (List<TodoItem>)e.CurrentSelection;


        if (e.CurrentSelection.FirstOrDefault() is not AaaTestBaeume item)
            return;

        currentID = (long)(e.CurrentSelection.FirstOrDefault() as AaaTestBaeume)?.Id;
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

        foreach (AaaTestBaeume item in Items)
        {
                     await database.SaveBaumeAsync(item);
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

        AaaTestBaeume item = new AaaTestBaeume();
        await database.SaveBaumeAsync(item);
        Items.Add(item);

    }

    async void OnItemDelete(object sender, EventArgs e)
    {
        //foreach (TodoItem item in Items.Where(x => x.ID == currentID))
        //{
        //    await database.DeleteItemAsync(item);
        //}

        await database.DeleteBaumeAsync(currentitem);

        var items = await database.GetBaumeAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);

        });
    }

    private void baumartpicker1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        long wert;

        if (selectedIndex != -1)
        {

           
            wert = ((TodoSQLite.Models.X3Ba)picker.ItemsSource[selectedIndex]).Icode;
            currentitem.Ba = wert;
        }
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        // vielleicht ein Picker element anschalten wenn das Element den Focus erhält
        //Picker picker = new Picker { Title = "BauMArt" };
        //picker.ItemsSource = BaumArtList;
        //picker.ItemDisplayBinding = new Binding("Icode");
        //picker.SelectedIndex = 0;
        //picker.Focus();
        var c = e.GetType;
        var a = e.VisualElement.BindingContext;
        var Ba = ((TodoSQLite.Models.AaaTestBaeume)a).Ba;
      
    }

    private void Entry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        

        //foreach (AaaTestBaeume item in ItemsBaumarten)
        //{
        //    item.Ba = long.Parse(e.NewTextValue); 
           
        //}

        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
    }

    

    //private void baumartpicker_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var alois = (((TodoSQLite.Models.X3Ba)baumartpicker.SelectedItem).Icode);

    //}



    //private void baumartpicker1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var alois = (((TodoSQLite.Models.X3Ba)baumartpicker1.SelectedItem).Icode);

    //}


}