using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MockSem3FishCatch.models
{
    [DataContract]
    public class Catch
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string  fisherName { get; set; }

        [DataMember]
        public string  fishType { get; set; }

        [DataMember]
        public double weight { get; set; }

        [DataMember]
        public string place { get; set; }

        [DataMember]
        public int week { get; set; }

    }
}