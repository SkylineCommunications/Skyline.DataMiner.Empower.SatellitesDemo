namespace Satellites
{
	/// <summary>
	/// Represents the position of a <see cref="Satellite"/> in space.
	/// </summary>
	public sealed class Position
	{
		public double Latitude { get; }
		public double Longitude { get; }
		public double Altitude { get; }

		internal Position(double latitude, double longitude, double altitude)
		{
			Latitude = latitude;
			Longitude = longitude;
			Altitude = altitude;
		}
	}
}
