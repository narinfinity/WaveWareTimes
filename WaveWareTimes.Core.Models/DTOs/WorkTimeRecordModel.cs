using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WaveWareTimes.Core.Models
{
    public class WorkTimeRecordModel
    {
        public WorkTimeRecordModel()
        {

        }
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description
        {
            get
            {
                return string.Format("The record has been created by {0} for a period between {1:MMM dd, yyyy} and {2:MMM dd, yyyy}", 
                    CreatedByUserName, 
                    Start, 
                    End);
            }
        }

        public string CreatedByUserName { get; set; }
    }


}
