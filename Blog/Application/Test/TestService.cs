using Core.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Application.Test;
[DynamicApi(ServiceLifeCycle = "Scoped")]
public class TestService:ITestService
{
    [HttpGet]
    public string GetUserName()
    {
        return "张三";
    }
}