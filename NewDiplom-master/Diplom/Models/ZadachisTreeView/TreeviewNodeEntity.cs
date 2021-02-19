using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Diplom.Models.ZadachisTreeView
{
    public class TreeviewNodeEntity
    {
        //[DataMember, NonSerialized]
        public int id { get; set; }

        //[DataMember, NonSerialized]
        public int? parentid { get; set; }

        //[DataMember, NonSerialized]
        public string text { get; set; }

        public TreeviewNodeEntity[] children { get; set; }
    }
}
