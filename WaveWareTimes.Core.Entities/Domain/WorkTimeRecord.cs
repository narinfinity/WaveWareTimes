using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveWareTimes.Core.Entities.Domain
{
    public class WorkTimeRecord: IEntityIdentity<int>
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
