namespace HelloWorld240318.ViewModels
{
    public class TodosSearch
    {
        public List<Todos> TodoList { get; set; }

        public long? UID { get; set; }

        public string Name { get; set;}

        public string DeadLine { get; set; }

        public string InsertDate { get; set;}

        public string UpdateDate { get; set;}

        public List<IdAndName> NameList { get; set; }
    }
}
