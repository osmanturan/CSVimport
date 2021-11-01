using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public enum Operation
    {
        NotSet = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        CreateOrUpdate = 4
    }
}
