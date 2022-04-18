﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public abstract class Entity :IEquatable<Entity>
    {
        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}
