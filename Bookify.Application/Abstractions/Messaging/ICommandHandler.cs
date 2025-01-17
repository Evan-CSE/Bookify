﻿using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : ICommand
    {
    }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : ICommand<TResponse>
    { }
}
