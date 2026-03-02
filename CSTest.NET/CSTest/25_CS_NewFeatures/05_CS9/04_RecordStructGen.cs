using System.Text;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
    public readonly struct AngleGen : IEquatable<AngleGen>
    {
        public int Degrees { get; init; }
        public int Minutes { get; init; }
        public int Seconds { get; init; }
        public string? Name { get; init; }
        public AngleGen(int Degrees, int Minutes, int Seconds, string? Name)
        {
            this.Degrees = Degrees;
            this.Minutes = Minutes;
            this.Seconds = Seconds;
            this.Name = Name;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("Angle");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append("Degrees = ");
            builder.Append(Degrees);
            builder.Append(", Minutes = ");
            builder.Append(Minutes);
            builder.Append(", Seconds = ");
            builder.Append(Seconds);
            builder.Append(", Name = ");
            builder.Append(Name);
            return true;
        }

        public static bool operator !=(AngleGen left, AngleGen right) =>
        !(left == right);

        public static bool operator ==(AngleGen left, AngleGen right) =>
        left.Equals(right);

        public override int GetHashCode()
        {
            static int GetHashCode(int integer) =>
            EqualityComparer<int>.Default.GetHashCode(integer);

            return GetHashCode(Degrees) * -1521134295
            + GetHashCode(Minutes) * -1521134295
            + GetHashCode(Seconds) * -1521134295
            + EqualityComparer<string>.Default.GetHashCode(Name!);
        }

        public override bool Equals(object? obj) =>
        obj is AngleGen angle && Equals(angle);

        public bool Equals(AngleGen other) =>
        EqualityComparer<int>.Default.Equals(Degrees, other.Degrees)
        && EqualityComparer<int>.Default.Equals(Minutes, other.Minutes)
        && EqualityComparer<int>.Default.Equals(Seconds, other.Seconds)
        && EqualityComparer<string>.Default.Equals(Name, other.Name);

        public void Deconstruct(out int Degrees, out int Minutes,
        out int Seconds, out string? Name)
        => (Degrees, Minutes, Seconds, Name) =
        (this.Degrees, this.Minutes, this.Seconds, this.Name);
    }

}