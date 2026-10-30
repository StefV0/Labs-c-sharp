using Business.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Tokens
{
    public class Punctuation : IToken
    {
        public string Value { get; set; }

        public Punctuation()
        {
            Value = string.Empty;
        }

        public Punctuation(string value)
        {
            Value = value;
        }


        public bool IsSentenceEnding =>
            Value.Contains('.') || Value.Contains('!') || Value.Contains('?') || Value.Contains('…');


        public bool IsQuestion => Value.Contains('?');

        public override string ToString() => Value;
    }

}
