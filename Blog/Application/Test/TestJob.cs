using Quartz;

namespace Application.Test;

public class TestJob:IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("testjob");
        ;
    }
}