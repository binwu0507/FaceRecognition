using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionDataAccess.Models
{
    public class PersonSimilarity
    {
        public string CandidateName { get; set; }
        public FaceEncoding CandidateFaceEncoding { get; set; }
        public double Distance { get; set; }
    }
}
