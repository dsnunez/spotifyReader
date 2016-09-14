﻿using System.Collections.Generic;
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

            int limitTruncated = Math.Max(limit, 0);
            Limit = limitTruncated;
            limitTruncated = limitTruncated == 0 ? limitTruncated = completeListCount - offset : limitTruncated;
            limitTruncated = Math.Min(limitTruncated, completeListCount - offset);
            

            Items = completeList.Skip(offset).Take(limitTruncated).ToList();

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