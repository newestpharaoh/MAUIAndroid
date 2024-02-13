using System;

namespace CommonLibraryCoreMaui.Exceptions
{
    public class ProviderException : Exception
    {
        public ProviderException(string message) : base(String.Format("Error: {0}", message))
        {

        }
    }

	public class PatientException : Exception
	{
		public PatientException(string message) : base(String.Format("Error: {0}", message))
		{

		}
	}
}