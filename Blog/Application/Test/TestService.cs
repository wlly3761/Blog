using Core.Attribute;

namespace Application.Test;
[DynamicApi(ServiceLifeCycle = "Scoped")]
public class TestService:ITestService
{
    public string GetUserName()
    {
        return "张三";
    }
}