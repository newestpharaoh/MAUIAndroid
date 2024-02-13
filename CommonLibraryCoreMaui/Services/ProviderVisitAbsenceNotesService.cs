using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class ProviderVisitAbsenceNotesService : IProviderVisitAbsenceNotesService
    {
        public ProviderVisitAbsenceNotesService()
        {
        }

        public async Task<List<AbsenceNote>> GetAbsenceNotes(string sVisitID)
        {
            List<AbsenceNote> absenceNotes = await DataUtility.GetVisitAbsenceNotesAsync(SettingsValues.ApiURLValue, sVisitID, CommonAuthSession.Token);
            return absenceNotes;
        }
    }
}