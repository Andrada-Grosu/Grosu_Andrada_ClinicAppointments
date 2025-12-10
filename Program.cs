using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grosu_Andrada_ClinicAppointments.Data;
using Grosu_Andrada_ClinicAppointments.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Grosu_Andrada_ClinicAppointmentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Grosu_Andrada_ClinicAppointmentsContext")
    ?? throw new InvalidOperationException("Connection string 'Grosu_Andrada_ClinicAppointmentsContext' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<INoShowPredictionService, NoShowPredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:63600/");
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
