namespace OpenIdConnect.Db.Models;

public class ExternalLoginRole
{
    public Guid ExternalLoginId { get; set; }

    public int RoleId { get; set; }
}
