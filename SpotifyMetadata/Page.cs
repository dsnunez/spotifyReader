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
            //Positive indexes
            pageNum = Math.Max(pageNum, 1);
            perPage = perPage < 0 ? _defaultPerPage : perPage;

            //Needed to calculate "skip" and "take"
            int completeListCount = completeList.Count();
            int lastPossibleIndex = completeListCount - 1;
            int maxPages = (int)Math.Ceiling((double)completeListCount / perPage);
            int lastPageResultCount = completeListCount % perPage;

            //try to skip all the items from previous pages...
            int skip = perPage * (pageNum - 1);
            //... but let the user know if s/he asked for a non-existent page
            OutOfBounds = skip > lastPossibleIndex;
            //... and then truncate skip if necessary
            skip = Math.Min(skip, completeListCount - lastPageResultCount);

            int take = perPage;
            //If "perPage" is zero, is a way to say "take them all"
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