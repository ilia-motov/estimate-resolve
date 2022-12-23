using System.Reflection;
using EstimateResolve.Controllers;
using EstimateResolve.DataAccess;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Services;
using MudBlazor.Services;
using TanvirArjel.EFCore.GenericRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EstimateResolveDbContext>();
builder.Services.AddGenericRepository<EstimateResolveDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSingleton<IExceptionInterceptorService, ExceptionInterceptorService>();

builder.Services.AddScoped<IDataSeedingService, DataSeedingService>();

builder.Services.AddScoped<IController<ClientDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new ClientController(repository);

    return new ControllerWrapper<ClientDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<CompanyServiceDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new CompanyServiceController(repository);

    return new ControllerWrapper<CompanyServiceDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<ConstructionObjectDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new ConstructionObjectController(repository);

    return new ControllerWrapper<ConstructionObjectDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<EstimateDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new EstimateController(repository);

    return new ControllerWrapper<EstimateDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<MaterialDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new MaterialController(repository);

    return new ControllerWrapper<MaterialDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<UnitOfMeasurementDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new UnitOfMeasurementController(repository);

    return new ControllerWrapper<UnitOfMeasurementDto>(controller, exceptionInterceptorService);
});

builder.Services.AddScoped<IController<ContractDto>>(serviceProvider => {
    var exceptionInterceptorService = serviceProvider.GetRequiredService<IExceptionInterceptorService>();
    var repository = serviceProvider.GetRequiredService<IRepository>();
    var controller = new ÑontractController(repository);

    return new ControllerWrapper<ContractDto>(controller, exceptionInterceptorService);
});

var app = builder.Build();

app.Services
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<IDataSeedingService>()
    .SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
