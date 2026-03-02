using System.Runtime.CompilerServices;
using System.Text;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
    [CompilerGenerated]
    public class CoordinateGen : IEquatable<CoordinateGen>
    {

        public AngleGen Longitude { get; init; }
        public AngleGen Latitude { get; init; }

        public CoordinateGen(AngleGen Longitude, AngleGen Latitude) : base()
        {
            this.Longitude = Longitude;
            this.Latitude = Latitude;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("CoordinateGen");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack();
            builder.Append("Longitude = ");
            builder.Append(Longitude.ToString());
            builder.Append(", Latitude = ");
            builder.Append(Latitude.ToString());
            return true;
        }

        public static bool operator !=(CoordinateGen? left, CoordinateGen? right) =>     
        !(left == right);

        public static bool operator ==(
        CoordinateGen? left, CoordinateGen? right) =>
        ReferenceEquals(left, right) ||
        (left?.Equals(right) ?? false);

        public override int GetHashCode()
        {
            static int GetHashCode(AngleGen AngleGen) =>
            EqualityComparer<AngleGen>.Default.GetHashCode(AngleGen);

            return (EqualityComparer<Type>.Default.GetHashCode(
            EqualityContract()) * -1521134295
            + GetHashCode(Longitude)) * -1521134295
            + GetHashCode(Latitude);
        }

        public override bool Equals(object? obj) =>
        Equals(obj as CoordinateGen);

        public virtual bool Equals(CoordinateGen? other) =>
        (object)this == other || (other is not null
        && EqualityContract() == other!.EqualityContract()
        && EqualityComparer<AngleGen>.Default.Equals(
        Longitude, other!.Longitude)
        && EqualityComparer<AngleGen>.Default.Equals(
        Latitude, other!.Latitude));

        public void Deconstruct(
        out AngleGen Longitude, out AngleGen Latitude)
        {
            Longitude = this.Longitude;
            Latitude = this.Latitude;
        }

        protected virtual Type EqualityContract() => typeof(CoordinateGen);

        public Type ExternalEqualityContract => EqualityContract();


        // Actual name in IL is "<Clone>$". However, 
        // you can't add a Clone method to a record.
        public CoordinateGen Clone() => new(this);

        protected CoordinateGen(CoordinateGen original)
        {
            Longitude = original.Longitude; Latitude = original.Latitude;
        }
    }

}