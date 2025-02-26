using SGPdotNET.Propagation;
using SGPdotNET.TLE;
using System;

namespace Satellites
{
	/// <summary>
	/// Represents a satellite orbiting in space.
	/// </summary>
	public sealed class Satellite
	{
		/// <summary>
		/// The name of the satellite.
		/// </summary>
		public string Name { get; }

		private readonly Sgp4 _model;

		internal Satellite(string name, string line1, string line2)
		{
			Name = name;
			var tle = new Tle(name, line1, line2);
			_model = new Sgp4(tle);
		}

		/// <summary>
		/// Calculates where the satellite is currently located.
		/// </summary>
		/// <returns>The current position of the satellite.</returns>
		public Position GetPosition()
		{
			return GetPosition(DateTime.UtcNow);
		}

		/// <summary>
		/// Calculates where the satellites is located at a specific time.
		/// </summary>
		/// <param name="time">The time at which to determine the position.</param>
		/// <returns>The position of the satellite at the specified <paramref name="time"/>.</returns>
		public Position GetPosition(DateTime time)
		{
			try
			{
                time = SatelliteAPI.TleOrigin + time.TimeOfDay;

                var eci = _model.FindPosition(time);
				var geo = eci.ToGeodetic();

				var latitude = geo.Latitude.Degrees;
				var longitude = geo.Longitude.Degrees;
				if (longitude > 180)
					longitude -= 360;
				var altitude = geo.Altitude;

				return new Position(latitude, longitude, altitude);
			}
			catch
			{
				return new Position(0, 0, 0);
			}
		}
	}
}
