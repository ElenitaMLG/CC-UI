using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HealthcareManagerUiWebAssem;
using HealthcareManagerUiWebAssem.Services.Appointment;
using HealthcareManagerUiWebAssem.Services.Authentication;
using HealthcareManagerUiWebAssem.Services.Http;
using HealthcareManagerUiWebAssem.Services.Profile;
using HealthcareManagerUiWebAssem.Services.User;
using HealthcareManagerUiWebAssem.Services.UserSessionInformation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IUserSessionInformation, UserSessionInformation>();
builder.Services.AddScoped<IRequestHttpService, RequestHttpService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

await builder.Build().RunAsync();
