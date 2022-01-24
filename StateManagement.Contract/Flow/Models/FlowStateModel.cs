using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateManagement.Contract.Flow.Models
{
    public class FlowStateModel
    {
        public string Name { get; }
        public byte Order { get; }
        public long FlowId { get; }

        public FlowStateModel(string name, byte order, long flowId)
        {
            Name = name;
            Order = order;
            FlowId = flowId;
        }
    }
}
