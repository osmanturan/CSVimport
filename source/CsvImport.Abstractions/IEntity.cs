using System;

namespace CsvImport
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
