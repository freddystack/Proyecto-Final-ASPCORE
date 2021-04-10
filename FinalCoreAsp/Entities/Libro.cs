using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCoreAsp.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
