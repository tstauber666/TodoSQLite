using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;
using Telerik.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;

namespace TodoSQLite.Views;




public partial class TelerikDataGrid : ContentPage
{
    TodoItemDatabase database;
    public Int64 currentID;
    AaaTestBaeume currentitem;

    public ObservableCollection<AaaTestBaeume> Items { get; set; } = new();
    public ObservableCollection<BaumartenListe> ItemsBaumarten { get; set; } = new();
    public ObservableCollection<X3Ba> BaumArtList { get; set; } = new();
    public TelerikDataGrid(TodoItemDatabase todoItemDatabase)
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


            Items.Clear();
            foreach (var item in items)
            {

                Items.Add(item);
            }

            
        });

        this.dataGrid.ItemsSource = Items;
        var alois=this.dataGrid.Columns;
       

    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TelerikDataGrid), true, new Dictionary<string, object>
        {
            ["Item"] = new AaaTestBaeume()
        });
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

    private void dataGrid_SelectionChanged(object sender, Telerik.Maui.Controls.Compatibility.DataGrid.DataGridSelectionChangedEventArgs e)
    {
        //if (e.CurrentSelection.FirstOrDefault() is not AaaTestBaeume item)
        //    return;

        //currentID = (long)(e.CurrentSelection.FirstOrDefault() as AaaTestBaeume)?.Id;

        currentitem = (AaaTestBaeume)this.dataGrid.SelectedItem;
    }
}