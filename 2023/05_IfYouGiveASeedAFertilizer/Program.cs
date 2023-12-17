using System.Runtime.CompilerServices;
using _05_IfYouGiveASeedAFertilizer;

var almanach = Inputs.ParseInput(Inputs.Puzzle);

almanach.Print();

var nearestLocation = almanach.SeedRanges
    .SelectMany(r => AdaptMapping(almanach.SeedToSoil!, r.From, r.To))
    .SelectMany(m => AdaptMapping(almanach.SoilToFertilizer!, m.Destination, m.DestinationTo))
    .SelectMany(m => AdaptMapping(almanach.FertilizerToWater!, m.Destination, m.DestinationTo))
    .SelectMany(m => AdaptMapping(almanach.WaterToLight!, m.Destination, m.DestinationTo))
    .SelectMany(m => AdaptMapping(almanach.LightToTemperature!, m.Destination, m.DestinationTo))
    .SelectMany(m => AdaptMapping(almanach.TemperatureToHumidity!, m.Destination, m.DestinationTo))
    .SelectMany(m => AdaptMapping(almanach.HumidityToLocation!, m.Destination, m.DestinationTo))
    .Min(m => m.Destination);

Console.WriteLine($"Nearest location: {nearestLocation}");

long Map(IReadOnlyList<Mapping> mappings, long from)
{
    foreach (var mapping in mappings)
    {
        if (from >= mapping.Source && from <= mapping.Source + mapping.Length)
        {
            return mapping.Destination + (from - mapping.Source);
        }
    }

    return from;
}

IReadOnlyList<Mapping> AdaptMapping(IReadOnlyList<Mapping> mappings, long from, long to, [CallerArgumentExpression(nameof(mappings))] string? paramName = null)
{
    var adaptedMappings = mappings
        .Where(m => m.Source <= to && m.Source + m.Length - 1 >= from)
        .Select(m =>
        {
            var mapping = m;
            if (mapping.Source < from)
            {
                mapping = new Mapping
                {
                    Source = from,
                    Destination = mapping.Destination + from - mapping.Source,
                    Length = mapping.Length - (from - mapping.Source)
                };
            }

            if (mapping.Source + mapping.Length - 1 > to)
            {
                mapping = mapping with
                {
                    Source = mapping.Source,
                    Length = to - mapping.Source + 1
                };
            }

            return mapping;
        })
        .OrderBy(m => m.Source)
        .ToList();

    if (adaptedMappings.Count == 0)
    {
        adaptedMappings.Add(new Mapping
        {
            Source = from,
            Destination = from,
            Length = to - from + 1
        });
    }
    else
    {
        var firstMapping = adaptedMappings[0]; 
        if (firstMapping.Source > from)
        {
            adaptedMappings.Insert(0, new Mapping
            {
                Source = from,
                Destination = from,
                Length = firstMapping.Source - from
            });
        }

        for (int i = 0; i < adaptedMappings.Count - 1; i++)
        {
            var mapping = adaptedMappings[i];
            var nextMapping = adaptedMappings[i + 1];
            if (mapping.Source + mapping.Length < nextMapping.Source)
            {
                adaptedMappings.Insert(i + 1, new Mapping
                {
                    Source = mapping.Source + mapping.Length,
                    Destination = mapping.Source + mapping.Length,
                    Length = nextMapping.Source - mapping.Source - mapping.Length
                });

                i++;
            }
        }

        var lastMapping = adaptedMappings.Last();
        if (lastMapping.Source + lastMapping.Length - 1 < to)
        {
            adaptedMappings.Add(new Mapping
            {
                Source = lastMapping.Source + lastMapping.Length,
                Destination = lastMapping.Source + lastMapping.Length,
                Length = to - lastMapping.Source - lastMapping.Length + 1
            });
        }
    }
    
    Console.WriteLine($"Possible mappings in {paramName} from {from} to {to}:");
    foreach (var mapping in adaptedMappings)
    {
        Console.WriteLine(mapping);
    }

    return adaptedMappings;
}