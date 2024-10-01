using gP2os.ACL.Mail.BrasilApi;
using gP2os.ACL.Mail.BrasilApi.Models;
using gP2os.ACL.Mail.ViaCEP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents()
      .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();

builder.Services.AddFastReport();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "gP2os.ParticipantsTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();

builder.Services.AddLocalization();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<HelperAuthToken>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<gP2Navigation>();

builder.Services.AddSingleton<NavigationHistory>();
builder.Services.AddSingleton<SecurityAuthState>();

builder.Services.AddScoped<AuthenticationStateProvider, UserAuthStateProvider>();

builder.Services.AddHttpClient<IUserApiRepository, UserApiRepository>();

builder.Services.AddDbContextFactory<gP2osUserDSDBContext>();
builder.Services.AddScoped<IUserDataSchemaRepository, UserDataSchemaRepository>();

builder.Services.AddDbContextFactory<gP2ParticipantDBContext>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IParticipantAddressRepository, ParticipantAddressRepository>();

builder.Services.AddDbContextFactory<gP2GenericTableDBContext>();
builder.Services.AddScoped<IGenericTableStatusRepository, GenericTableStatusRepository>();
builder.Services.AddScoped<IGenericTableCatalogRepository, GenericTableCatalogRepository>();

builder.Services.AddDbContextFactory<gP2DataBinderDBContext>();
builder.Services.AddScoped<IDataBinderRepository, DataBinderRepository>();

builder.Services.AddScoped<CheckAuthToken>();

builder.Services.AddSingleton<ReportService>();

builder.Services.AddScoped<BrasilApi>();
builder.Services.AddScoped<ViaCEP>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseFastReport();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();