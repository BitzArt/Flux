﻿using Microsoft.Extensions.DependencyInjection;

namespace Flex;

internal class CommunicatorBuilder : ICommunicatorBuilder
{
	public IServiceCollection Services { get; private set; }
	public ICommunicatorServiceFactory Factory { get; init; }

    public CommunicatorBuilder(IServiceCollection services)
	{
		Services = services;
		Factory = new CommunicatorServiceFactory();
	}
}
