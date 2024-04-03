using journal.api.Domain;

namespace journal.api.Dto;

public class GetAccountsResponse
{
    public IEnumerable<Account> Accounts { get; set; } = [];
}