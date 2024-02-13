using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public interface IProviderVisitAbsenceNotesService
    {
        Task<List<AbsenceNote>> GetAbsenceNotes(string sVisitID);
    }
}