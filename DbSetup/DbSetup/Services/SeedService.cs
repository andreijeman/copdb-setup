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
        await SeedUsersAsync();
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

    private async Task SeedUsersAsync()
    {
        var adminEmail = _configuration["Users:Admin:Email"]!;
        var adminUserName = _configuration["Users:Admin:UserName"]!;
        var adminPassword = _configuration["Users:Admin:Password"]!;

        if (await _userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new User
            {
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
        }
    }

    private async Task SeedBanksAsync()
    {
        var bank1 = new ReferenceBank
        {
            BankName = _configuration["Banks:Pepe:Name"]!,
            BankCode = _configuration["Banks:Pepe:Code"]!,
            Country = _configuration["Banks:Pepe:Country"]!,
            Id = Guid.CreateVersion7(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var bank2 = new ReferenceBank
        {
            BankName = _configuration["Banks:XBank:Name"]!,
            BankCode = _configuration["Banks:XBank:Code"]!,
            Country = _configuration["Banks:XBank:Country"]!,
            Id = Guid.CreateVersion7(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var bank3 = new ReferenceBank
        {
            BankName = _configuration["Banks:McBank:Name"]!,
            BankCode = _configuration["Banks:McBank:Code"]!,
            Country = _configuration["Banks:McBank:Country"]!,
            Id = Guid.CreateVersion7(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        _dbContext.ReferenceBanks.Add(bank1);
        _dbContext.ReferenceBanks.Add(bank2);
        _dbContext.ReferenceBanks.Add(bank3);
        
        await _dbContext.SaveChangesAsync();
    }
}