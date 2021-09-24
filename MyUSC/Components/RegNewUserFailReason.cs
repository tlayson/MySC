using System;

namespace SamplePortal.Components
{
	/// <summary>
	/// RegNewUserFailReason
	/// </summary>
	public enum RegNewUserResult
	{
		Success,
		UsernameAlreadyExists,
		DatabaseFail
	}
}
