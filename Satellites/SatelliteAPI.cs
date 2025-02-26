using System;
using System.Collections.Generic;
using System.IO;

namespace Satellites
{
    /// <summary>
    /// An API that can be used to retrieve satellites orbiting in space.
    /// </summary>
    public static class SatelliteAPI
    {
        internal const int MaxSatelliteCount = 100;
        internal const string TlePath = @"C:\Skyline DataMiner\Documents\Empower\satellites.tle.txt";
        internal static readonly DateTime TleOrigin = new DateTime(2023, 10, 16, 13, 28, 00, DateTimeKind.Utc);

        private static readonly object _lock = new object();
        private static IReadOnlyList<Satellite> _satellites;

        /// <summary>
        /// Retrieves the list of known satellites.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<Satellite> GetSatellites()
        {
            if (_satellites != null)
                return _satellites;

            lock (_lock)
            {
                if (_satellites != null)
                    return _satellites;

                _satellites = ReadSatellites();
            }

            return _satellites;
        }

        private static List<Satellite> ReadSatellites()
        {
            if (!File.Exists(TlePath))
                throw new FileNotFoundException("Failed to find satellite data", TlePath);

            try
            {
                using (var reader = File.OpenText(TlePath))
                {
                    return ReadSatellites(reader);
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read satellite data", ex);
            }
        }

        private static List<Satellite> ReadSatellites(StreamReader reader)
        {
            var satellites = new List<Satellite>();

            while (satellites.Count < MaxSatelliteCount && reader.Peek() >= 0)
            {
                var name = reader.ReadLine();
                var line1 = reader.ReadLine();
                var line2 = reader.ReadLine();

                if (!TryCreateSatellite(name, line1, line2, out var satellite))
                    continue;

                satellites.Add(satellite);
            }

            return satellites;
        }

        private static bool TryCreateSatellite(string name, string line1, string line2, out Satellite satellite)
        {
            try
            {
                satellite = new Satellite(name, line1, line2);
                return true;
            }
            catch
            {
                satellite = null;
                return false;
            }
        }
    }
}
