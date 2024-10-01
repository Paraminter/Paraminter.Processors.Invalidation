﻿namespace Paraminter.Processing.Invalidation;

using Paraminter.Cqs;
using Paraminter.Processing.Invalidation.Queries;

/// <summary>Handles queries by reading the invalidity status.</summary>
/// <typeparam name="TQuery">The type of the handled queries.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public sealed class ProcessInvalidityReadingQueryHandler<TQuery, TResponse>
    : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery
{
    private readonly IQueryHandler<IIsProcessInvalidatedQuery, TResponse> InvalidityReader;

    /// <summary>Instantiates a query-handler which reads the invalidity status.</summary>
    /// <param name="invalidityReader">Reads the invalidity status.</param>
    public ProcessInvalidityReadingQueryHandler(
        IQueryHandler<IIsProcessInvalidatedQuery, TResponse> invalidityReader)
    {
        InvalidityReader = invalidityReader ?? throw new System.ArgumentNullException(nameof(invalidityReader));
    }

    TResponse IQueryHandler<TQuery, TResponse>.Handle(
        TQuery query)
    {
        if (query is null)
        {
            throw new System.ArgumentNullException(nameof(query));
        }

        return InvalidityReader.Handle(IsProcessInvalidatedQuery.Instance);
    }
}