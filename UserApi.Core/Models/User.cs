namespace UserApi.Core.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Suffix { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
