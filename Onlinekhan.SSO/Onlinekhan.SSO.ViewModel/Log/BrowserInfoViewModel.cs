public class BrowserInfoViewModel
{
    public string Type { get; set; }

    public string Name { get; set; }

    public string Version { get; set; }

    public int MajorVersion { get; set; }

    public double MinorVersion { get; set; }

    public string Platform { get; set; }

    public bool IsBeta { get; set; }

    public bool IsCrawler { get; set; }

    public bool IsAol { get; set; }

    public bool IsWin16 { get; set; }

    public bool IsWin32 { get; set; }

    public bool SupportsFrames { get; set; }

    public bool SupportsTables { get; set; }

    public bool SupportsCookies { get; set; }

    public bool SupportsVbScript { get; set; }

    public string SupportsJavaScript { get; set; }

    public bool SupportsJavaApplets { get; set; }

    public bool SupportsActiveXControls { get; set; }

    public string SupportsJavaScriptVersion { get; set; }

    public string Ip { get; set; }

    public string UserAgent { get; set; }

    public string Device { get; set; }
}