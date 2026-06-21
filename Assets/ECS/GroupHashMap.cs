using System;
using System.Collections.Generic;
using Svelto.ECS;

namespace ECS
{
    public static class GroupHashMap
    {
        public static Dictionary<Type, ExclusiveGroupStruct> Map = new();
    }
}