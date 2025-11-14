using System.Security.Claims;
using DbSetup.Data.Context;
using DbSetup.Data.Entities;
using DbSetup.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace DbSetup.Services;

public class SeedService
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public SeedService(AppDbContext dbContext, UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        await SeedBanksAsync();
        await SeedRolesAsync();
        await SeedAdminAsync();
        await SeedBankOperatorAsync();
        await SeedAccountsAsync();
    }

    private async Task SeedRolesAsync()
    {
        if (await _roleManager.FindByNameAsync(UserRole.SystemAdministrator) is null)
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.SystemAdministrator));
        
        if (await _roleManager.FindByNameAsync(UserRole.BankOperator) is null)
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.BankOperator));
        
        if (await _roleManager.FindByNameAsync(UserRole.Customer) is null)
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.Customer));
    }

    private async Task SeedAdminAsync()
    {
        var adminEmail = _configuration["Users:Admin:Email"]!;
        var adminUserName = _configuration["Users:Admin:UserName"]!;
        var adminPassword = _configuration["Users:Admin:Password"]!;

        if (await _userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new User
            {
                Id = Guid.Parse(_configuration["Users:Admin:Id"]!),
                UserName = adminUserName,
                Email = adminEmail,
                UserStatus = UserStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var createResult = await _userManager.CreateAsync(admin, adminPassword);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors);
                throw new Exception($"Failed to create admin user: {errors}");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(admin, UserRole.SystemAdministrator);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors);
                throw new Exception($"Failed to add admin to role: {errors}");
            }
            
            var role = await _roleManager.FindByNameAsync(UserRole.SystemAdministrator);
            
            if (role is null)
            {
                throw new Exception("Admin role not found");
            }

            foreach (var scope in UserScope.All)
            {
                await _roleManager.AddClaimAsync(role, new Claim("scope", scope));
            }
        }
    }

    private async Task SeedBanksAsync()
    {
        var bank1 = new ReferenceBank
        {
            Id = Guid.Parse(_configuration["Banks:Pepe:Id"]!),
            BankName = _configuration["Banks:Pepe:Name"]!,
            BankCode = _configuration["Banks:Pepe:Code"]!,
            Country = _configuration["Banks:Pepe:Country"]!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var bank2 = new ReferenceBank
        {
            Id = Guid.Parse(_configuration["Banks:XBank:Id"]!),
            BankName = _configuration["Banks:XBank:Name"]!,
            BankCode = _configuration["Banks:XBank:Code"]!,
            Country = _configuration["Banks:XBank:Country"]!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var bank3 = new ReferenceBank
        {
            Id = Guid.Parse(_configuration["Banks:McBank:Id"]!),
            BankName = _configuration["Banks:McBank:Name"]!,
            BankCode = _configuration["Banks:McBank:Code"]!,
            Country = _configuration["Banks:McBank:Country"]!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _dbContext.ReferenceBanks.AddRange(bank1, bank2, bank3);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedAccountsAsync()
    {
        var account1 = new Account
        {
            AccountId = Guid.Parse(_configuration["Accounts:Account1:AccountId"]!),
            BankId = Guid.Parse(_configuration["Banks:Pepe:Id"]!), 
            AccountName = _configuration["Accounts:Account1:AccountName"]!,
            AccountNumber = _configuration["Accounts:Account1:AccountNumber"]!,
            RoutingNumber = _configuration["Accounts:Account1:RoutingNumber"]!,
            Status = Enum.Parse<AccountStatus>(_configuration["Accounts:Account1:Status"]!, true),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var account2 = new Account
        {
            AccountId = Guid.Parse(_configuration["Accounts:Account2:AccountId"]!),
            BankId = Guid.Parse(_configuration["Banks:Pepe:Id"]!), 
            AccountName = _configuration["Accounts:Account2:AccountName"]!,
            AccountNumber = _configuration["Accounts:Account2:AccountNumber"]!,
            RoutingNumber = _configuration["Accounts:Account2:RoutingNumber"]!,
            Status = Enum.Parse<AccountStatus>(_configuration["Accounts:Account2:Status"]!, true),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var account3 = new Account
        {
            AccountId = Guid.Parse(_configuration["Accounts:Account3:AccountId"]!),
            BankId = Guid.Parse(_configuration["Banks:Pepe:Id"]!),
            AccountName = _configuration["Accounts:Account3:AccountName"]!,
            AccountNumber = _configuration["Accounts:Account3:AccountNumber"]!,
            RoutingNumber = _configuration["Accounts:Account3:RoutingNumber"]!,
            Status = Enum.Parse<AccountStatus>(_configuration["Accounts:Account3:Status"]!, true),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Accounts.AddRange(account1, account2, account3);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedBankOperatorAsync()
    {
        var operatorEmail = _configuration["Users:BankOperator:Email"]!;
        var operatorUserName = _configuration["Users:BankOperator:UserName"]!;
        var operatorPassword = _configuration["Users:BankOperator:Password"]!;

        if (await _userManager.FindByEmailAsync(operatorEmail) is null)
        {
            var bankOperator = new User
            {
                Id =  Guid.Parse(_configuration["Users:BankOperator:Id"]!),
                BankId = Guid.Parse(_configuration["Banks:McBank:Id"]!),
                UserName = operatorUserName,
                Email = operatorEmail,
                UserStatus = UserStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var createResult = await _userManager.CreateAsync(bankOperator, operatorPassword);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors);
                throw new Exception($"Failed to create BankOperator user: {errors}");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(bankOperator, UserRole.BankOperator);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors);
                throw new Exception($"Failed to add BankOperator to role: {errors}");
            }
            
            var role = await _roleManager.FindByNameAsync(UserRole.BankOperator);
            
            if (role is null)
            {
                throw new Exception("BankOperator role not found");
            }

            foreach (var scope in UserScope.All)
            {
                await _roleManager.AddClaimAsync(role, new Claim("scope", scope));
            }
        }
    }
}