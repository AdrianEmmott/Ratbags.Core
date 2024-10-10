namespace Ratbags.Core.Settings;

public class AppSettingsBase
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Ports Ports { get; set; }
    public Certificate Certificate { get; set; } = new Certificate();
    public JWT JWT { get; set; }
    public ExternalAuthentication ExternalAuthentication { get; set; }
    public Messaging Messaging { get; set; } = new Messaging();
    public string AllowedHosts { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public class Ports
{
    public string Http { get; set; }
    public string Https { get; set; }
}

public class Certificate
{
    public string Name { get; set; }
    /// <summary>
    /// use if PEM/Key
    /// </summary>
    public string Key { get; set; }
    public string Path { get; set; }
    /// <summary>
    /// use if PFX
    /// </summary>
    public string Password { get; set; }
}

public class JWT
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

public class ExternalAuthentication
{
    public ExternalSigninProvider Google { get; set; }
    public ExternalSigninProvider Facebook { get; set; }
}

public class Messaging
{
    public string Hostname { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class ExternalSigninProvider
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
