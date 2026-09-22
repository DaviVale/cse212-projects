public class FeatureCollection
{
    // Property called Features
    // It should store multiple Feature objects.
    public Feature[] Features { get; set; } = [];
}

public class Feature
{
    // Property called Properties
    // Its type should be the class below.
    public Properties Properties { get; set; } = new();
}

public class Properties
{
    // Property called Mag
    // Magnitude is a decimal number and can potentially be null.

    // Property called Place
    // Place is text and can potentially be null.
    public double? Mag { get; set; }
    public string Place { get; set; } = "";
}