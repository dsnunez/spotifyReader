using System.Collections.Generic;
using System;
using System.Linq;

namespace SpotifyMetadata
{
    public class Page<T>
    {
        public Page(int pageNum, int offset, int limit, IOrderedQueryable<T> completeList)
        {
            int completeListCount = completeList.Count();

            offset = Math.Max(offset, 0);
            offset = Math.Min(offset, completeListCount);
            Offset = offset;

            limit = Math.Max(limit, 0);
            limit = limit == 0 ? limit = completeListCount - offset : limit;
            limit = Math.Min(limit, completeListCount - offset);
            Limit = limit;

            Items = completeList.Skip(offset).Take(limit).ToList();

            NextPage = offset + limit < completeListCount;
            CurrentPage = pageNum;
        }

        public List<T> Items { get; internal set; }
        public int Limit { get; internal set; }
        public int Offset { get; internal set; }

        public bool NextPage { get; internal set; }

        public int CurrentPage { get; internal set; }

    }
}