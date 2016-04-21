using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WaveWareTimes.Service.Models
{
    [DataContract]
    public class WorkTimeRecordModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CreatedByUserName { get; set; }
    }
}
