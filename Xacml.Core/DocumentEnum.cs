namespace Xacml.Core
{
	/// <summary>
	/// Enumeration describing the access to the document
	/// </summary>
	public enum DocumentAccess
	{
		/// <summary>
		/// Read-only access
		/// </summary>
		ReadOnly,

		/// <summary>
		/// Read-write access
		/// </summary>
		ReadWrite
	}

	/// <summary>
	/// Enumeration describing the document's type
	/// </summary>
	public enum DocumentType
	{
		/// <summary>
		/// Policy document
		/// </summary>
		Policy,

		/// <summary>
		/// Request document
		/// </summary>
        Request
	}
}
