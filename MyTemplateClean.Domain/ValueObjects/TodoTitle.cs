namespace MyTemplateClean.Domain.ValueObjects;

    public record TodoTitle
    {
        private const int DefaultLength = 5;
        
        public string Value { get; }
        
        private TodoTitle(string value) => Value = value;

        public static TodoTitle Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new TodoTitle(value);
        }
        
    }