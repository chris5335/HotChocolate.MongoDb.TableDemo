// <copyright file="PageData.cs" company="Silver Fern.">
// Copyright (c) Silver Fern. All rights reserved.
// </copyright>

namespace BlazorFrontEnd.Shared;

public sealed record PageData
{
    public int? FirstNumberOfRows { get; init; }

    public string? AfterCursor { get; init; }

    public string? BeforeCursor { get; init; }

    public int? LastNumberOfRows { get; init; }

    public int CurrentPageNumber { get; init; }
    public int CurrentPageSize { get; init; } = 10;

    public int TotalCount { get; init; }
    public int PageCount => (TotalCount + CurrentPageSize - 1) / CurrentPageSize;
    public int LastPageNumber => PageCount - 1;
    public string SearchString { get; init; } = string.Empty;

    public PersonSortInput[]? SortInput { get; init; }

    public PersonFilterInput FilterInput => GetFilterInput();

    private PersonFilterInput GetFilterInput() => new() { And = GetFilterAnd().ToList() };

    private IEnumerable<PersonFilterInput> GetFilterAnd()
    {
        if (string.IsNullOrWhiteSpace(SearchString) == false)
        {
            yield return new PersonFilterInput
                { Name = new StringOperationFilterInput { Contains = SearchString } };
        }
    }
}