namespace Ratbags.Core.Settings;

public class AppSettingsBase
{
    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public Ports Ports { get; set; } = default!;
    public Certificate Certificate { get; set; } = default!;
    public JWT JWT { get; set; } = default!;
    public ExternalAuthentication ExternalAuthentication { get; set; } = default!;
    public Messaging Messaging { get; set; } = default!;
    public string AllowedHosts { get; set; } = default!;
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; } = default!;
}

public class Ports
{
    public string Http { get; set; } = default!;
    public string Https { get; set; } = default!;
}

public class Certificate
{
    public string Name { get; set; } = default!;
    /// <summary>
    /// use if PEM/Key
    /// </summary>
    public string Key { get; set; } = default!;
    public string Path { get; set; } = default!;
    /// <summary>
    /// use if PFX
    /// </summary>
    public string Password { get; set; } = default!;
}

public class JWT
{
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}

public class ExternalAuthentication
{
    public ExternalSigninProvider Google { get; set; } = default!;
    public ExternalSigninProvider Facebook { get; set; } = default!;
}
public class ExternalSigninProvider
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}

public class Messaging
{
    public MessagingASB ASB { get; set; } = default!;
}

public class MessagingASB
{
    public string Connection { get; set; } = default!;

    public string ResponseTopic { get; set; } = default!;

    public string ResponseSubscription { get; set; } = default!;
}