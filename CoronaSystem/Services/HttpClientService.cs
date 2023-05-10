namespace CoronaSystem.Services
{
	public static class HttpClientService
	{
		private static HttpClient _client = new HttpClient();

		public static async Task<bool> ExistInTheGovernmentDatabase (EGovernmentDatabaseTypes eType, String filter1, String filter2 = "")
		{
			String uri = "";
			uri += System.Configuration.ConfigurationManager.AppSettings.Get ("BaseGovernmentDataUri");

			switch (eType)
			{
				case EGovernmentDatabaseTypes.Addresses:
					uri += System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataAddressesId");
					break;
				case EGovernmentDatabaseTypes.Phones:
					uri += System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataPhonesId");
					break;
				default:
					return false;
			}
			uri += "&" + System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataFilters");
			switch (eType)
			{
				case EGovernmentDatabaseTypes.Addresses:
					uri += System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataCity")
						+ filter1 + " "
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataCloseFilter")
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataStreet")
						+ filter2 + " "
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataLastFilter");
					break;
				case EGovernmentDatabaseTypes.Phones:
					uri += System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataPhone")
						+ filter1
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataCloseFilter")
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataNotesPhone")
						+ filter2
						+ System.Configuration.ConfigurationManager.AppSettings.Get ("GovernmentDataLastFilter");
					break;
				default:
					return false;
			}
			var response = await _client.GetFromJsonAsync<Res>(uri);
			if (response == null)
				return false;
			return (response.result.total > 0);
		}
	}

	public enum EGovernmentDatabaseTypes
	{
		Addresses,
		Phones
	}


	public class Res
	{
		public Result result { get; set; }
	}
	public class Result
	{
		public int total { get; set; }
	}
}
