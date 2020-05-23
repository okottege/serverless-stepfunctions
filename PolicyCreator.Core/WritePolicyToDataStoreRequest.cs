namespace PolicyCreator.Core
{
    public class WritePolicyToDataStoreRequest
    {
        public string PolicyNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Quote Quote { get; set; }
    }
}
