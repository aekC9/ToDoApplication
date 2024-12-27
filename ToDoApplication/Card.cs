using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class Card
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public TeamMember AssignedMember { get; set; }
        public CardSize Size { get; set; }

        public Card(string title, string content, TeamMember assignedMember,CardSize size)
        {
            Title = title;
            Content = content;
            AssignedMember = assignedMember;
            Size = size;
        }
    }
}
