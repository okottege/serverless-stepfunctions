using PolicyCreator.Core;

namespace PolicyCreator.Functions.OutputDeclarations
{
    public class CreatePolicyResponse : BaseOperation
    {
        public string PolicyNumber { get; set; }
        public Quote Quote { get; set; }
    }
}
