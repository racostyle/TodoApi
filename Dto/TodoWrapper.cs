using System;

namespace Dto
{
    public class TodoWrapper : ITodo
    {
        private readonly ITodo _todo;

        public TodoWrapper(ITodo todo)
        {
            _todo = todo;
        }

        private DateTime DateWithoutMills(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, input.Second);
        }

        public DateTime CreatedDate { get => DateWithoutMills(_todo.CreatedDate); set => _todo.CreatedDate = DateWithoutMills(value); }
        public DateTime DueDate { get => DateWithoutMills(_todo.DueDate); set => _todo.DueDate = DateWithoutMills(value); }
        public string Creator { get => _todo.Creator; set => _todo.Creator = value; }
        public string Description { get => _todo.Description; set => _todo.Description = value; }
        public bool Alert { get => _todo.Alert; set => _todo.Alert = value; }
        public string Extra1 { get => _todo.Extra1; set => _todo.Extra1 = value; }
        public string Extra2 { get => _todo.Extra2; set => _todo.Extra2 = value; }
        public string Extra3 { get => _todo.Extra3; set => _todo.Extra3 = value; }
        public string Extra4 { get => _todo.Extra4; set => _todo.Extra4 = value; }
        public string Extra5 { get => _todo.Extra4; set => _todo.Extra4 = value; }
    }
}
