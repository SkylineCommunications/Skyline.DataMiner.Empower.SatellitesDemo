# Skyline.DataMiner.Empower.SatellitesDemo

## About

This NuGet package is intended for use during an [Empower](https://empower.skyline.be/) demo.
It provides an API to access dummy satellite data on a DataMiner system.

### About the data

> [!NOTE]
> This package it does **not** provide actual current satellite information.

Satellite information is read from a TLE text file assumed to be present in the local file system at **C:\Skyline DataMiner\Documents\Empower\satellites.tle.txt**.
The satellite data is assumed to be retrieved from the [CelesTrak Satellite TLE API](https://celestrak.org/NORAD/elements/gp.php?GROUP=active&FORMAT=tle) on October 16th, 2023 at 13h28 UTC. 

To avoid satellite decay errors when retrieving the position of a satellite, only the time of day from the provided `DateTime` is used and added as an offset.
Therefore, the position will not match the real satellite position at that time.

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exist. In addition, you can leverage DataMiner Development Packages to build you own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

## Getting Started

### Retrieving satellites

```csharp
using Satellites;

var satellites = SatelliteAPI.GetSatellites();
```

### Getting satellite positions

```csharp
var satellite = satellites[0];

// Get the position of the satellite at the current time
var currentPosition = satellite.GetPosition();

// Get the position of the satellite in one hour from now
var nextHour = DateTime.UtcNow.AddHours(1);
var nextHourPosition = satellite.GetPosition(nextHour);
```

### Getting satellite positions periodically

This package contains a `Trigger` class to help you update satellite positions every second.

```csharp
var trigger = new Trigger(() => {
	var updatedPosition = satellite.GetPosition();
	
	// Do something with the updated position
	...
});
```