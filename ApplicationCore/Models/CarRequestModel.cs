using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class CarRequestModel : RequestModel
{
    [Range(0.050f, 100f)] public float CargoMass { get; set; }

    [Range(0.10f, 100f)] public float? CargoLength { get; set; }

    [Range(0.10f, 100f)] public float? CargoWidth { get; set; }

    [Range(0.10f, 100f)] public float? CargoHeight { get; set; }

    [Range(0.0010f, 50000f)] public float CargoVolume { get; set; }
}
