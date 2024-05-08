using Quartz;
using Quartz.Spi;

namespace Core.Quartz;

/// <summary>
/// 手动实现Quartz定时器工厂 避免Jobs无法注入service的问题
/// </summary>
public class QuartzJobFactory : IJobFactory
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
 
    public QuartzJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
 
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IServiceScope serviceScope = _serviceProvider.CreateScope();
        IJob job;
        try
        {
            Type jobType = bundle.JobDetail.JobType;
            job = (IJob) serviceScope.ServiceProvider.GetService(jobType);
        }
        catch
        {
            serviceScope.Dispose();
            throw;
        }
 
        return job;
    }
    public IJobDetail CreateJobDetail(string jobType, string jobName, string groupName)  
    {  
        // 假设你有一个机制来将jobType映射到具体的作业类型  
        Type jobTypeToCreate = Type.GetType(jobType); // 或使用其他逻辑来确定类型  
        if (jobTypeToCreate == null || !typeof(IJob).IsAssignableFrom(jobTypeToCreate))  
        {  
            throw new InvalidOperationException("Invalid job type.");  
        }  
  
        // 使用依赖注入容器来创建作业实例  
        IJob jobInstance = (IJob)_serviceProvider.GetService(jobTypeToCreate);  
        return JobBuilder.Create(jobInstance.GetType())  
            .WithIdentity(jobName, groupName)
            .Build();  
    }
    public ITrigger CreateTrigger(string jobName, string groupName)  
    {  
        return TriggerBuilder.Create()
            .WithIdentity(jobName, groupName)
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();
    }  
 
    /// <summary>
    /// 清理Quartz任务
    /// </summary>
    public void ReturnJob(IJob job)
    {
        if (job == null)
        {
            return;
        }
 
        IDisposable disposable = job as IDisposable;
        disposable?.Dispose();
    }
}