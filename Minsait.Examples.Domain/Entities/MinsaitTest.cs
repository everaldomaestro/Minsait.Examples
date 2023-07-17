namespace Minsait.Examples.Domain.Entities
{
    public class MinsaitTest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? Update { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }

        public void UpdateStatus(bool active)
        {
            Active = active;
            Update = DateTime.Now;
        }
    }
}
