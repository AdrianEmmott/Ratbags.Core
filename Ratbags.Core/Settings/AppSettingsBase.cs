namespace Ratbags.Core.Settings;

public class AppSettingsBase
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Certificate Certificate { get; set; } = new Certificate();
    public JWT JWT { get; set; }
    public Messaging Messaging { get; set; } = new Messaging();
    public string AllowedHosts { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public class Certificate
{
    public string Name { get; set; }
    public string Password { get; set; }
}

public class JWT
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

public class Messaging
{
    public string Hostname { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
