using PolicyCreator.Core;

namespace PolicyCreator.Functions.InputDeclarations
{
    public class CreatePolicyRequest : BaseOperation
    {
        public Quote Quote { get; set; }
    }
}
