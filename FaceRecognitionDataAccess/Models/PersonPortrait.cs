using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionDataAccess.Models
{
    public class PersonPortrait
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public byte[] Portrait { get; set; }
    }
}
