using FaceRecognitionDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionDataAccess.Repositories.Interfaces
{
    public interface IPersonPortraitRepository
    {
        List<PersonPortrait> Read();
        PersonPortrait ReadById(int id);
        int Save(PersonPortrait personPortrait);

    }
}
