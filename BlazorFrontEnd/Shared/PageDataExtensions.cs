// <copyright file="PageDataExtensions.cs" company="Silver Fern.">
// Copyright (c) Silver Fern. All rights reserved.
// </copyright>

using MudBlazor;

namespace BlazorFrontEnd.Shared;

public static class PageDataExtensions
{
    public static PageData SetSearchString(this PageData pageData, string searchString) =>
        pageData with
        {
            SearchString = searchString,
            FirstNumberOfRows = pageData.CurrentPageSize,
            BeforeCursor = null,
            LastNumberOfRows = null,
            AfterCursor = null,
            CurrentPageNumber = 0
        };

    private static PageData SetFirstPage(this PageData pageData) =>
        pageData with
        {
            FirstNumberOfRows = pageData.CurrentPageSize,
            BeforeCursor = null,
            LastNumberOfRows = null,
            AfterCursor = null,
            CurrentPageNumber = 0
        };

    private static PageData SetLastPage(this PageData pageData, int pageNumber) =>
        pageData with
        {
            FirstNumberOfRows = null,
            BeforeCursor = null,
            LastNumberOfRows = pageData.CurrentPageSize,
            AfterCursor = null,
            CurrentPageNumber = pageNumber
        };

    private static PageData SetNextPage(this PageData pageData, int pageNumber, string? cursor) =>
        pageData with
        {
            FirstNumberOfRows = pageData.CurrentPageSize,
            AfterCursor = cursor,
            LastNumberOfRows = null,
            BeforeCursor = null,
            CurrentPageNumber = pageNumber
        };

    private static PageData SetPreviousPage(this PageData pageData, int pageNumber, string? cursor) =>
        pageData with
        {
            FirstNumberOfRows = null,
            AfterCursor = null,
            LastNumberOfRows = pageData.CurrentPageSize,
            BeforeCursor = cursor,
            CurrentPageNumber = pageNumber
        };

    private static PageData SetPageSize(this PageData pageData, int newPageSize) =>
        pageData switch
        {
            { LastNumberOfRows: { } } => pageData with
            {
                CurrentPageSize = newPageSize,
                FirstNumberOfRows = null,
                AfterCursor = null,
                LastNumberOfRows = newPageSize,
                BeforeCursor = null
            },
            _ => pageData with
            {
                CurrentPageSize = newPageSize,
                FirstNumberOfRows = newPageSize,
                AfterCursor = null,
                LastNumberOfRows = null,
                BeforeCursor = null
            }
        };

    public static PageData SetPaging(this PageData pageData, TableState state, string? startCursor, string? endCursor) =>
        state switch
        {
            { PageSize: var size } when size != pageData.CurrentPageSize => pageData.SetPageSize(size),
            { Page: 0 } => pageData.SetFirstPage(),
            { Page: var page } when page >= pageData.LastPageNumber => pageData.SetLastPage(state.Page),
            { Page: var page } when page > pageData.CurrentPageNumber => pageData.SetNextPage(page, endCursor),
            { Page: var page } when page < pageData.CurrentPageNumber => pageData.SetPreviousPage(page, startCursor),
            _ => pageData
        };

}