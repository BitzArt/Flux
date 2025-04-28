namespace BitzArt.Flux.Services;

internal interface ITerminatable
{
    /// <summary>
    /// <para>
    /// Terminates the <see cref="IFluxServiceBuilder"/>, restricting any further terminations.
    /// </para>
    /// <para>
    /// This method is a part of internal implementation details and should not be used directly. <br />
    /// It is only exposed for the purposes of <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
    /// </para>
    /// </summary>
    public void Terminate();
}