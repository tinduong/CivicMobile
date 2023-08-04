using SQLite;

namespace CivicMobile.Database;

public static class Constants
{
    public const string DatabaseFilename = "civic.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}

public class CivicDbContext
{
    private SQLiteAsyncConnection Database;

    public CivicDbContext()
    {
    }

    private async Task CreateDbIfNotExisted<TModel>() where TModel : class, new()
    {
        if (Database is not null) return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<TModel>();
    }

    public async Task<IEnumerable<TModel>> Get<TModel>() where TModel : class, new()
    {
        await CreateDbIfNotExisted<TModel>();
        var result = await Database.Table<TModel>().ToListAsync();
        return result;
    }

    public async Task Add<TModel>(TModel model) where TModel : class, new()
    {
        await CreateDbIfNotExisted<TModel>();
        await Database.InsertAsync(model);
    }

    public async Task AddMultiple<TModel>(IEnumerable<TModel> items) where TModel : class, new()
    {
        await CreateDbIfNotExisted<TModel>();
        await Database.InsertAllAsync(items);
    }

    public async Task Update<TModel>(TModel model) where TModel : class, new()
    {
        await CreateDbIfNotExisted<TModel>();
        await Database.UpdateAsync(model);
    }
}