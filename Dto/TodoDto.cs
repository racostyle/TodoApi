using System;

namespace Dto
{
    public class TodoDto : ITodo
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public bool Alert { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }
        public string Extra5 { get; set; }
    }
}
