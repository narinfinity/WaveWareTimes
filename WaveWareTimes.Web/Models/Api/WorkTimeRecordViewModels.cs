using System;
using System.ComponentModel.DataAnnotations;
using WaveWareTimes.Core.Entities.Domain;

namespace WaveWareTimes.Web.Models.Api
{
    public class WorkTimeRecordViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(512)]
        public string Description { get; set; }

        [Display(Name = "Created by")]
        public string UserName { get; set; }

    }
}
