using HotChocolate.Language;
using HotChocolate.Types;

namespace Server.Shared.GraphQL
{
    // Minimal DateTime scalar wrapper to ensure GraphQL DateTime consistency
    public class DateTimeType : ScalarType<System.DateTime, StringValueNode>
    {
        public DateTimeType() : base("DateTime") { }

        public override IValueNode ParseResult(object? resultValue)
        {
            if (resultValue is System.DateTime dt)
                return new StringValueNode(dt.ToString("o"));
            throw new SerializationException("Unable to parse result to DateTime", this);
        }

        public override System.DateTime ParseLiteral(StringValueNode valueSyntax)
        {
            return System.DateTime.Parse(valueSyntax.Value);
        }

        public override object? Serialize(System.DateTime runtimeValue)
        {
            return runtimeValue.ToString("o");
        }

        protected override System.DateTime ParseValue(StringValueNode valueSyntax)
        {
            return System.DateTime.Parse(valueSyntax.Value);
        }
    }
}
