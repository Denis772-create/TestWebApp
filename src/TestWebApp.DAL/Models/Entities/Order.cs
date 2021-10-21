<<<<<<< HEAD
<<<<<<< HEAD

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

=======
﻿using System.Collections.Generic;
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e
=======
﻿using System.Collections.Generic;
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e

namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
<<<<<<< HEAD
=======
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e
    }
}