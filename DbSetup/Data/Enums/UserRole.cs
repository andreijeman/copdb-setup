namespace DbSetup.Data.Enums;

public static class UserRole
{
    public const string SystemAdministrator = "SYSTEM_ADMINISTRATOR";
    public const string BankOperator = "BANK_OPERATOR";
    public const string Customer = "CUSTOMER";

    public static readonly IReadOnlyCollection<string> AllRoles =
    [
        SystemAdministrator,
        BankOperator,
        Customer
    ];
}