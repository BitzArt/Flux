using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitzArt.Flux;

public class FluxSetOperationOnNonFluxQueryableException : Exception
{
    public FluxSetOperationOnNonFluxQueryableException(string methodName)
        : base($"Calling {methodName} is only supported with Flux Sets")
    {
    }
}
