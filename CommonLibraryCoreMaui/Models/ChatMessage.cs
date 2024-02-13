using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Models
{
	public class ChatMessage
	{
		public string Token { get; set; }
		public string ProviderID { get; set; }
		public string VisitID { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Text { get; set; }
		public string UploadedFileName { get; set; }
		public string Timestamp { get; set; }
		public string ReadTimestamp { get; set; }
		public string MessageType { get; set; }
		public bool IsDone { get; set; }
		public string Counter { get; set; }
	}
	//From Chat Server: WebSynchEvents.cs
	public class VisitRequestMessage
	{
		public string Token { get; set; }
		public string ProviderID { get; set; }
		public string VisitID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ReasonForVisit { get; set; }
		public string Timestamp { get; set; }
		public string ReadTimestamp { get; set; }
		public string MessageType { get; set; }
		public string SystemMessage { get; set; }
	}
	public class ChatTranscriptDTO
	{
		public int VisitID { get; set; }
		public string JsonContent { get; set; }
	}

}
