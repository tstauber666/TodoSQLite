using TodoSQLite.Data;
using TodoSQLite.Views;
using Telerik.Maui.Controls.Compatibility;


namespace TodoSQLite;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        builder.Services.AddSingleton<TodoListPage>();
        builder.Services.AddSingleton<DataGridPage>();
        builder.Services.AddSingleton<TelerikDataGrid>();
        builder.Services.AddTransient<TodoItemPage>();
        builder.Services.AddSingleton<TodoItemDatabase>();
        builder.UseTelerik();
        
        return builder.Build();
    }
}