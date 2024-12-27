using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeamMember(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
