using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine.Serialization;

namespace SGame
{
	public struct Account : IComponentData
	{
		public long pid;
	}

	public class AccountData 
	{
		[FormerlySerializedAs("user_id")]
		public long playerID;
		public int platform;
		public string token;

		public string account;
		public string mail;

		public Account To()
		{
			return new Account() { pid = playerID };
		}

	}
}
