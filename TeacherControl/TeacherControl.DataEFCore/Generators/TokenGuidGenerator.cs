using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.Models;

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
            return Guid.NewGuid().ToString().Split("-").Last().ToLower();
        }
    }
}
