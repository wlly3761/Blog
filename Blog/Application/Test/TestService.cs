using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Enum;
using Repository.Model;

namespace Application.Test;

[DynamicApi(ServiceLifeCycle = "Scoped")]
public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;

    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string GetUserName()
    {
        _logger.LogError("测试123");
        return "张三";
    }

    [HttpGet]
    public string GetEnumValue(CertificateModel model)
    {
        var returnStr = "";
        switch (model.SexEnum)
        {
            case SexEnum.Man:
                returnStr = SexEnum.Man.ToString();
                break;
            case SexEnum.WoMan:
                returnStr = SexEnum.WoMan.ToString();
                break;
        }

        return returnStr;
    }
}