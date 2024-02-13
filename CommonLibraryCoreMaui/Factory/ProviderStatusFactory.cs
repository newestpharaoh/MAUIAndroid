using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui
{
    public static class ProviderStatusFactory
	{
		public static IProviderStatus Get(int? visitId, int patientId, int providerId)
		{
			IProviderStatus ips = null;
			if (visitId is null)
			{
				ips = new ProviderUnavailableNoVisit();
				((ProviderUnavailableNoVisit)ips).PatientId = patientId;
				((ProviderUnavailableNoVisit)ips).ProviderId = providerId;
			}
			else
			{
				ips = new ProviderUnavailableWithVisit();
				((ProviderUnavailableWithVisit)ips).VisitId = (int)visitId;
			}

			return ips;
		}
	}

	public interface IProviderStatus
	{
		Task<bool> IsProviderNotAvailable(string token = "");
	}

	public class ProviderUnavailableNoVisit : IProviderStatus
	{
		public int PatientId { get; set; }
		public int ProviderId { get; set; }

		public async Task<bool> IsProviderNotAvailable(string token = "")
		{
			bool ret = false;

			try
			{
                List<ProviderInfo> providers = await DataUtility.PatientStartVisitStep3Async(SettingsValues.ApiURLValue, PatientId, string.IsNullOrEmpty(token) ? CommonAuthSession.Token : token).ConfigureAwait(false);

				if (providers != null)
				{
					if (!providers.Contains(providers.Where(x => x.ProviderID == ProviderId).FirstOrDefault()))
					{
						ret = true;
					}
				}
			}
			catch { }

			return await Task.FromResult<bool>(ret);
		}
	}

	public class ProviderUnavailableWithVisit : IProviderStatus
	{
		public int VisitId { get; set; }

		public async Task<bool> IsProviderNotAvailable(string token)
		{
			bool ret = false;

			try
			{
				StatusResponse resp = await DataUtility.PollVisitStatusAsync(SettingsValues.ApiURLValue, VisitId, string.IsNullOrEmpty(token) ? CommonAuthSession.Token : token).ConfigureAwait(false);

				if (resp.StatusCode == StatusCode.ProviderNotAvailable)
				{
					StatusResponse resp2 = await DataUtility.DeleteVisitAsync(SettingsValues.ApiURLValue, VisitId, string.IsNullOrEmpty(token) ? CommonAuthSession.Token : token).ConfigureAwait(false);

					StartVisit.Instance.VisitID = (int?)null;

					ret = true;
				}
			}
			catch { }

			return await Task.FromResult<bool>(ret);
		}
	}
}
