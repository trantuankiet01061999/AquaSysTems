using AquaSolution.Data.Repositories;
using AquaSolution.Server.Services.Administration.ApprovalFlowService;
using AquaSolution.Server.Services.Administration.DepartmentService;
using AquaSolution.Server.Services.Administration.FactoryService;
using AquaSolution.Server.Services.Administration.InventoriesService;
using AquaSolution.Server.Services.Administration.MenusService;
using AquaSolution.Server.Services.Administration.PageManagement;
using AquaSolution.Server.Services.Administration.PermissionService;
using AquaSolution.Server.Services.Administration.PositionService;
using AquaSolution.Server.Services.Administration.RolePermissionService;
using AquaSolution.Server.Services.Administration.RoleService;
using AquaSolution.Server.Services.Administration.UserService;
using AquaSolution.Server.Services.ManageMedicalRooms.Products;
using AquaSolution.Server.Services.ManageMedicalRooms.WarehouseImportService;


namespace AquaSolution.Server
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // Đăng ký UserService
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IFactoryService, FactoryService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IApprovalFlowService, ApprovalFlowService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IWarehouseImportService, WarehouseImportService>();


            return services;
        }
    }
}
