using System.Collections.Generic;
using System;

namespace SpotifyMetadata
{
    public class Page<T>
    {
        public Page(int pageNum, int offset, int limit, List<T> completeList)
        {
            offset = Math.Max(offset, 0);
            offset = Math.Min(offset, completeList.Count);
            Offset = offset;

            limit = Math.Max(limit, 0);
            limit = limit == 0 ? limit = completeList.Count - offset : limit;
            limit = Math.Min(limit, completeList.Count - offset);
            Limit = limit;

            Items = completeList.GetRange(offset, limit);

            NextPage = offset + limit < completeList.Count;
            CurrentPage = pageNum;
        }

        public List<T> Items { get; internal set; }
        public int Limit { get; internal set; }
        public int Offset { get; internal set; }

        public bool NextPage { get; internal set; }

        public int CurrentPage { get; internal set; }

    }
}