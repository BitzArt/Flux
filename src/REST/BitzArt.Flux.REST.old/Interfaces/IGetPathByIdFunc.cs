namespace BitzArt.Flux.REST;

internal interface IGetPathByIdFunc
{
    Func<object?, string>? Value { get; set; }
}
