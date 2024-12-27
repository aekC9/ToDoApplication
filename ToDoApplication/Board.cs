using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class Board
    {
        public List<Card> Todo { get; set; }
        public List<Card> InProgress { get; set; }
        public List<Card> Done { get; set; }

        public Board()
        {
            Todo = new List<Card>();
            InProgress = new List<Card>();
            Done = new List<Card>();
        }
    }
}
