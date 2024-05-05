using Application.Test;
using Blog.BaseConfigSerivce;
using Blog.BaseConfigSerivce.DynamicAPi;
using Blog.BaseConfigSerivce.Filter;
using Blog.BaseConfigSerivce.Jwt;
using Blog.BaseConfigSerivce.SqlSugar;
using Blog.BaseConfigSerivce.Swagger;
using Blog.Core.SignalR;
using Core.Quartz;
using NLog.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

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
//注册Quartz任务调度
//注入调度中心
builder.Services.AddSingleton<SchedulerCenter>();
//注入Quartz任何工厂及调度工厂
builder.Services.AddSingleton<IJobFactory, QuartzJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
//注入具体实现业务作业
builder.Services.AddTransient<TestTrigger>();
builder.Services.AddTransient<TestJob>();

 

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
//使用SinganR
app.UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/chatHub"); });
//任务调度
//获取调度中心实例
var quartz = app.Services.GetRequiredService<SchedulerCenter>();
app.Lifetime.ApplicationStarted.Register(() =>
{
    quartz.StartScheduler(); //项目启动后启动调度中心
});
app.Lifetime.ApplicationStopped.Register(() =>
{
    quartz.StopScheduler();  //项目停止后关闭调度中心
});
app.Run();