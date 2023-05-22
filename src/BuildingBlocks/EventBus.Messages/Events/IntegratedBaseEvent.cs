
namespace EventBus.Messages.Events
{
    public class IntegratedBaseEvent
    {
        public Guid Id { get;private set; }
        public DateTime CreationDate { get;private set; }
        public IntegratedBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        public IntegratedBaseEvent(Guid id,DateTime creationDate)
        {
            Id=id;
            CreationDate = creationDate;
        }
    }
}
