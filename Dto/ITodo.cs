using System;

namespace Dto
{
    public interface ITodo
    {
        DateTime CreatedDate { get; set; }
        DateTime DueDate { get; set; }
        string Creator { get; set; }
        string Description { get; set; }
        bool Alert { get; set; }
        string Extra1 { get; set; }
        string Extra2 { get; set; }
        string Extra3 { get; set; }
        string Extra4 { get; set; }
        string Extra5 { get; set; }
    }
}