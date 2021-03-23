using System;

namespace CodingCompetition.Web.Models
{
	public class Message<T>
	{
		public string Sender { get; set; }
		public DateTime Sent { get; set; }
		public T Data { get; set; }
	}
}
