using System;

namespace ASPBoilerplate.Configurations;

public class MailSettings
{
    public static string? Server { get; set; }
    public static int Port { get; set; }
    public static string? SenderName { get; set; }
    public static string? SenderEmail { get; set; }
    public static string? UserName { get; set; }
    public static string? Password { get; set; }

    public static void InitalizeMailSettings(WebApplicationBuilder builder)
    {
        Server = builder.Configuration["MailSettings:Server"];
        Port = builder.Configuration["MailSettings:Port"] != null ? Convert.ToInt32(builder.Configuration["MailSettings:Port"]) : 587;
        SenderName = builder.Configuration["MailSettings:SenderName"];
        SenderEmail = builder.Configuration["MailSettings:SenderEmail"];
        UserName = builder.Configuration["MailSettings:UserName"];
        Password = builder.Configuration["MailSettings:Password"];
    }

}
