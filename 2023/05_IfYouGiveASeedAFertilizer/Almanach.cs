namespace _05_IfYouGiveASeedAFertilizer;

public class Almanach
{
    public List<long> Seeds { get; set; } = new();

    public List<SeedRange> SeedRanges { get; set; } = new();
    
    public IReadOnlyList<Mapping>? SeedToSoil { get; set; }
    
    public IReadOnlyList<Mapping>? SoilToFertilizer { get; set; }
    
    public IReadOnlyList<Mapping>? FertilizerToWater { get; set; }
    
    public IReadOnlyList<Mapping>? WaterToLight { get; set; }
    
    public IReadOnlyList<Mapping>? LightToTemperature { get; set; }
    
    public IReadOnlyList<Mapping>? TemperatureToHumidity { get; set; }
    
    public IReadOnlyList<Mapping>? HumidityToLocation { get; set; }

    public void Print()
    {
        Console.WriteLine($"Seeds: {string.Join(", ", Seeds.Select(s => s.ToString()))}");
        foreach (var seedRange in SeedRanges)
        {
            Console.WriteLine(seedRange);
        }

        PrintMapping("Seed to soil:", SeedToSoil);
        PrintMapping("Soil to fertilizer:", SoilToFertilizer);
        PrintMapping("Fertilizer to Water", FertilizerToWater);
        PrintMapping("Water to Light", WaterToLight);
        PrintMapping("Light to Temperature", LightToTemperature);
        PrintMapping("Temperature to Humidity", TemperatureToHumidity);
        PrintMapping("Humidity to Location", HumidityToLocation);
    }

    private static void PrintMapping(string caption, IReadOnlyList<Mapping>? mapping)
    {
        if (mapping is not null)
        {
            Console.WriteLine();
            Console.WriteLine(caption);
            foreach (var map in mapping)
            {
                Console.WriteLine(map);
            }
        }
    }
}