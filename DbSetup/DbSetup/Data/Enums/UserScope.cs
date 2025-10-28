namespace DbSetup.Data.Enums;

public static class UserScope
{
    public const string BanksRead = "banks:read";
    public const string BanksWrite = "banks:write";
    public const string AccountsRead = "accounts:read";
    public const string AccountsWrite = "accounts:write";
    public const string TransactionsRead = "transactions:read";
    public const string TransactionsWrite = "transactions:write";
    public const string UsersRead = "users:read";
    public const string UsersWrite = "users:write";
    public const string UserRolesAssign = "user-roles:assign";

    public static readonly string[] All =
    [
        BanksRead, BanksWrite, AccountsRead, AccountsWrite,
        TransactionsRead, TransactionsWrite, UsersRead, UsersWrite, UserRolesAssign
    ];
}