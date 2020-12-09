using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Processed
{
    public class ProcessedComparer : IEqualityComparer<Processed>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Processed x, Processed y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x.Id, y.Id)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.RequestId.Equals(y.RequestId);
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Processed processed)
        {
            //Check whether the object is null
            return processed.RequestId.GetHashCode();
        }
    }
}
