namespace ASPBoilerplate.Services;

class CronJobService {
    public static int TotalTestJobFired = 0;
    public static void TestJob () {
        TotalTestJobFired += 1;
        Console.WriteLine($"Running Cron {TotalTestJobFired} times");
    }
}