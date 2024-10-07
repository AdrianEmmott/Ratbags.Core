namespace Ratbags.Core.Settings;

public class AppSettingsBase
{
    public Certificate Certificate { get; set; } = new Certificate();

    public Messaging Messaging { get; set; } = new Messaging();

    public string AllowedHosts { get; set; }
}

public class Certificate
{
    public string Name { get; set; }
    public string Password { get; set; }
}

public class Messaging
{
    public string Hostname { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
