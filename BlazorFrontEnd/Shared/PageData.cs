// <copyright file="PageData.cs" company="Silver Fern.">
// Copyright (c) Silver Fern. All rights reserved.
// </copyright>

namespace BlazorFrontEnd.Shared;

public sealed record PageData
{
    private readonly string _searchString = string.Empty;
    private readonly int? _firstNumberOfRows;
    private readonly string? _beforeCursor;
    private readonly int? _lastNumberOfRows;
    private readonly string? _afterCursor;

    public int? FirstNumberOfRows
    {
        get => _firstNumberOfRows;
        init
        {
            _firstNumberOfRows = value;
            _lastNumberOfRows = null;
            _beforeCursor = null;
        }
    }

    public string? AfterCursor
    {
        get => _afterCursor;
        init
        {
            _afterCursor = value;
            _lastNumberOfRows = null;
            _beforeCursor = null;
        }
    }

    public string? BeforeCursor
    {
        get => _beforeCursor;
        init
        {
            _beforeCursor = value;
            _firstNumberOfRows = null;
            _afterCursor = null;
        }
    }

    public int? LastNumberOfRows
    {
        get => _lastNumberOfRows;
        init
        {
            _lastNumberOfRows = value;
            _firstNumberOfRows = null;
            _afterCursor = null;
        }
    }


    public int CurrentPageNumber { get; init; } = 1;
    public int CurrentPageSize { get; init; } = 10;

    public string SearchString
    {
        get => _searchString;
        init
        {
            _searchString = value;
            FirstNumberOfRows = null;
            BeforeCursor = null;
            LastNumberOfRows = null;
            AfterCursor = null;
            CurrentPageNumber = 1;
        }
    }

    public PersonSortInput[]? SortInput { get; init; }

    public  PersonFilterInput FilterInput
    {
        get => GetFilterInput();
        init { }
    }

    private PersonFilterInput GetFilterInput() => new() { And = GetFilterAnd().ToList() };

    private IEnumerable<PersonFilterInput> GetFilterAnd()
    {
        if (string.IsNullOrWhiteSpace(_searchString))
        {
            yield return new PersonFilterInput
                { Name = new StringOperationFilterInput { Contains = SearchString } };
        }
    }
}