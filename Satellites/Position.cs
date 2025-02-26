using SGPdotNET.CoordinateSystem;

namespace Satellites
{
    /// <summary>
    /// Represents the position of a <see cref="Satellite"/> in space.
    /// </summary>
    public sealed class Position
    {
        /// <summary>
        /// Latitude in degrees.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Longitude in degrees.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Altitude in kilometers.
        /// </summary>
        public double Altitude { get; }

        internal Position(GeodeticCoordinate coordinate)
        {
            Latitude = coordinate.Latitude.Degrees;
            Longitude = coordinate.Longitude.Degrees;
            if (Longitude > 180)
                Longitude -= 360;
            Altitude = coordinate.Altitude;
        }

        internal Position()
        {
            Latitude = 0;
            Longitude = 0;
            Altitude = 0;
        }
    }
}
