using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPBoilerplate.Loggers;

public class AboutModel : PageModel
{
    private readonly ILogger _logger;
    public AboutModel(ILogger<AboutModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("About page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());
    }
}
