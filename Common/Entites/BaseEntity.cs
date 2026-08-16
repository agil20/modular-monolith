using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entites
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
