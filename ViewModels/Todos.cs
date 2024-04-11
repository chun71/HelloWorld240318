
namespace HelloWorld240318.ViewModels
{
    public class Todos
    {
        public long ID { get; set; }

        public long UID { get; set; }

        public long? ItemNum { get; set; }

        public string Name { get; set; }

        public string CnName { get; set; }

        public string ItemName { get; set; }
        
        public bool IsDone { get; set; }

        public DateOnly? DeadLine { get; set; }

        public string DeadLineMsg { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
