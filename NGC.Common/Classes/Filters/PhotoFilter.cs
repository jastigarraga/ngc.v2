using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace NGC.Common.Classes.Filters
{
    public class PhotoFilter : IMerakiFilter<Photo>
    {
        public int Id { get; set; }

        public int IdCustomer { get; set; }
        public Expression<Func<Photo, bool>> FilterExpression => 
            p=> (Id == 0 || p.Id == Id) &&
                (IdCustomer == 0 || p.IdCustomer == IdCustomer);
    }
}
