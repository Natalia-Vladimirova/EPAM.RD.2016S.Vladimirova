﻿using System;

namespace Attributes
{
    // Should be applied to classes only.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstantiateUserAttribute : Attribute
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Id { get; set; }

        public InstantiateUserAttribute(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public InstantiateUserAttribute(int id, string firstName, string lastName)
            : this(firstName, lastName)
        {
            Id = id;
        }
    }
}
