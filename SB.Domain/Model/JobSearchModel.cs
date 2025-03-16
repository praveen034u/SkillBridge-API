using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace SB.Domain.Model;

public class JobSearchModel
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string Id { get; set; }

    [SearchableField(IsSortable = true)]
    public string Title { get; set; }

    [SearchableField]
    public string Description { get; set; }

    [SearchableField]
    public string Location { get; set; }

    [SimpleField(IsFilterable = true)]
    public string Company { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true)]
    public DateTime PostedDate { get; set; }
}


