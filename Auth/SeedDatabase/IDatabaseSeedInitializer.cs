namespace Auth.SeedDatabase;

public interface IDatabaseSeedInitializer
{
    void InitializeSeedRoles();
    void InitializeSeedUsers();
}
