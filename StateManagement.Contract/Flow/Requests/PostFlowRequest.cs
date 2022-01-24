using StateManagement.Contract.Flow.Models;

namespace StateManagement.Contract.Flow.Requests
{
    public class PostFlowRequest
    {
        public string Name { get; set; }
        public List<FlowStateModel> FlowStates { get; set; }
    }


}
