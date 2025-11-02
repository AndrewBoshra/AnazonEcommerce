
namespace Anazon.Shared.Contracts;


public abstract class BaseQuery
{
    public string? Q { set;  get; }
}

public abstract class BasePagedQuery : BaseQuery
{
    public int? PageSize { set;  get; }
    public int? Page { set;  get; }
}

