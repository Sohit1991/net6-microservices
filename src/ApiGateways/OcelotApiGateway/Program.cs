using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot()
            .AddCacheManager(a => a.WithDictionaryHandle()); // Enabling ocelot caching
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
// To- DO Need to add logging , need to find way for Net 6

//COnfigurelogging((hostingCOntext, logginbuilder) =>
//{
//    logginbuilder.AddConfiguration(hostingCOntext.configuratin.getsection("Loggin"));
//logginbuilder.AddConsole();
// logginbuilder.AddDebug();
//});
