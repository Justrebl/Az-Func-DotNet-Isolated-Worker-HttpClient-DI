using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => 
        s.AddHttpClient("JsonPlaceholder", HttpClient =>
                HttpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("JSONPHBaseAddress") ?? "")))
    .Build();

host.Run();
