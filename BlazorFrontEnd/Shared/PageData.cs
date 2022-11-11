// <copyright file="PageData.cs" company="Silver Fern.">
// Copyright (c) Silver Fern. All rights reserved.
// </copyright>

using System.Reflection;
using MudBlazor;

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

    public static TSort[]? GetSortInput<TSort>(string fieldName, SortDirection sortDirection) where TSort : new()
    {
        if (string.IsNullOrWhiteSpace(fieldName))
        {
            return null;
        }

        TSort sort = new();
        Type type = typeof(TSort);
        PropertyInfo sortProperty = type.GetProperty(fieldName) ??
                                    throw new NullReferenceException($"{fieldName} property does not exist in type.");
        sortProperty.SetValue(sort, MapSortDirection(sortDirection));

        return new[] { sort };
    }

    private static SortEnumType MapSortDirection(SortDirection sortDirection) =>
        sortDirection switch
        {
            SortDirection.Ascending => SortEnumType.Asc,
            SortDirection.Descending => SortEnumType.Desc,
            _ => SortEnumType.Asc
        };

    private const string NameFilterField = "Name";
    public TFilter? GetFilterInput<TFilter>() where TFilter : new()
    {
        if (string.IsNullOrWhiteSpace(SearchString))
        {
            return default;
        }
        
        TFilter filter = new();
        Type type = typeof(TFilter);
        PropertyInfo andProperty = type.GetProperty("And") ??
                                   throw new NullReferenceException("And property does not exist in type.");

        TFilter filterField = new();
        PropertyInfo filterFieldProperty = type.GetProperty(NameFilterField) ??
                                           throw new NullReferenceException(
                                               $"{NameFilterField} property does not exist in type.");

        filterFieldProperty.SetValue(filterField, new StringOperationFilterInput { Contains = SearchString });

        andProperty.SetValue(filter, new List<TFilter>(new[] { filterField }));

        return filter;
    }
}
