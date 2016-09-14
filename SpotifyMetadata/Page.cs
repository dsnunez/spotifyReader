using System.Collections.Generic;
using System;
using System.Linq;

namespace SpotifyMetadata
{
    public class Page<T>
    {
        private const int _defaultPerPage = 5;
        public Page(int pageNum, int perPage, IOrderedQueryable<T> completeList, string query)
        {
            pageNum = Math.Max(pageNum, 1);
            perPage = perPage < 0 ? _defaultPerPage : perPage;

            int completeListCount = completeList.Count();
            int lastPossibleIndex = completeListCount - 1;
            int maxPages = (int)Math.Ceiling((double)completeListCount / perPage);
            int lastPageResultCount = completeListCount % perPage;

            int skip = perPage * (pageNum - 1);
            OutOfBounds = skip > lastPossibleIndex;
            skip = Math.Min(skip, completeListCount - lastPageResultCount);

            int take = perPage;
            take = take == 0 ? take = completeListCount : take;

            Items = completeList.Skip(skip).Take(take).ToList();

            Offset = skip;
            PerPage = perPage;
            NextPage = skip + take < completeListCount;
            CurrentPage = Math.Min(pageNum, maxPages);
            Query = query;
        }

        public List<T> Items { get; internal set; }
        public int PerPage { get; internal set; }
        public int Offset { get; internal set; }

        public bool OutOfBounds { get; internal set; }

        public bool NextPage { get; internal set; }

        public int CurrentPage { get; internal set; }

        public string Query { get; set; }

    }
}