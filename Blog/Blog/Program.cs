using Application.Test;
using Blog.BaseConfigSerivce;
using Blog.BaseConfigSerivce.DynamicAPi;
using Blog.BaseConfigSerivce.Filter;
using Blog.BaseConfigSerivce.Jwt;
using Blog.BaseConfigSerivce.SqlSugar;
using Blog.BaseConfigSerivce.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);
builder.Services.JwtRegesiterService(builder.Configuration);
builder.Services.AutoRegistryService();
builder.Services.AddControllers().AddDynamicWebApi();
builder.Services.AddSwaggerGenExtend();
builder.Services.AddSqlsugarSetup(builder.Configuration);
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ApiFilter>();
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Core V1");
    c.RoutePrefix="ApiDoc";
});

app.Run();