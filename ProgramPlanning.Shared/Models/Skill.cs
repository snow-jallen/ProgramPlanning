using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Models
{
    public class Skill
    {
        public Skill()
        {

        }

        public Skill(string name)
        {
            Name = name;
        }

        public Skill(int id, string name) : this(name)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
