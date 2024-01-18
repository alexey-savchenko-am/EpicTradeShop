
namespace AppCommon.Persistence;

public interface IDatabaseInitializer
{
    Task<bool> InitializeWithTestDataAsync(bool recreateDatabase);
}
