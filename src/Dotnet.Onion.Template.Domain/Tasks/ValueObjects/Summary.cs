using System;
using System.Collections.Generic;
using System.Text;


/*
 * Encapsulate the tiny domain business rules. Structures that are unique 
 * by their properties and the whole object is immutable, once it is created
 * its state can not change.
 * https://martinfowler.com/bliki/ValueObject.html
 */

namespace Dotnet.Onion.Template.Domain.Tasks.ValueObjects
{
    public readonly struct Summary
    {
        private readonly string _text;

        public Summary(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"Summary value is required");
            }

            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
