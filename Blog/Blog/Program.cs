using Blog.BaseConfigSerivce;
using Blog.BaseConfigSerivce.DynamicAPi;
using Blog.BaseConfigSerivce.Filter;
using Blog.BaseConfigSerivce.Jwt;
using Blog.BaseConfigSerivce.SqlSugar;
using Blog.BaseConfigSerivce.Swagger;
using Blog.Core.SignalR;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", false, true).AddJsonFile(
    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);
builder.Services.JwtRegesiterService(builder.Configuration);
builder.Services.AutoRegistryService();
builder.Services.AddControllers().AddDynamicWebApi();
builder.Services.AddSwaggerGenExtend();
// builder.Services.AddSqlsugarSetup(builder.Configuration);
// builder.Logging.ClearProviders();
// builder.Logging.AddNLog("NLog.config");
// builder.Host.UseNLog();
builder.Services.AddMvc(options => { options.Filters.Add<ApiFilter>(); });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();
//注册SignalR
builder.Services.AddSignalR();
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "AllowCore", x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
    });
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Core V1");
    c.RoutePrefix = "ApiDoc";
});
app.UseCors("AllowCore");
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/chatHub"); });
app.Run();