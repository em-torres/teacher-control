using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.Generators
{
    public class TokenGuidGenerator : ValueGenerator<string>
    {

        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            Guid guid = Guid.NewGuid();
            if (entry.Metadata.ClrType == typeof(Assignment))
            {
                return guid.ToString().Split('-').Last();
            }
          
            return string.Join(string.Empty, guid.ToString().Split("-").ToArray());
        }
    }
}
