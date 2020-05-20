using System.Collections.Generic;

namespace PolicyCreator.Core
{
    public class Quote
    {
        public string Id { get; set; }
        public Person PolicyOwner { get; set; }
        public List<Person> OtherMembers { get; set; } = new List<Person>();
        public QuoteStatus Status { get; set; } = QuoteStatus.Created;
    }
}
