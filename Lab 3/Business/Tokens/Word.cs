using Business.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Business.Tokens
{
    [XmlType("word")]
    public class Word : IToken
    {
        [XmlText]
        public string Value { get; set; }

        [XmlIgnore]
        public int Length => Value?.Length ?? 0;

        public Word()
        {
            Value = string.Empty;
        }

        public Word(string value)
        {
            Value = value;
        }

        public bool StartsWithConsonant()
        {
            if (string.IsNullOrEmpty(Value))
                return false;

            char firstChar = char.ToLowerInvariant(Value[0]);
            if (!char.IsLetter(firstChar))
                return false;


            const string vowels = "aeiouyаеёиоуыэюя";
            return !vowels.Contains(firstChar);
        }

        public override string ToString() => Value;
    }

}
